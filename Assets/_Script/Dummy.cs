using UnityEngine;
using System;

public class Dummy : MonoBehaviour
{
    [SerializeField] private float chaseRadius = 10f; // ���� �ݰ�
    [SerializeField] private float moveSpeed = 3f; // �̵� �ӵ�
    [SerializeField] private float rotationSpeed = 5f; // ȸ�� �ӵ�
    [SerializeField] private LayerMask playerLayer; // �÷��̾� ���̾�
    [SerializeField] private float maxHealth = 50f; // �ִ� ü��
    [SerializeField] private GameObject bulletPrefab; // �Ѿ� ������
    [SerializeField] private Transform firePoint; // �ѱ� ��ġ
    [SerializeField] private float fireRate = 1f; // �߻� �ֱ� (��)
    private Transform player; // ���� ��� (�÷��̾�)
    private bool isChasing; // ���� ������ ����
    private float currentHealth; // ���� ü��
    private float detectionInterval = 0.5f; // ���� �ֱ� (��)
    private float nextDetectionTime; // ���� ���� �ð�
    private float nextFireTime; // ���� �߻� �ð�
    public Action OnDestroyCallback; // �ı� �� ȣ��Ǵ� �ݹ�

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

        // �÷��̾� ���� �� �߻�
        if (isChasing && player != null)
        {
            ChasePlayer();
            ShootAtPlayer();
        }
    }

    // �÷��̾� ����
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
        direction.y = 0; // Y�� �̵� ���� (���� �̵�)

        // �÷��̾ �ٶ󺸵��� ȸ��
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
            Destroy(gameObject); // ü�� 0 ���� �� �ı�
        }
    }


    private void OnDestroy()
    {
        OnDestroyCallback?.Invoke();
    }

    
}