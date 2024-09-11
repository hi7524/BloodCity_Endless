using UnityEngine;
using UnityEngine.UI;

// 플레이어의 스킬 사용 및 관리
public class PlayerSkill : MonoBehaviour
{
    // 플레임 벨트 스킬 오브젝트
    public GameObject flameObj;

    // 쿨타임을 나타낼 이미지
    public Image fragemntCoolImg;
    public Image iceCoolImg;

    private float fragemntTime; // 파편 수류탄 스킬 시간 계산 위한 변수
    private float fragemntCoolTime = 1; // 파편 수류탄 쿨타임
    private float iceTime; // 얼음 수류탄 스킬 시간 계산 위한 변수
    private float iceCoolTime = 2; // 얼음 수류탄 쿨타임

    private PlayerCharacterState characterState;

    private void Awake()
    {
        characterState = GetComponent<PlayerCharacterState>();
    }

    private void Start()
    {
        // 쿨타임 설정
        SetCooltime();

        // 스킬 해금 여부에 따라 활성화 및 비활성화
        if (GameManager.Instance.passiveSkill)
        {
            flameObj.SetActive(true);
        }
        else
        {
            flameObj.SetActive(false);
        }

        fragemntCoolImg.fillAmount = 1;
        iceCoolImg.fillAmount = 1;
    }

    private void Update()
    {
        if (GameManager.Instance.fragmentGrenade)
        {
            fragemntCoolImg.fillAmount = fragemntTime / fragemntCoolTime;
            fragemntTime += Time.deltaTime;
            if (fragemntTime > fragemntCoolTime)
            {
                FragmentGrenade();
                fragemntTime = 0;
            }
        }

        if (GameManager.Instance.iceGrenade)
        {
            iceCoolImg.fillAmount = iceTime / iceCoolTime;
            iceTime += Time.deltaTime;
            if (iceTime > iceCoolTime)
            {
                IceGrenade();
                iceTime = 0;
            }
        }
    }

    // 쿨타임 설정
    private void SetCooltime()
    {
        fragemntCoolTime = fragemntCoolTime - (fragemntCoolTime * characterState.abilityHaste); // 쿨감 적용 쿨타임
        iceCoolTime = iceCoolTime - (iceCoolTime * characterState.abilityHaste); // 쿨감 적용 쿨타임
    }

    private void FragmentGrenade()
    {
        Debug.Log("파편 수류탄 스킬");
    }

    private void IceGrenade()
    {
        Debug.Log("얼음 수류탄 스킬");
    }
}
