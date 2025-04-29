using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // 총알 데미지
    [SerializeField] private float lifetime = 5f; // 총알 생존 시간 (초)

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // 충돌 처리

    private void OnTriggerEnter(Collider other)
    {
        Damageable target = other.gameObject.GetComponent<Damageable>();
        Debug.Log($"target : {target}");
        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}