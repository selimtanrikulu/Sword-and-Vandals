using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DoubleSkill")]
public class DoubleSkill : Skill
{
    [SerializeField] public Motion attackAnimation1;
    [SerializeField] public GameObject skillImpact1;
    
    [SerializeField] public Motion attackAnimation2;
    [SerializeField] public GameObject skillImpact2;
    
}
