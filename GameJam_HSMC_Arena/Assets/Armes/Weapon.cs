using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Armes")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int mag;
    public float delay;
    public float reloadTime;
    public bool isMelee;

    public Effect effect;
}
