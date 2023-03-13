using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


[Serializable]
public struct CharacterWearings
{
    public GameObject weaponSocket;
}


public class CharacterWearer : MonoBehaviour
{

    [SerializeField] private CharacterWearings characterWearings;
    private IItemManager _itemManager;


    public GameObject weaponGameObject;

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
        weaponGameObject = Instantiate(_itemManager.GetWearedWeapon().weaponPrefab, characterWearings.weaponSocket.transform);
    }


}
