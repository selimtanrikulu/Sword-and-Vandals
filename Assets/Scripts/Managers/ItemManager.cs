
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum HeldItemHold
{
    Right,
    Left,
}


public enum CombatClass
{
    Warrior,
    Berserker,
    BattleMage,
    BattlePriest,
    Duelist,
    Assassin,
    Crossbowman,
    Archer,
    Mage,
    Priest,
    Error
}


public interface IItemManager
{
    void SetWearedHeldItem(HeldItem heldItem, HeldItemHold heldItemHold);

    void ResetWearedHeldItem(HeldItemHold heldItemHold);
    
    HeldItem GetWearedHeldItem(HeldItemHold heldItemHold);
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
        SetWearedHeldItem(_heldItemPack.heldItems[0],HeldItemHold.Right);
    }

    public void SetWearedHeldItem(HeldItem heldItem, HeldItemHold heldItemHold)
    {
        switch (heldItemHold)
        {
            case HeldItemHold.Left:
                _wearedLeftHandHeldItem = heldItem;
                break;
            
            
            case HeldItemHold.Right:
                _wearedRightHandHeldItem = heldItem;
                break;
        }
    }

    public void ResetWearedHeldItem(HeldItemHold heldItemHold)
    {
        switch (heldItemHold)
        {
            case HeldItemHold.Left:
                _wearedLeftHandHeldItem = null;
                break;
            
            case HeldItemHold.Right:
                _wearedRightHandHeldItem = null;
                break;
            
        }
    }

    public HeldItem GetWearedHeldItem(HeldItemHold heldItemHold)
    {
        switch (heldItemHold)
        {
            case HeldItemHold.Left:
                return _wearedLeftHandHeldItem;

            case HeldItemHold.Right:
                return _wearedRightHandHeldItem;
            
        }

        
        Debug.LogError("Unknow HeldItemHold");
        return null;
    }

    public CombatClass GetCombatClass()
    {
        if (_wearedLeftHandHeldItem is OneHanded && _wearedRightHandHeldItem is OneHanded) return CombatClass.Duelist;
        if (_wearedLeftHandHeldItem is null && _wearedRightHandHeldItem is TwoHanded) return CombatClass.Berserker;
        if (_wearedLeftHandHeldItem is Shield or null && _wearedRightHandHeldItem is OneHanded) return CombatClass.Warrior;
        if (_wearedLeftHandHeldItem is Dagger && _wearedRightHandHeldItem is Dagger) return CombatClass.Assassin;
        if (_wearedLeftHandHeldItem is Crossbow && _wearedRightHandHeldItem is null) return CombatClass.Crossbowman;
        if (_wearedLeftHandHeldItem is SpellBook && _wearedRightHandHeldItem is OneHanded) return CombatClass.BattleMage;
        if (_wearedLeftHandHeldItem is HolySymbol && _wearedRightHandHeldItem is OneHanded) return CombatClass.Priest;
        if (_wearedLeftHandHeldItem is Bow && _wearedRightHandHeldItem is null) return CombatClass.Archer;
        if (_wearedLeftHandHeldItem is Wand && _wearedRightHandHeldItem is SpellBook or null) return CombatClass.Mage;
        if (_wearedLeftHandHeldItem is Wand && _wearedRightHandHeldItem is HolySymbol) return CombatClass.Priest;
      
        return CombatClass.Error;

    }
}
