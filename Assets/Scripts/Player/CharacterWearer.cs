using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


[Serializable]
public struct CharacterWearings
{
    public GameObject rightHandWeaponSocket;
    public GameObject leftHandWeaponSocket;
}


public class CharacterWearer : MonoBehaviour
{

    [SerializeField] private CharacterWearings characterWearings;
    private IItemManager _itemManager;


    [HideInInspector]public GameObject rightHandWeaponGameObject;
    [HideInInspector]public GameObject leftHandWeaponGameObject;

    [Inject]
    void Inject(IItemManager itemManager)
    {
        _itemManager = itemManager;
    }


    void Awake()
    {
        WearWearings();
    }

    void Start()
    {
        
    }

    private void WearWearings()
    {
        Weapon weapon = _itemManager.GetWearedWeapon();

        if (weapon.holdHand == HoldHand.Right)
        {
            rightHandWeaponGameObject = Instantiate(_itemManager.GetWearedWeapon().weaponPrefab, characterWearings.rightHandWeaponSocket.transform);
        }
        else
        {
            leftHandWeaponGameObject = Instantiate(_itemManager.GetWearedWeapon().weaponPrefab, characterWearings.leftHandWeaponSocket.transform);
        }
        
    }


}
