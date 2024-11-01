using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

public class TimeManager : MonoBehaviour // 타임 매니저 (스폰 기능 처리)
{
    public static TimeManager Instance { get; private set; } // 싱글톤 인스턴스

    private void Awake() // 파괴 방지
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // 시간 관련 프로퍼티
    public float nowTime { get; private set; } // 현재 시간
    public bool isReady { get; private set; } = true; // 준비 여부
    public bool isPaused { get; private set; } = false; // 정지 여부
    public bool isGameOver { get; private set; } = false; // 게임 오버 여부


    // 스폰 관련 프로퍼티
    [SerializeField]
    private int[] minSpawnNums = new int[16]; // 분당 최소 스폰 마리수

    [SerializeField]
    private GameObject[] NormalMobPrefabs; // 일반 몬스터 프리팹

    [SerializeField]
    private GameObject[] HalfBossMobPrefabs; // 중간 보스 몬스터 프리팹

    [SerializeField]
    private GameObject[] BossMobPrefabs; // 보스 몬스터 프리팹

    private List<GameObject> Pools; // 오브젝트 풀
    private int spawnNums; //현재 스폰 마리 수    

    private Transform playerTransform; // 플레이어의 Transform
    private Tilemap tilemap; // 타일맵

    private float minXDistance = 20f; // X 방향 최소 거리
    private float minYDistance = 15f; // Y 방향 최소 거리

    private LayerMask obstacleLayer; // 콜라이더를 가진 오브젝트의 레이어



    public void ResetAll() // 일괄 초기화 (씬 전환 후 호출 필수)
    {
        // isReady = true;
        isReady = false;
        Time.timeScale = 1f; // 게임 시작 시 타임 스케일 초기화
        nowTime = 0; // 현재 시간 초기화
        
        spawnNums = 0; // 현재 스폰 수 초기화
        Pools = new List<GameObject>(); // 풀 초기화
    }

    void Start() // 씬 시작 시 최초 초기화
    {
        ResetAll();
    }


    void Update() // 현재 시간 업데이트
    {
        print("현재 시간 : " + (int)nowTime);

        if (isReady || isGameOver || isPaused) return; // 게임 오버 상태나 정지 상태일 경우 시간 업뎃 방지

        nowTime += Time.deltaTime;

        spawnNums = GameObject.FindGameObjectsWithTag("Enemy").Length; // 현재 몬스터 스폰 수

        SpawnMonster(); // 몬스터 스폰 검사 및 수행
    }

    
    public void EndReady() // 준비 종료
    {
        isReady = false;
    }

    public void GameOver() // 게임 오버 처리
    {
        isGameOver = true;
        Time.timeScale = 0; // 게임 오버 시 시간 정지
    }

    public void TogglePause() // 일시 정지 처리
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void SetTimeScale(float scale) // 시간 속도 설정
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // 물리 계산 보정
    }


    private void SpawnMonster() // 몬스터 스폰
    {

        int nowMin = (int)(nowTime / 60); // 현재 분
        nowMin = nowMin > 15 ? 15 : nowMin;

        
        if (spawnNums < minSpawnNums[nowMin]) // 최소 스폰 마리 미달일 경우
        playerTransform = GameObject.FindWithTag("Player").transform;

        if (playerTransform != null ) // 플레이어 유닛이 있을 때만
        {
            int spawnPer = Random.Range(1, 101); // 스폰 확률
            GameObject mob = null; // 스폰할 몹

            int spawnPers = 0; // 합산 스폰 확률
            foreach (var monster in NormalMobPrefabs)
            {
                int nowSpawnPer = monster.GetComponent<MobAI>().obj.spawnPers[nowMin]; // 현재 스폰 확률

                if (nowSpawnPer > 0) // 스폰 확률이 있고
                {
                    spawnPers += nowSpawnPer;

                    if (spawnPers >= nowSpawnPer) // 현재 스폰 확률보다 높거나 같다면
                    {
                        mob = monster; // 이 몬스터로 스폰
                        break;
                    }

                }

            }

            Vector2 spawnPosition = GetSpawnPosition(); // 스폰 포인트 구함

            // 유효한 위치에서만 몬스터 생성
            if (mob != null && spawnPosition != Vector2.zero)
                Instantiate(mob, spawnPosition, Quaternion.identity);
        }

    }

    private Vector2 GetSpawnPosition() // 스폰 포인트 반환
    {
        for (int i = 0; i < 10; i++) // 시도 횟수 제한 (10회)
        {
            float spawnX = playerTransform.position.x + (Random.value > 0.5f ? minXDistance : -minXDistance) + Random.Range(0, 5f);
            float spawnY = playerTransform.position.y + (Random.value > 0.5f ? minYDistance : -minYDistance) + Random.Range(0, 5f);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            // 스폰 위치가 타일맵 위에 있는지 확인
            Vector3Int cellPosition = tilemap.WorldToCell(spawnPosition);
            if (tilemap.HasTile(cellPosition))
            {
                // 타일이 있고 콜라이더가 없을 경우에만 반환
                if (!Physics2D.OverlapPoint(spawnPosition, obstacleLayer))
                {
                    return spawnPosition;
                }
            }
        }

        // 유효한 위치를 찾지 못하면 Vector2.zero 반환
        return Vector2.zero;
    }

}