using System;
using UnityEngine;
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

    public GameObject GetHitLocation(HeldItemHold heldItemHold)
    {
        switch (heldItemHold)
        {
            case HeldItemHold.Left:
                if (leftHandHeldItemGameObject)
                {
                    return leftHandHeldItemGameObject.transform.Find("HitLocation").gameObject;
                }
                Debug.LogError("Held Item not exists");
                break;
            
            case HeldItemHold.Right:
                if (rightHandHeldItemGameObject)
                {
                    return rightHandHeldItemGameObject.transform.Find("HitLocation").gameObject;
                }
                break;
        }

        return null;

    }

    public ParticleSystem GetWeaponTrail(HeldItemHold heldItemHold)
    {
        switch (heldItemHold)
        {
            case HeldItemHold.Left:
                if (leftHandHeldItemGameObject)
                {
                    return leftHandHeldItemGameObject.GetComponentInChildren<ParticleSystem>();
                }
                break;
            
            case HeldItemHold.Right:
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
        HeldItem leftHeldItem = _itemManager.GetWearedHeldItem(HeldItemHold.Left);
        HeldItem rightHeldItem = _itemManager.GetWearedHeldItem(HeldItemHold.Right);
        
        if (rightHeldItem != null)
        {
            rightHandHeldItemGameObject = Instantiate(rightHeldItem.heldItemPrefab, characterWearings.rightHandWeaponSocket.transform);
        }
        if( leftHeldItem != null)
        {
            leftHandHeldItemGameObject = Instantiate(leftHeldItem.heldItemPrefab, characterWearings.leftHandWeaponSocket.transform);
        }
        
    }


    
    
}
