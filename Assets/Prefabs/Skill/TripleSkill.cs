using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/TripleSkill")]
public class TripleSkill : Skill
{
    [SerializeField] public Motion attackAnimation1;
    [SerializeField] public GameObject skillImpact1;
    
    [SerializeField] public Motion attackAnimation2;
    [SerializeField] public GameObject skillImpact2;
    
    [SerializeField] public Motion attackAnimation3;
    [SerializeField] public GameObject skillImpact3;
    
    
    public override GameObject GetCurrentImpact()
    {
        if (phase == 1) return skillImpact1;
        if (phase == 2) return skillImpact2;
        if (phase == 3) return skillImpact3;
        Debug.LogError("Unknown phase");
        return null;
    }

    public override void IncrementPhase()
    {
        if (phase == 1) phase = 2;
        else if (phase == 2) phase = 3;
        else if (phase == 3) phase = 1;
        else
        {
            Debug.LogError("Unknown phase");
        }
    }
}
