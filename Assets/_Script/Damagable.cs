using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; // 최대 체력
    [SerializeField] private GameObject particleObjectPrefab; // 파티클로 사용할 3D 객체 프리팹
    [SerializeField] private int particleCount = 5; // 한 번에 생성할 객체 수
    [SerializeField] private float particleDuration = 0.5f; // 객체 지속 시간
    [SerializeField] private float particleSpeed = 2f; // 객체 이동 속도
    [SerializeField] private float particleAngularSpeed = 360f; // 객체 회전 속도 (도/초)
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        // 3D 객체 파티클 생성
        if (particleObjectPrefab != null)
        {
            for (int i = 0; i < particleCount; i++)
            {
                GameObject particle = Instantiate(particleObjectPrefab, transform.position, Random.rotation);

                Rigidbody rb = particle.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = particle.AddComponent<Rigidbody>();
                }
                rb.useGravity = false; // 중력 비활성화
                rb.linearVelocity = Random.insideUnitSphere * particleSpeed; // 무작위 방향 속도
                rb.angularVelocity = Random.insideUnitSphere * particleAngularSpeed * Mathf.Deg2Rad; // 무작위 회전

                Destroy(particle, particleDuration);
            }
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }
}