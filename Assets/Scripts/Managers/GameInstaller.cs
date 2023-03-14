using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


[Serializable]
public struct SkillPack
{
    public SkillSet swordShieldSkillSet;
    public SkillSet twoHandedSwordSkillSet;
    public SkillSet archerSkillSet;
}


[Serializable]
public struct SkillSet
{
    public Skill basicAttack;
    public Skill skill1;
    public Skill skill2;
}


[Serializable]
public struct HeldItemPack
{
    public List<HeldItem> heldItems;
}

public class GameInstaller : MonoInstaller
{

    [SerializeField] private SkillPack skillPack;
    [SerializeField] private HeldItemPack heldItemPack;
    public override void InstallBindings()
    {
        Container.BindInstance(skillPack);
        Container.BindInstance(heldItemPack);
        
        
        Container.Bind<ITestManager>().To<TestManager>().AsSingle();
        Container.Bind<IItemManager>().To<ItemManager>().AsSingle();
        Container.Bind<ISkillManager>().To<SkillManager>().AsSingle();
    }
}