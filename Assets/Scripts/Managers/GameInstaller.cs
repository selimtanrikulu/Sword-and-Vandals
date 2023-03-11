using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[Serializable]
public struct SkillPack
{
    public List<Skill> skills;
}

public class GameInstaller : MonoInstaller
{

    [SerializeField] private SkillPack skillPack;
    public override void InstallBindings()
    {
        Container.BindInstance(skillPack);
        
        
        Container.Bind<ITestManager>().To<TestManager>().AsSingle();
        Container.Bind<IItemManager>().To<ItemManager>().AsSingle();
        Container.Bind<ISkillManager>().To<SkillManager>().AsSingle();
    }
}