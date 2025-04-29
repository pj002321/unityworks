using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // �Ѿ� ������
    [SerializeField] private Transform firePoint; // �ѱ� ��ġ (��: �� ��)
    [SerializeField] private float bulletSpeed = 20f; // �Ѿ� �ӵ�
    [SerializeField] private float fireRate = 0.2f; // ��� ���� (��)
    private float nextFireTime; 
    private Camera mainCamera; 

    void Start()
    {
        mainCamera = Camera.main; 
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�
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