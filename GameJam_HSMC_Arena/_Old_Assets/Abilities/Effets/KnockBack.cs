using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/KnockBack")]
public class KnockBack : Effect
{
    KnockBack()
    {
        onPoint = false;
        onUnit = true;
    }

    public float force;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        Debug.Log("Application d'une force sur l'objet " + target.name);
        Vector3 dpos = (target.transform.position - source.transform.position) * force;
        dpos.y = 0;
        target.GetComponent<Rigidbody>().AddForce(dpos, ForceMode.Impulse);
        /*  = (target.transform.position - source.transform.position).normalized * force;
        Debug.Log(dpos);
        target.transform.Translate(dpos);*/
    }

    public override void ApplyEffect(GameObject source, Vector3 point)
    { }
}
