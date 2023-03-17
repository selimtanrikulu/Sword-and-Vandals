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

    [Inject]
    void Inject(IPrefabCreator prefabCreator,DiContainer diContainer)
    {
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
        //------


        //Create ai
        GameObject aiGameObject = _prefabCreator.CreatePrefab(PrefabType.Character);
        _diContainer.InstantiateComponent<AIController>(aiGameObject);
        playerGameObject.GetComponent<Animator>().runtimeAnimatorController = aiAoc;
        //--------
    }
}
