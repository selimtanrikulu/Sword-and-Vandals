using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameScene : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController playerAoc;
    [SerializeField] private AnimatorOverrideController aiAoc;

    private IPrefabCreator _prefabCreator;
    private DiContainer _diContainer;
    private IItemManager _itemManager;

    [Inject]
    void Inject(IPrefabCreator prefabCreator,DiContainer diContainer,IItemManager itemManager)
    {
        _itemManager = itemManager;
        _diContainer = diContainer;
        _prefabCreator = prefabCreator;
    }


    private void Start()
    {
        CreateCharacters();
    }

    private void CreateCharacters()
    {
        //Create player
        GameObject playerGameObject = _prefabCreator.CreatePrefab(PrefabType.Character);
        _diContainer.InstantiateComponent<PlayerControl>(playerGameObject);
        playerGameObject.GetComponent<Animator>().runtimeAnimatorController = playerAoc;
        //Wear player
        playerGameObject.GetComponent<CharacterWearer>().WearWearings(_itemManager.GetWearedHeldItem(HeldItemHold.Left),_itemManager.GetWearedHeldItem(HeldItemHold.Right));
        //----------
        
        

        //Create ai
        GameObject aiGameObject = _prefabCreator.CreatePrefab(PrefabType.Character);
        _diContainer.InstantiateComponent<AIController>(aiGameObject);
        playerGameObject.GetComponent<Animator>().runtimeAnimatorController = aiAoc;
        //Wear ai
        aiGameObject.GetComponent<CharacterWearer>().WearWearings(_itemManager.GetItemByIndex(0),null);
        //---------
        
    }
}
