using UnityEngine;

public interface ISkillManager
{
    Skill GetRandomSkill();

    Skill GetSkillByIndex(int index);

}


public class SkillManager : ISkillManager
{
    private SkillPack _skillPack;
    
    
    SkillManager(SkillPack skillPack)
    {
        _skillPack = skillPack;
    }


    public Skill GetRandomSkill()
    {
        int rand = Random.Range(0, _skillPack.skills.Count);
        return _skillPack.skills[rand];
    }

    public Skill GetSkillByIndex(int index)
    {
        if (index > _skillPack.skills.Count - 1)
        {
            Debug.LogError("Out of index");
            return null;
        }
        return _skillPack.skills[index];
    }
}
