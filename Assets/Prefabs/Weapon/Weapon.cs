using UnityEngine;


public enum WeaponType
{
   OneHandedSword,
   TwoHandedSword,
}

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{ 
   [SerializeField] public WeaponType weaponType;
   [SerializeField] public GameObject weaponPrefab;
   [SerializeField] public float damage;
}
