using UnityEngine;


public enum SkillType
{
    Basic,
    Skill1,
    Skill2,
}



public interface ISkillManager
{


    Skill GetSkill(SkillType skillType);

}




public class SkillManager : ISkillManager
{
    private SkillPack _skillPack;
    private readonly IItemManager _itemManager;


    SkillManager(SkillPack skillPack,IItemManager itemManager)
    {
        _itemManager = itemManager;
        _skillPack = skillPack;
    }



    public Skill GetSkill(SkillType skillType)
    {
        switch (_itemManager.GetCombatClass())
         {
             case CombatClass.OneHandShield:
                 return GetSkillBySkillType(_skillPack.swordShieldSkillSet, skillType);
 
             case CombatClass.TwoHandedSword:
                 return GetSkillBySkillType(_skillPack.twoHandedSwordSkillSet, skillType);
             
             case CombatClass.Archer:
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
