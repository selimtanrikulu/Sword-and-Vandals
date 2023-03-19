using UnityEngine;


public enum ElementalType
{
    Fire,
    Ice,
    Shock,
    Poison,
    Void,
    Arcane,
    None
}


public abstract class Weapon : HeldItem
{
    [SerializeField] public float baseDamage;
    [SerializeField] public ElementalType elementalType;
}
