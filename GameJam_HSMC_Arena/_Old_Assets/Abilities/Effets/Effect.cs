using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    string eName;
    protected bool onUnit;
    protected bool onPoint;

    public void Apply(GameObject source, GameObject target, Vector3 point)
    {
        if(target != null && onUnit)
        {
            ApplyEffect(source, target);
        }
        else
        {
            ApplyEffect(source, point);
        }
    }

    public abstract void ApplyEffect(GameObject source, Vector3 point);
    public abstract void ApplyEffect(GameObject source, GameObject target);
}
