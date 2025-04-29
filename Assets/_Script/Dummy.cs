using UnityEngine;
using System;

public class Dummy : MonoBehaviour
{
    [SerializeField] private float chaseRadius = 10f; // 추적 반경
    [SerializeField] private float moveSpeed = 3f; // 이동 속도
    [SerializeField] private float rotationSpeed = 5f; // 회전 속도
    [SerializeField] private LayerMask playerLayer; // 플레이어 레이어
    [SerializeField] private float maxHealth = 50f; // 최대 체력
    [SerializeField] private GameObject bulletPrefab; // 총알 프리팹
    [SerializeField] private Transform firePoint; // 총구 위치
    [SerializeField] private float fireRate = 1f; // 발사 주기 (초)
    private Transform player; // 추적 대상 (플레이어)
    private bool isChasing; // 추적 중인지 여부
    private float currentHealth; // 현재 체력
    private float detectionInterval = 0.5f; // 감지 주기 (초)
    private float nextDetectionTime; // 다음 감지 시간
    private float nextFireTime; // 다음 발사 시간
    public Action OnDestroyCallback; // 파괴 시 호출되는 콜백

    void Start()
    {
        currentHealth = maxHealth;
        nextDetectionTime = Time.time; 
        nextFireTime = Time.time; 
    }

    void Update()
    {
        if (Time.time >= nextDetectionTime)
        {
            DetectPlayer();
            nextDetectionTime = Time.time + detectionInterval;
        }

        // 플레이어 추적 및 발사
        if (isChasing && player != null)
        {
            ChasePlayer();
            ShootAtPlayer();
        }
    }

    // 플레이어 감지
    private void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, chaseRadius, playerLayer);
        isChasing = false; 

        if (hits.Length > 0)
        {
            player = hits[0].transform;
            isChasing = true;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Y축 이동 제외 (지면 이동)

        // 플레이어를 바라보도록 회전
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 moveDirection = direction * moveSpeed * Time.deltaTime;
        transform.position += moveDirection;
    }

    private void ShootAtPlayer()
    {
        if (Time.time >= nextFireTime && firePoint != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Vector3 shootDirection = (player.position - firePoint.position).normalized;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = shootDirection * 20f; 
            }

            nextFireTime = Time.time + fireRate;
        }
    }


    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 체력 0 이하 시 파괴
        }
    }


    private void OnDestroy()
    {
        OnDestroyCallback?.Invoke();
    }

    
}