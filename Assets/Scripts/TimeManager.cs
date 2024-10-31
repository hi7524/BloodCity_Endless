using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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


    void Start() // 씬 시작 시 초기화
    {
        Time.timeScale = 1f; // 게임 시작 시 타임 스케일 초기화
        nowTime = 0; // 현재 시간 초기화
    }


    void Update() // 현재 시간 업데이트
    {
        print("현재 시간 : " + (int)nowTime);

        if (isReady || isGameOver || isPaused) return; // 게임 오버 상태나 정지 상태일 경우 시간 업뎃 방지

        nowTime += Time.deltaTime;
    }


    public void ResetTime() // 시간 초기화
    {
        isReady = true;
        nowTime = 0;
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

}