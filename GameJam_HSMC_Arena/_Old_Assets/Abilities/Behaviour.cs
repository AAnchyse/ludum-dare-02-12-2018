using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour")]
public class Behaviour : ScriptableObject
{
    public Effect activation;
    public Effect desactivation;
    public int maxStack;
    public float duree;
    public bool timed;

    public float fireRateMultiplier;
    public bool giveInvulnerability;
}
