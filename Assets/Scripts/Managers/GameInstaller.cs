using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[Serializable]
public struct SkillPack
{
    public SkillSet swordShieldSkillSet;
    public SkillSet twoHandedSwordSkillSet;
}


[Serializable]
public struct SkillSet
{
    public Skill basicAttack;
    public Skill skill1;
    public Skill skill2;
}


[Serializable]
public struct WeaponPack
{
    public List<Weapon> weapons;
}

public class GameInstaller : MonoInstaller
{

    [SerializeField] private SkillPack skillPack;
    [SerializeField] private WeaponPack weaponPack;
    public override void InstallBindings()
    {
        Container.BindInstance(skillPack);
        Container.BindInstance(weaponPack);
        
        
        Container.Bind<ITestManager>().To<TestManager>().AsSingle();
        Container.Bind<IItemManager>().To<ItemManager>().AsSingle();
        Container.Bind<ISkillManager>().To<SkillManager>().AsSingle();
    }
}