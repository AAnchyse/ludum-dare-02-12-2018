using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Effect/SearchArea")]
public class SearchArea : Effect
{
    public float radius;
    [Range(0, 180)] public float arc = 180f;
    public Effect effet;
    public LayerMask layerMask;
    public bool ignoreSource = false;

    public override void ApplyEffect(GameObject source, Vector3 point)
    {
        Collider[] targets = Physics.OverlapSphere(point, radius, layerMask);
        Debug.Log("Nombre de cibles : " + targets.Length);
        foreach(Collider col in targets)
        {
            if(!(col.gameObject == source && ignoreSource))
            {
                float f = Vector3.Angle(source.transform.forward, col.transform.position - source.transform.position);
                // Debug.Log(f);
                // On vérifie si la cible est dans l'arc de cercle de la capacité
                if (Mathf.Abs(f) < arc)
                {
                    // C'est pas bon du tout comme code.
                    effet.ApplyEffect(source.gameObject, col.gameObject);
                }
            }
        }
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        ApplyEffect(source, target.transform.position);
    }
}