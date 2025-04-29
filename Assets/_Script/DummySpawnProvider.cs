using UnityEngine;
using System.Collections.Generic;

public class DummySpawnProvider : MonoBehaviour
{
    [SerializeField] private GameObject dummyPrefab; // 스폰할 프리팹 (예: Dummy)
    [SerializeField] private List<Transform> spawnPoints; // 스폰 위치 리스트
    [SerializeField] private float spawnInterval = 5f; // 스폰 주기 (초)
    [SerializeField] private int maxSpawnCount = 10; // 최대 스폰 수 (0 = 무제한)
    private float nextSpawnTime; // 다음 스폰 시간
    private int currentSpawnCount; // 현재 스폰된 객체 수

    // Start는 MonoBehaviour가 생성된 후 첫 Update 실행 전에 한 번 호출됩니다
    void Start()
    {
        nextSpawnTime = Time.time; // 초기 스폰 시간 설정
        currentSpawnCount = 0; // 스폰 카운트 초기화
    }

    // Update는 매 프레임마다 호출됩니다
    void Update()
    {
        if (Time.time >= nextSpawnTime && (maxSpawnCount == 0 || currentSpawnCount < maxSpawnCount))
        {
            SpawnDummy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // 프리팹 스폰
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