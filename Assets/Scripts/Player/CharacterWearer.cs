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


    public HeldItem rightHeldItem;
    public HeldItem leftHeldItem;

    [Inject]
    void Inject(IItemManager itemManager)
    {
        _itemManager = itemManager;
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

    public void WearWearings(HeldItem left,HeldItem right)
    {
        leftHeldItem = left;
        rightHeldItem = right;
        
        if (right != null)
        {
            rightHandHeldItemGameObject = Instantiate(right.heldItemPrefab, characterWearings.rightHandWeaponSocket.transform);
        }
        if( left != null)
        {
            leftHandHeldItemGameObject = Instantiate(left.heldItemPrefab, characterWearings.leftHandWeaponSocket.transform);
        }
    }


    
    
}
