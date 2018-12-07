using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    PlayerController pc;
    CharacteristicsManager cm;

    public AudioSource ads;
    public AudioClip adcCac;
    public AudioClip adcDist;
    private Animator animator;

    public Weapon[] allWeapons;
    Weapon currentWeapon;

    float weaponCooldown;
    int currentAmmo;
    public Transform gunPoint;

   	// Use this for initialization
	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        weaponCooldown = 0.0f;
        pc = GetComponent<PlayerController>();
        cm = GetComponent<CharacteristicsManager>();
        currentAmmo = allWeapons[1].mag;
    }
    int compteur = 0;
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (weaponCooldown <= 0)
        { 
		    if(Input.GetAxis("Fire1") > 0.5)
            {
                Debug.Log("Fire1");
                FireWeapon(allWeapons[0]);
                if (cm.currentHealth > 0)
                {
                    ads.clip = adcCac;
                    ads.Play();
                }
            }

            else if (Input.GetAxis("Fire2") > 0.5)
            {
                if(currentAmmo > 0)
                {
                    if (cm.currentHealth > 0)
                    {
                        ads.clip = adcDist;
                        ads.Play();
                    }
                    compteur = 0;
                    animator.SetBool("Shooting", true);
                    FireWeapon(allWeapons[1]);
                }
                else
                {
                    // PlaySound empty clip
                    Reload();
                }
            }
            compteur++;
            if (compteur >= 25)
            {
                animator.SetBool("Shooting", false);
            }
        }

        weaponCooldown -= Time.deltaTime;
    }

    public void FireWeapon(Weapon w)
    {
        // Si l'arme est une arme de melée
        if(w.isMelee)
        {
            w.effect.Apply(gameObject, null, transform.position);
            weaponCooldown = w.delay;
        }
        else
        {
            w.effect.Apply(gameObject, null, pc.GetMousePosition());
            weaponCooldown = w.delay * cm.delayMultiplier;
            currentAmmo--;
        }
    }

    public void Reload()
    {
        currentAmmo = allWeapons[1].mag;
    }
}
