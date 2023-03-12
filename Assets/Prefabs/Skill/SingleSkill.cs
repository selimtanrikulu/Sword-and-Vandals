using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/SingleSkill")]
public class SingleSkill : Skill
{
    [SerializeField] public Motion attackAnimation;
    [SerializeField] public GameObject skillImpact;
    
}
