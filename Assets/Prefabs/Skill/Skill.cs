using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{ 
    [SerializeField] public Motion attackAnimation;
    [SerializeField] public GameObject skillImpact;
    [SerializeField] public Vector3 startOffset;
    
}
