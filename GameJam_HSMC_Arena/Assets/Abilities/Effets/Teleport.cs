using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/TeleportEffect")]
public class Teleport : Effect
{
    public float duree;

    public override void ApplyEffect(GameObject source, Vector3 point)
    {
        source.transform.position = point;
        if (source.transform.position.magnitude > 43)
        {
            Vector3 newPos = new Vector3(source.transform.position.x, 0, source.transform.position.y);
            newPos.Normalize();
            newPos *= 43;
            newPos.y = 2;
            source.transform.position = newPos;
        }
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {

    }
}
