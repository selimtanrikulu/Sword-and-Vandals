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


}
