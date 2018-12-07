using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PlayerController pc;

    public Weapon[] allWeapons;
    Weapon currentWeapon;

    float weaponCooldown;

   	// Use this for initialization
	void Start ()
    {
        weaponCooldown = 0.0f;
        pc = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(weaponCooldown <= 0)
        { 
		    if(Input.GetAxis("Fire1") > 0.5)
            {
                FireWeapon(allWeapons[0]);
            }

            else if (Input.GetAxis("Fire2") > 0.5)
            {
                FireWeapon(allWeapons[1]);
            }
        }

        weaponCooldown -= Time.deltaTime;
    }

    public void FireWeapon(Weapon w)
    {
        // Si l'arme est une arme de melée
        if(w.isMelee)
        {
            Debug.Log("Bam");
            w.effect.Apply(gameObject, null, transform.position);
            weaponCooldown = w.delay;
        }
        else
        {
            w.effect.Apply(gameObject, null, pc.GetMousePosition());
            weaponCooldown = w.delay * pc.cooldownMultiplier;
        }
    }
}
