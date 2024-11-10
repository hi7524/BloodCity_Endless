using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 설명
public class WeaponLvUpSystem : MonoBehaviour
{
    public Image weaponImg; // 업그레이드 할 무기를 보여주는 이미지 
    public TMP_Text weaponNameTMP; // 무기 이름을 나타낼 TMP
    public TMP_Text optionTMP0;
    public TMP_Text optionTMP1;
    public AudioClip selectSound; // 랜덤 뽑기시 재생할 사운드

    private PlayerWeaponManager weaponManager;
    private AudioSource audioSource;
    private int upgradeWeaponNum;
    private Sprite weaponSprite;


    private void Awake()
    {
        // 컴포넌트 초기화
        audioSource = gameObject.GetComponent<AudioSource>();

        // 플레이어 WeaponManager 받아오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        weaponManager = playerObject.GetComponentInChildren<PlayerWeaponManager>();
    }

    private void Start()
    {
        // 초기 설정
        weaponNameTMP.text = "???";
        optionTMP0.text = "?";
        optionTMP1.text = "?";

        // 업그레이드 무기 설정
        if (weaponManager.count == 1) // 무기를 1개만 소지하고 있으면 랜덤 뽑기 진행되지 않음
        {
            SetUpgradeWeapon();
        }
        else
        {
            StartCoroutine(RandomEffect());
        }
    }

    // 랜덤 뽑기 이펙트
    IEnumerator RandomEffect()
    {
        float setTime = 0;
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < weaponManager.count; j++)
            {
                // 스프라이트 설정 및 조정
                weaponSprite = weaponManager.playerSkills[j].GetComponent<SpriteRenderer>().sprite;
                weaponImg.sprite = weaponSprite;
                weaponImg.SetNativeSize();

                // 효과음 재생
                audioSource.PlayOneShot(selectSound);

                if (j == weaponManager.count)
                {
                    j = 0;
                }
                setTime = i * 0.01f;
                yield return new WaitForSecondsRealtime(i * 0.02f);
            }
        }

        // 최종 결정
        Debug.Log(setTime);
        yield return new WaitForSecondsRealtime(setTime);
        SetUpgradeWeapon();
    }

    // 업그레이드 무기 설정
    private void SetUpgradeWeapon()
    {
        // 무기 결정
        upgradeWeaponNum = Random.Range(0, weaponManager.count);

        // 결정된 무기 스프라이트로 변경
        weaponSprite = weaponManager.playerSkills[upgradeWeaponNum].GetComponent<SpriteRenderer>().sprite;
        weaponImg.sprite = weaponSprite;
        weaponImg.SetNativeSize();

        // 효과음
        audioSource.PlayOneShot(selectSound);

        // 무기에 따라 업그레이드 할 스텟 결정 
        SetUpgradeState();

        Debug.Log(weaponManager.playerSkills[upgradeWeaponNum].name);
    }


    // 업그레이드 할 속성 설정
    private void SetUpgradeState()
    {
        int upgradeStateNum = Random.Range(0, 1);
        // 샷건

        if (weaponManager.playerSkills[upgradeWeaponNum].name == "ShotGun")
        {
            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "ShotGun 옵션 1";
            optionTMP1.text = "ShotGun 옵션 2";
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "HeavyMachineGun")
        {
            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "HeavyMachineGun 옵션 1";
            optionTMP1.text = "HeavyMachineGun 옵션 2";
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "PlasmaGun")
        {
            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "PlasmaGun 옵션 1";
            optionTMP1.text = "PlasmaGun 옵션 2";
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "ShaftGun")
        {
            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "ShaftGun 옵션 1";
            optionTMP1.text = "ShaftGun 옵션 2";
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "RailGun")
        {

            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "RailGun 옵션 1";
            optionTMP1.text = "RailGun 옵션 2";
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "RocketLauncher")
        {

            weaponNameTMP.text = weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper();
            optionTMP0.text = "RocketLauncher 옵션 1";
            optionTMP1.text = "RocketLauncher 옵션 2";
        }
        else
        {
            Debug.Log("등록되지 않은 무기입니다.");
        }

        Debug.Log(weaponManager.playerSkills[upgradeWeaponNum]);
        // 헤비 머신건
        // 플라즈마건
        // 샤프트 건
        // 로켓런처


    }


    // 지금 가지고 있는 무기 받아오기
    // 랜덤하게 선택하기
    // 스탯 중 랜덤하게 업그레이드 하기
    // 업그레이드 할 스탯: 공격력 증가, 공격 속도 증가, 샷건이면 사거리 증가..? 총알 개수 증가
    // 업그레이드 하고나면 Sprite 변경 되기



}
