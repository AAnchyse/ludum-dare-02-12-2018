using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Effect/ApplyBehaviour")]
public class ApplyBehaviour : Effect
{
    public Behaviour behaviour;

    public override void ApplyEffect(GameObject source, Vector3 point)
    {
            
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        target.GetComponent<Unit>().addBehaviour(behaviour);
    }
}
