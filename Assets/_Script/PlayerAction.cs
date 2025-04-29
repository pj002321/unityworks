using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // 총알 프리팹
    [SerializeField] private Transform firePoint; // 총구 위치 (예: 총 끝)
    [SerializeField] private float bulletSpeed = 20f; // 총알 속도
    [SerializeField] private float fireRate = 0.2f; // 사격 간격 (초)
    private float nextFireTime; 
    private Camera mainCamera; 

    void Start()
    {
        mainCamera = Camera.main; 
    }

    // Update는 매 프레임마다 호출됩니다
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            Vector3 shootDirection = mainCamera.transform.forward; 
            bulletRb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}