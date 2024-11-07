using UnityEngine;

// **테스트를 위한 임시 작성 코드**  플레이어의 스킬 영구 획득 테스트
public class DRAFT_UnlockSkills : MonoBehaviour
{
    PermanentSkillManager PermanentBuffSkill;

    private void Start()
    {
        // 컴포넌트 초기화
        // 플레이어의 영구 강화 스킬 스크립트 불러오기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PermanentBuffSkill = player.GetComponentInChildren<PermanentSkillManager>();
    }

    public void ClickUnlockBtn(int num)
    {
        PermanentBuffSkill.AddPermanentSkill(num);
    }
}
