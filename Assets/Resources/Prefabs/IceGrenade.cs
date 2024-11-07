using UnityEngine;

// 얼음 수류탄
public class IceGrenade : Grenade
{
    // 얼음 수류탄 고유 스킬
    override public void ActivateSkill(Collider2D obj)
    {
        // 속도 저하
        obj.GetComponent<MobAI>().SetSpeed(1);
        obj.GetComponent<SpriteRenderer>().color = new Color(0.38f, 0.77f, 1f, 1f);
    }
}
