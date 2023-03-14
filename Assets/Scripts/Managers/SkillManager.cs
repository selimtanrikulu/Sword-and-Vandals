using UnityEngine;


public enum SkillType
{
    Basic,
    Skill1,
    Skill2,
}



public interface ISkillManager
{


    Skill GetSkill(WeaponType weaponType, SkillType skillType);

}


public class SkillManager : ISkillManager
{
    private SkillPack _skillPack;
    
    
    SkillManager(SkillPack skillPack)
    {
        _skillPack = skillPack;
    }



    public Skill GetSkill(WeaponType weaponType, SkillType skillType)
    {
        switch (weaponType)
        {
            case WeaponType.OneHandedSword:
                return GetSkillBySkillType(_skillPack.swordShieldSkillSet, skillType);

            case WeaponType.TwoHandedSword:
                return GetSkillBySkillType(_skillPack.twoHandedSwordSkillSet, skillType);
            
            case WeaponType.Bow:
                return GetSkillBySkillType(_skillPack.archerSkillSet, skillType);

            default:
                Debug.LogError("Unknown weapon type !");
                return null;

        }
    }

    
    //for internal usage
    private Skill GetSkillBySkillType(SkillSet skillSet, SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Basic:
                return skillSet.basicAttack;
            
            case SkillType.Skill1:
                return skillSet.skill1;
            
            case SkillType.Skill2:
                return skillSet.skill2;
            
            default:
                Debug.LogError("Unknown skill type");
                return null;
        }
    }
    
}
