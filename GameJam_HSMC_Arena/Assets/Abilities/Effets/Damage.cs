using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Effect/Damage")]
public class Damage : Effect
{
    public int amount;
    public bool useDamageMultiplier;

    public override void ApplyEffect(GameObject source, GameObject target)
    {
        if (source != null)
        {
            float mult = 1f;
            if(useDamageMultiplier)
            {
                CharacteristicsManager cm = source.GetComponent<CharacteristicsManager>();
                mult = (cm != null ? cm.weaponMultiplier : 1f);
            }
            target.GetComponent<Enemy>().TakeDamage(amount * mult);
        }
    }

    public override void ApplyEffect(GameObject source, Vector3 point)
    {

    }
}
