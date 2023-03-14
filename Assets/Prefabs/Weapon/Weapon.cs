using UnityEngine;


public enum WeaponType
{
   OneHandedSword,
   TwoHandedSword,
   Bow,
}

public enum HoldHand
{
   Left,
   Right,
   
}

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{ 
   [SerializeField] public WeaponType weaponType;
   [SerializeField] public HoldHand holdHand;
   [SerializeField] public GameObject weaponPrefab;
   [SerializeField] public float damage;
}
