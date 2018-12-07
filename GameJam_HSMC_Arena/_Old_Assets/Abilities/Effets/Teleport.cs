using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/TeleportEffect")]
public class Teleport : Effect
{
    Teleport()
    {
        onPoint = false;
        onUnit = false;
    }

    public override void ApplyEffect(GameObject source, Vector3 point)
    {
        source.transform.position = point;
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    { }
}
