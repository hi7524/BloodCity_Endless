using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using System.Drawing;
using System.Runtime.CompilerServices;

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
    public float nowTime { get; private set; } // 현재 시간 (초)
    public int nowMin { get; protected set; } // 현재 시간 (분)

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

    private Coroutine spawn_coroutine; // 몬스터 생성 무한 루프 코루틴
    private Coroutine spawnBoss_coroutine; // 보스 몬스터 생성 코루틴

    private List<GameObject> Pools; // 오브젝트 풀
    public int spawnNums; //현재 스폰 마리 수    

    private Transform playerTransform; // 플레이어의 Transform
    private GameObject grid; // 그리드 오브젝트

    private float minXDistance = 35f; // X 방향 최소 거리
    private float minYDistance = 35f; // Y 방향 최소 거리

    private LayerMask obstacleLayer; // 콜라이더를 가진 오브젝트의 레이어

    public float[] hpPers = { 1, 1.1f, 1.15f, 1.15f, 1.2f, 1.3f, 1.35f, 1.4f, 1.45f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2, 2 }; // 시간 비례 몬스터 체력량  


    public void ResetAll() // 일괄 초기화 (씬 전환 후 호출 필수)
    {
        // isReady = true;
        isReady = false;
        Time.timeScale = 1f; // 게임 시작 시 타임 스케일 초기화
        nowTime = 0; // 현재 시간 초기화

        spawnNums = 0; // 현재 스폰 수 초기화
        Pools = new List<GameObject>(); // 풀 초기화
        grid = GameObject.Find("Grid");

        spawn_coroutine = StartCoroutine(SpawnMonster()); // 스폰 코루틴 최초 시작
        spawnBoss_coroutine = StartCoroutine(SpawnBossMonster_Routine());
    }

    void Start() // 씬 시작 시 최초 초기화
    {
        ResetAll();
    }


    void Update() // 현재 시간 업데이트
    {
        if (isReady || isGameOver || isPaused) return; // 게임 오버 상태나 정지 상태일 경우 시간 업뎃 방지
        nowTime += Time.deltaTime;
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


    private IEnumerator SpawnMonster() // 몬스터 스폰 코루틴
    {
        while (true) // 무한 루프
        {
            if (isReady || isGameOver || isPaused) // 게임 오버 상태나 정지 상태일 경우 스폰 방지
            {
                yield return new WaitForSeconds(0.2f);
            }
            else
            {

                nowMin = (int)(nowTime / 60); // 현재 분
                nowMin = nowMin > 15 ? 15 : nowMin;

                // print("현재 시간(분) : " + nowMin);
                // print("몬스터 수 : " + spawnNums + " / " + minSpawnNums[nowMin]);

                if (spawnNums < minSpawnNums[nowMin]) // 최소 스폰 마리 미달일 경우
                {
                    playerTransform = GameObject.FindWithTag("Player").transform;

                    if (playerTransform != null) // 플레이어 유닛이 있을 때만
                    {
                        int spawnPer = Random.Range(1, 101); // 스폰 확률
                        GameObject mob = null; // 스폰할 몹

                        int spawnPers = 0; // 합산 스폰 확률
                        int rand = Random.Range(1, 101);

                        foreach (var monster in NormalMobPrefabs)
                        {
                            int nowSpawnPer = monster.GetComponent<MobAI>().obj.spawnPers[nowMin]; // 현재 스폰 확률

                            if (nowSpawnPer > 0) // 스폰 확률이 있고
                            {
                                spawnPers += nowSpawnPer;
                                // print("랜덤 확률 : " + rand + " 현재 스폰 확률 : " + spawnPers + "\n몬스터 이름 : " + monster.GetComponent<MobAI>().obj.name);
                                if (rand <= spawnPers) // 랜덤 확률보다 높거나 같다면
                                {
                                    mob = monster; // 이 몬스터로 스폰
                                    break;
                                }

                            }

                        }

                        // 스폰 포인트 구함
                        float xDir = (Random.value > 0.5f ? 1 : -1) * Random.value;
                        float yDir = (Random.value > 0.5f ? 1 : -1) * Random.value;
                        float spawnX = playerTransform.position.x + Random.Range(minXDistance * xDir, 45 * xDir);
                        float spawnY = playerTransform.position.y + Random.Range(minYDistance * yDir, 45 * yDir);

                        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

                        // 몬스터 생성
                        if (mob != null)
                        {
                            Instantiate(mob, spawnPosition, Quaternion.identity)
                                .GetComponent<MobAI>().Init(hpPers[nowMin]);
                            spawnNums += 1;
                        }

                    }
                }

                // 0.1초 대기
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    public void SpawnBossMonster(int stage = 0) // 보스 몬스터 스폰
    {
        float xDir = (Random.value > 0.5f ? 1 : -1) * Random.value;
        float yDir = (Random.value > 0.5f ? 1 : -1) * Random.value;
        float spawnX = playerTransform.position.x + Random.Range(minXDistance * xDir, 50 * xDir);
        float spawnY = playerTransform.position.y + Random.Range(minYDistance * yDir, 50 * yDir);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        Instantiate(stage > 0 ? BossMobPrefabs[stage - 1] : HalfBossMobPrefabs[Random.Range(0, HalfBossMobPrefabs.Length)], spawnPosition, Quaternion.identity)
        .GetComponent<MobAI>().Init(hpPers[nowMin]);
    }

    private IEnumerator SpawnBossMonster_Routine() // 보스 몬스터 스폰 코루틴 (임시)
    {

        yield return new WaitForSeconds(5);
        SpawnBossMonster();
        yield return new WaitForSeconds(5);
        SpawnBossMonster();
        yield return new WaitForSeconds(5);
        SpawnBossMonster();
        yield return new WaitForSeconds(30);
        SpawnBossMonster(1);
    }

    /*
    private IEnumerator SpawnBossMonster_Routine() // 보스 몬스터 스폰 코루틴
    {

        yield return new WaitForSeconds(180);
        SpawnBossMonster();
        yield return new WaitForSeconds(120);
        SpawnBossMonster();
        yield return new WaitForSeconds(60);
        SpawnBossMonster();
        yield return new WaitForSeconds(180);
        SpawnBossMonster();
        yield return new WaitForSeconds(60);
        SpawnBossMonster();
        yield return new WaitForSeconds(120);
        SpawnBossMonster();
        yield return new WaitForSeconds(180);
        SpawnBossMonster(1);
    }
    */

    void OnDestroy()
    {
        // 객체가 파괴될 때 코루틴 중지
        if (spawn_coroutine != null)
        {
            StopCoroutine(spawn_coroutine);
        }
        if (spawnBoss_coroutine != null)
        {
            StopCoroutine(spawnBoss_coroutine);
        }

    }

}
