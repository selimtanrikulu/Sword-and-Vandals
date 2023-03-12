using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/SingleSkill")]
public class SingleSkill : Skill
{
    [SerializeField] public AnimationClip attackAnimation;
    [SerializeField] public GameObject skillImpact;
    
}
