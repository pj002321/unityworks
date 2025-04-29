using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // �Ѿ� ������
    [SerializeField] private float lifetime = 5f; // �Ѿ� ���� �ð� (��)

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // �浹 ó��

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