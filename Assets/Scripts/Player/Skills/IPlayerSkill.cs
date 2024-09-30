using UnityEngine;

// 플레이어 스킬 인터페이스
public interface IPlayerSkill
{
    public Vector3 playerVec { get; set; }
    public void UseSkill();
}
