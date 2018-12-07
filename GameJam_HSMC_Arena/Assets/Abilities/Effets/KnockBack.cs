using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/KnockBack")]
public class KnockBack : Effect
{
    public float force;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        float mult = 1;
        if(source.GetComponent<CharacteristicsManager>())
        {
            mult = source.GetComponent<CharacteristicsManager>().currentKnockbackRange;
        }
        Debug.Log("Application d'une force sur l'objet " + target.name);
        Vector3 dpos = (target.transform.position - source.transform.position) * force * mult;
        dpos.y = 0;
        target.GetComponent<Rigidbody>().AddForce(dpos, ForceMode.Impulse);
        /*  = (target.transform.position - source.transform.position).normalized * force;
        Debug.Log(dpos);
        target.transform.Translate(dpos);*/
    }

    public override void ApplyEffect(GameObject source, Vector3 point)
    { }
}
