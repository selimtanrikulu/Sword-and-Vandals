
using System;
using UnityEngine;

[Serializable]
public enum WeaponHold
{
    Right,
    Left,
}


public enum CombatClass
{
    OneHandShield,
    TwoHandedSword,
    Archer,
    DualWield,
    BattleMage,
    Cleric,
    
    Crossbow,
    Mage,
    
    
    
    
    
    
    
    
    
    Error
}


public interface IItemManager
{
    HeldItem GetWearedLeftHandWeapon();
    HeldItem GetWearedRightHandWeapon();
    
    void SetWearedLeftHandItem(HeldItem heldItem);
    void SetWearedRightHandItem(HeldItem heldItem);

    CombatClass GetCombatClass();
}

public class ItemManager : IItemManager
{
    private readonly HeldItemPack _heldItemPack;

    private HeldItem _wearedLeftHandHeldItem;
    private HeldItem _wearedRightHandHeldItem;

    ItemManager(HeldItemPack heldItemPack)
    {
        _heldItemPack = heldItemPack;
        //SetWearedLeftHandItem(heldItemPack.heldItems[0]);
        SetWearedRightHandItem(heldItemPack.heldItems[3]);
    }


    public HeldItem GetWearedLeftHandWeapon()
    {
        return _wearedLeftHandHeldItem;
    }

    public HeldItem GetWearedRightHandWeapon()
    {
        return _wearedRightHandHeldItem;
    }

    public void SetWearedLeftHandItem(HeldItem heldItem)
    {
        if (heldItem is OneHandWeapon || heldItem is Bow ||heldItem is OffHand)
        {
            _wearedLeftHandHeldItem = heldItem;
        }
        else
        {
            Debug.LogError("Cannot hold weapon");
        }
    }

    public void SetWearedRightHandItem(HeldItem heldItem)
    {

        if (heldItem is OneHandWeapon || heldItem is TwoHandSword)
        {
            _wearedRightHandHeldItem = heldItem;
        }
        else
        {
            Debug.LogError("Cannot hold weapon");
        }
        
        
    }

    public CombatClass GetCombatClass()
    {
        if (_wearedRightHandHeldItem is OneHandWeapon)
        {
            if (_wearedLeftHandHeldItem is OneHandWeapon)
            {
                return CombatClass.DualWield;
            }
            if (_wearedLeftHandHeldItem == null || _wearedLeftHandHeldItem is Shield)
            {
                return CombatClass.OneHandShield;
            }

            if (_wearedLeftHandHeldItem is SpellBook)
            {
                return CombatClass.BattleMage;
            }

            if (_wearedLeftHandHeldItem is HolySymbol)
            {
                return CombatClass.Cleric;
            }

            return CombatClass.Error;
        }
        
        if (_wearedRightHandHeldItem is null)
        {
            if (_wearedLeftHandHeldItem is Bow)
            {
                return CombatClass.Archer;
            }
            return CombatClass.Error;
        }

        if (_wearedRightHandHeldItem is TwoHandSword)
        {
            if (_wearedLeftHandHeldItem == null)
            {
                return CombatClass.TwoHandedSword;
            }

            return CombatClass.Error;
        }

        return CombatClass.Error;

    }
}
