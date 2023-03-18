using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;



public abstract class Weapon : HeldItem
{
    [SerializeField] public float baseDamage;
    
}
