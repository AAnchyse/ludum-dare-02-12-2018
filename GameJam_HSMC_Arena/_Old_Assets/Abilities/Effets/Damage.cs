using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Effect
{
    public int amount;

    Damage()
    {
        onUnit = true;
        onPoint = false;
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        // target.takeDamage(source, target);
    }

    public override void ApplyEffect(GameObject source, Vector3 point)
    {

    }
}
