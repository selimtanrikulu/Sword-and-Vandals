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


    [HideInInspector]public GameObject rightHandHeldItemGameObject;
    [HideInInspector]public GameObject leftHandHeldItemGameObject;

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

    public GameObject GetHitLocation(WeaponHold weaponHold)
    {
        switch (weaponHold)
        {
            case WeaponHold.Left:
                if (leftHandHeldItemGameObject)
                {
                    return leftHandHeldItemGameObject.transform.Find("HitLocation").gameObject;
                }
                Debug.LogError("Held Item not exists");
                break;
            
            case WeaponHold.Right:
                if (rightHandHeldItemGameObject)
                {
                    return rightHandHeldItemGameObject.transform.Find("HitLocation").gameObject;
                }
                break;
        }

        return null;

    }

    public ParticleSystem GetWeaponTrail(WeaponHold weaponHold)
    {
        switch (weaponHold)
        {
            case WeaponHold.Left:
                if (leftHandHeldItemGameObject)
                {
                    return leftHandHeldItemGameObject.GetComponentInChildren<ParticleSystem>();
                }
                break;
            
            case WeaponHold.Right:
                if (rightHandHeldItemGameObject)
                {
                    return rightHandHeldItemGameObject.GetComponentInChildren<ParticleSystem>();
                }
                break;
        }

        return null;
    }

    private void WearWearings()
    {
        HeldItem leftHeldItem = _itemManager.GetWearedLeftHandWeapon();
        HeldItem rightHeldItem = _itemManager.GetWearedRightHandWeapon();
        
        if (rightHeldItem != null)
        {
            rightHandHeldItemGameObject = Instantiate(_itemManager.GetWearedRightHandWeapon().heldItemPrefab, characterWearings.rightHandWeaponSocket.transform);
        }
        if( leftHeldItem != null)
        {
            leftHandHeldItemGameObject = Instantiate(_itemManager.GetWearedLeftHandWeapon().heldItemPrefab, characterWearings.leftHandWeaponSocket.transform);
        }
        
    }


    
    
}
