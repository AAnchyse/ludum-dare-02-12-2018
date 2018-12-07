using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Effect/Projectile")]
public class SpawnProjectile : Effect
{
    public GameObject projectilePrefab;
    public float angle;
    public float v0;

    public override void ApplyEffect(GameObject source, Vector3 point)
    {
        Vector3 direction = point - source.transform.position;

        Vector3 spawnPoint = source.transform.position + 1.5f * direction.normalized + new Vector3(0, 3.5f, 0);
        if (source.GetComponent<WeaponManager>())
        {
            spawnPoint = source.GetComponent<WeaponManager>().gunPoint.transform.position;
        }
        GameObject proj = Instantiate(projectilePrefab, spawnPoint, source.transform.rotation);
        proj.GetComponent<LimitedLife>().spawnSource = source;

        // proj.name = "Projectile";
        float vxz = Mathf.Cos(angle * Mathf.Deg2Rad) * v0;
        float vy = Mathf.Sin(angle * Mathf.Deg2Rad) * v0;
        float theta = Mathf.Atan2(direction.z, direction.x);
        // Debug.Log(theta);
        float vx = Mathf.Cos(theta) * vxz;
        float vz = Mathf.Sin(theta) * vxz;
        proj.GetComponent<Rigidbody>().velocity = new Vector3(vx, vy, vz);
    }

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        ApplyEffect(source, target.transform.position);
    }
}
