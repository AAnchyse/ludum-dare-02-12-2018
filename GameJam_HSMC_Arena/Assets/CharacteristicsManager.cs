using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsManager : MonoBehaviour
{
    // Les 5 Caractéristiques de base du joueur
    public float b_moveSpeed;
    public float b_maxHealth;
    public float b_weaponDelay;
    public float b_WeaponMultiplicateur;
    public float b_KnockoutRange;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public bool invulnerable;
    [HideInInspector] public bool onRecovery;

    [HideInInspector] public float currentKnockbackRange;
    [HideInInspector] public float weaponMultiplier;
    [HideInInspector] public float delayMultiplier;
    [HideInInspector] public float delayMultiplierNoBehaviour;

    [HideInInspector] public float moveSpeed;

    public Animator an;

    public float healthRecuperationDelay;
    public float healthRegen;
    public float regenerationTick;

    public float recoveryTime;
    float recoveryDelay;

    public Text gameOver;

    float regenerationDelay;
    float healthTimer;

    private AudioSource ads;
    public AudioClip deathSound;
    public AudioClip woundedSound;
    public AudioClip woundedSound2;
    int compteur = 0;


    bool alive = true;

    public Image img;

    // Use this for initialization
    void Start ()
    {
        ads = GetComponent<AudioSource>();
        maxHealth = b_maxHealth;
        currentHealth = maxHealth;
        moveSpeed = b_moveSpeed;
        weaponMultiplier = b_WeaponMultiplicateur;
        currentKnockbackRange = b_KnockoutRange;
        delayMultiplier = b_weaponDelay;
        delayMultiplierNoBehaviour = b_weaponDelay;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Gestion de la régénération des points de vies
        if(currentHealth < maxHealth && healthRegen > 0)
        { 
            healthTimer += Time.deltaTime;
            if(healthTimer > healthRecuperationDelay)
            {
                regenerationDelay += Time.deltaTime;
                if (regenerationDelay > regenerationTick)
                {
                    currentHealth = Mathf.Clamp(currentHealth + healthRegen, 0f, maxHealth);
                    regenerationDelay = 0.0f;
                }
                img.color = new Color(1f, 0f, 0f, (1 - (currentHealth / maxHealth))  / 2);
            }
        }

        // Gestion du recovery time
        if(onRecovery && recoveryDelay < recoveryTime)
        {
            recoveryDelay += Time.deltaTime;
            if(recoveryDelay >= recoveryTime)
            {
                recoveryDelay = 0.0f;
                onRecovery = false;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        // Si le joueur est insensible aux dégats les ignore.
        if(invulnerable || onRecovery)
        {
            return;
        }

        healthTimer = 0;
        regenerationDelay = 0;
        currentHealth -= amount;
        onRecovery = true;

        img.color = new Color(1f, 0f, 0f, 1 - (currentHealth / maxHealth));

        if (currentHealth <= 0 )
        {
            if (alive)
            {
                Die();
            }
        }
        else
        {
            compteur++;
            if (compteur % 2 == 0)
            {
                ads.clip = woundedSound;
                ads.Play();
            }
            else
            {
                ads.clip = woundedSound2;
                ads.Play();

            }
        }
    }

    public void Die()
    {
        an.SetBool("Death", true);
        an.Play("Dead");
        ads.clip = deathSound;
        ads.volume = 1;
        ads.Play();
        alive = false;
        Destroy(gameObject, 4.2f);
        gameOver.gameObject.SetActive(true);
    }

    public float GetStatRatio(int i)
    {
        if(i == 4)
        {
            return Mathf.Clamp01(moveSpeed / b_moveSpeed);
        }
        if(i == 3)
        {
            return Mathf.Clamp01(currentKnockbackRange / b_KnockoutRange);
        }
        else if(i == 2)
        {
            return Mathf.Clamp01(maxHealth / b_maxHealth);
        }
        else if(i == 1)
        {
            return Mathf.Clamp01(b_weaponDelay / delayMultiplierNoBehaviour);
        }
        return Mathf.Clamp01(weaponMultiplier / b_WeaponMultiplicateur);
    }
}
