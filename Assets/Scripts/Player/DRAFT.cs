using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀링을 위한 기본 클래스
    [System.Serializable]
    public class Pool
    {
        public string tag; // 오브젝트 태그
        public GameObject prefab; // 생성할 프리팹
        public int size; // 풀 크기
    }

    public List<Pool> pools; // 여러 풀 관리
    public Dictionary<string, Queue<GameObject>> poolDictionary; // 풀 딕셔너리

    void Start()
    {
        // 풀 딕셔너리 초기화
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 각 풀 초기화
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // 지정된 크기만큼 오브젝트 생성
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false); // 비활성화 상태로 초기화
                objectPool.Enqueue(obj); // 큐에 추가
            }

            poolDictionary.Add(pool.tag, objectPool); // 딕셔너리에 추가
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // 지정된 태그가 딕셔너리에 없으면 null 반환
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        // 큐에서 오브젝트 가져오기
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true); // 활성화
        objectToSpawn.transform.position = position; // 위치 설정
        objectToSpawn.transform.rotation = rotation; // 회전 설정

        // 다시 큐에 추가
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn; // 스폰된 오브젝트 반환
    }

    
}


