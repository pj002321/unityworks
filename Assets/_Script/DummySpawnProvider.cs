using UnityEngine;
using System.Collections.Generic;

public class DummySpawnProvider : MonoBehaviour
{
    [SerializeField] private GameObject dummyPrefab; // ������ ������ (��: Dummy)
    [SerializeField] private List<Transform> spawnPoints; // ���� ��ġ ����Ʈ
    [SerializeField] private float spawnInterval = 5f; // ���� �ֱ� (��)
    [SerializeField] private int maxSpawnCount = 10; // �ִ� ���� �� (0 = ������)
    private float nextSpawnTime; // ���� ���� �ð�
    private int currentSpawnCount; // ���� ������ ��ü ��

    // Start�� MonoBehaviour�� ������ �� ù Update ���� ���� �� �� ȣ��˴ϴ�
    void Start()
    {
        nextSpawnTime = Time.time; // �ʱ� ���� �ð� ����
        currentSpawnCount = 0; // ���� ī��Ʈ �ʱ�ȭ
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�
    void Update()
    {
        if (Time.time >= nextSpawnTime && (maxSpawnCount == 0 || currentSpawnCount < maxSpawnCount))
        {
            SpawnDummy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // ������ ����
    private void SpawnDummy()
    {

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Spawn points list is empty!");
            return;
        }


        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[randomIndex].position;

        GameObject spawnedDummy = Instantiate(dummyPrefab, spawnPosition, Quaternion.identity);
        currentSpawnCount++; 

        Dummy dummyScript = spawnedDummy.GetComponent<Dummy>();
        if (dummyScript != null)
        {
            spawnedDummy.GetComponent<Dummy>().OnDestroyCallback += () => currentSpawnCount--;
        }
    }

 
}