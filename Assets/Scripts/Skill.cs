using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    [SerializeField]
    protected string skillName = "";

    [SerializeField]
    protected Sprite skillIcon = null;

    protected float skillCooldown = 1;

    bool isUsable = true;

    [SerializeField]
    bool isUnlocked = false;

    int skillLevel;

    [SerializeField]
    int maxSkillLevel;

    [SerializeField]
    protected Skill prerequisiteSkill = null;

    /// <summary>
    /// This is used to activate the skill and should not be modified
    /// </summary>
    protected void UseSkill()
    {
        if(isUsable == true && isUnlocked == true)
        {
            SkillFunctionality();
        }
    }


    /// <summary>
    /// This is used to determine what the skill is supposed to do
    /// This should be inherited from and modified
    /// </summary>
    protected virtual void SkillFunctionality()
    {

    }


    protected virtual void UpgradeSkill()
    {

    }

    protected virtual void UnlockSkill()
    {
        isUnlocked = true;
    }

    protected virtual IEnumerator SkillCooldownTimer()
    {
        isUsable = false;
        yield return new WaitForSeconds(skillCooldown);
        isUsable = true;
    }

}
