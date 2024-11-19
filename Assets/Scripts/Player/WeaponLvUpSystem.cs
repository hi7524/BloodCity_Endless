using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// 설명
public class WeaponLvUpSystem : MonoBehaviour
{
    public Image weaponImg; // 업그레이드 할 무기를 보여주는 이미지 
    public Text weaponNameTMP; // 무기 이름을 나타낼 TMP
    public Text optionTMP0;
    public Text optionTMP1;
    public AudioClip selectSound; // 랜덤 뽑기시 재생할 사운드

    private PlayerWeaponManager weaponManager;
    private AudioSource audioSource;
    private int upgradeWeaponNum;
    private Sprite weaponSprite;


    private void Awake()
    {
        // 컴포넌트 초기화
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 플레이어 WeaponManager 받아오기
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        weaponManager = playerObject.GetComponentInChildren<PlayerWeaponManager>();

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
    }

    // 업그레이드 할 속성 설정
    private void SetUpgradeState()
    {
        int upgradeStateNum = Random.Range(0, 1);
      
        if (weaponManager.playerSkills[upgradeWeaponNum].name == "ShotGun")
        {
            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("공격력 증가", 0.2f);
            optionTMP1.DOText("사거리 증가", 0.2f);
            // 총알 개수 추가, 공격력 증가, 사거리 증가 등
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "HeavyMachineGun")
        {
            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("공격력 증가", 0.2f);
            optionTMP1.DOText("발사 속도 증가", 0.2f);

            // 공격력 증가, 공격 속도 증가
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "PlasmaGun")
        {
            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("틱데미지 증가", 0.2f);
            optionTMP1.DOText("적 추적 속도 증가", 0.2f);
            // 틱데미지 증가, 발사 속도
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "ShaftGun")
        {
            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("공격력 증가", 0.2f);
            optionTMP1.DOText("발사 속도 증가", 0.2f);
            // 공격력 증가, 공격 속도 증가
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "RailGun")
        {
            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("공격력 증가", 0.2f);
            optionTMP1.DOText("발사 속도 증가", 0.2f);
            // 공격력 증가, 공격 속도 증가
        }
        else if (weaponManager.playerSkills[upgradeWeaponNum].name == "RocketLauncher")
        {

            weaponNameTMP.DOText(weaponManager.playerSkills[upgradeWeaponNum].name.ToUpper(), 0.5f);
            optionTMP0.DOText("폭발 범위 증가", 0.2f);
            optionTMP1.DOText("공격력 증가", 0.2f);
            // 공격력 증가, 폭발 범위 증가, 공격 속도 증가
        }
        else
        {
            Debug.Log("등록되지 않은 무기입니다.");
        }
    }
}
