using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DoubleSkill")]
public class DoubleSkill : Skill
{
    [SerializeField] public Motion attackAnimation1;
    [SerializeField] public GameObject skillImpact1;
    
    [SerializeField] public Motion attackAnimation2;
    [SerializeField] public GameObject skillImpact2;


    
    public override GameObject GetCurrentImpact()
    {
        if (phase == 1) return skillImpact1;
        if (phase == 2) return skillImpact2;
        Debug.LogError("Unknown phase");
        return null;
    }

    public override void IncrementPhase()
    {
        phase = phase == 1 ? 2 : 1;
    }
}
