using UnityEngine;



public abstract class Skill : ScriptableObject
{

    public int phase = 1;
    public abstract GameObject GetCurrentImpact();
    public abstract void IncrementPhase();

}
