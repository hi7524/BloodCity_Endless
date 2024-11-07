using UnityEngine;
using UnityEngine.UI;


// 영구 강화를 통해 얻는 스킬
public class PermanentSkillManager : MonoBehaviour
{
    [Header("영구 강화 스킬")] 
    public GameObject[] permanentSkills; // 영구 강화를 통해 얻을 수 있는 전체 스킬

    private int skillCount = 0; // 현재 사용 스킬 개수

    public Image[] skillImg;



    public Sprite[] allSkillImgs;

    public void AddPermanentSkill(int skillID)
    {
        // 사용 스킬 최대 개수 2개 제한
        if (skillCount < 2)
        {
            SetSkills();
            // 스킬 개수 추가
            skillCount++;

            // 스킬 활성화 (생성)
            GameObject permanentSkill = Instantiate(permanentSkills[skillID]);
            permanentSkill.transform.parent = transform;

            // 위치값 초기화
            permanentSkill.transform.localPosition = Vector3.zero; // 위치 초기화
            permanentSkill.transform.localRotation = Quaternion.identity; // 회전 초기화
        }
        else
        {
            Debug.Log("이미 2개의 스킬을 사용하고 있습니다.");
        }
    }



    public void SetSkills()
    {
        skillImg[skillCount].sprite = allSkillImgs[2];
    }


}
