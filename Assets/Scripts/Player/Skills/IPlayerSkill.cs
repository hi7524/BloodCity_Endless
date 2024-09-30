using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerSkill
{
    public bool isSkillActive { get; set; }
    public void UseSkill();
}
