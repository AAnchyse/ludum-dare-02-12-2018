using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public string eName;

    public void Apply(GameObject source, GameObject target, Vector3 point)
    {
        if(target != null)
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
