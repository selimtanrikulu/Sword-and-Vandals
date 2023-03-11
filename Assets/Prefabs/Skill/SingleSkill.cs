using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/SingleSkill")]
public class SingleSkill : Skill
{
    [SerializeField] public Motion attackAnimation;
    [SerializeField] public GameObject skillImpact;
    
    public override GameObject GetCurrentImpact()
    {
        return skillImpact;
    }

    public override void IncrementPhase()
    {
        //nothing;
    }
}
