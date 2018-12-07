using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    PlayerController pc;
    CharacteristicsManager cm;
    // Principalement utilisée pour le joueur
    public Ability[] allAbilities;
    protected float[] cooldownTimer;
    [HideInInspector] public Ability[] abilities;
    public Transform bubulle;

    private Animator animator;
    public AudioSource ads;
    int state = 0;

    GameObject target = null;
    public UI abilityUI;

    // Use this for initialization
    void Start ()
    {
        Debug.Log(Application.systemLanguage);
        abilities = new Ability[5];
   //     if (Application.systemLanguage == SystemLanguage.French)
   //     {
            abilities[0] = allAbilities[0];
            abilities[1] = allAbilities[2];
            abilities[2] = allAbilities[3];
            abilities[3] = allAbilities[4];
            abilities[4] = allAbilities[5];
   //     }
     /*   else
        {
            abilities[0] = allAbilities[1];
            abilities[1] = allAbilities[2];
            abilities[2] = allAbilities[3];
            abilities[3] = allAbilities[4];
            abilities[4] = allAbilities[5];
        }*/
        animator = GetComponentInChildren<Animator>();
        pc = GetComponent<PlayerController>();
        cm = GetComponent<CharacteristicsManager>();

        cooldownTimer = new float[abilities.Length];
        for (int i = 0; i < cooldownTimer.Length; i++)
        {
            cooldownTimer[i] = 0.0f;
        }
    }
    float timer = 0;
    float timerBubulle = 0;
	// Update is called once per frame
	void Update ()
    {
        /* Checks for ability input */
        for (int i = 0; i < abilities.Length; i++)
        {
            Ability currentAbility = abilities[i];
            // TODO: Faire en sorte de ne pouvoir utiliser qu'une seule capacité à la fois.
            if (Input.GetKeyDown(currentAbility.hotkey))
            {
                // Si le cooldown de la capacité est terminé
                if (cooldownTimer[i] <= 0)
                {
                    if(currentAbility.hotkey == KeyCode.Alpha2)
                    {
                        timer = 0;
                        animator.SetBool("Furying", true);
                    }
                    else if (currentAbility.hotkey == KeyCode.R)
                    {
                        Debug.Log("shield up");
                        animationBubulle(true);
                    }
                    // On paie le prix de la capacité
                    cm.moveSpeed *= currentAbility.speedMultiplier;
                    cm.maxHealth *= currentAbility.healthMultiplier;
                    cm.currentHealth *= currentAbility.healthMultiplier;
                    cm.delayMultiplier *= currentAbility.firerateMultiplier;
                    cm.delayMultiplierNoBehaviour *= currentAbility.firerateMultiplier;
                    cm.currentKnockbackRange *= currentAbility.knockbackStrenghtMultiplier;
                    cm.weaponMultiplier *= currentAbility.damageMultiplier;
                    ads.clip = currentAbility.adc;
                    ads.Play();

                    if (abilities[i].castOnSelf)
                    {
                        foreach (Effect e in abilities[i].effects)
                        {
                            e.Apply(gameObject, gameObject, transform.position);
                        }
                    }

                    else
                    {
                        foreach (Effect e in abilities[i].effects)
                        {

                            e.Apply(gameObject, target, pc.GetMousePosition());
                        }
                    }
                    cooldownTimer[i] = abilities[i].aCooldown;
                    abilityUI.LockAbility(i);
                    abilityUI.ChangeRatio(i, cm.GetStatRatio(i));
                }
            }
            timerBubulle += Time.deltaTime;
            animationBubulle(false);
        }

        // Mise à jour des cooldowns
        for (int i = 0; i < cooldownTimer.Length; i++)
        {
            if(cooldownTimer[i] >= 0)
            { 
                cooldownTimer[i] -= Time.deltaTime;
                if (cooldownTimer[i] <= 0)
                {
                    abilityUI.UnlockAbility(i);
                }
            }

        }
        timer += Time.deltaTime;
        if (timer >= 8)
        {
            animator.SetBool("Furying", false);
        }
    }

    public float getCooldownRate(int i)
    {
        return Mathf.Clamp01(cooldownTimer[i] / abilities[i].aCooldown);
    }

    public void animationBubulle(bool justActivated)
    {
   /*     Debug.Log(timerBubulle);
        // On active le bouclier avec le bouton droit
        if ( state == 0 && justActivated)
        {
            timerBubulle = 0;
            Debug.Log(state);
            state = -1;
        }

        // Le bouclier grandit
        if (state == -1)
        {
            bubulle.localScale += new Vector3(0.025f, 0.025f, 0.025f);
            if (bubulle.localScale.x >= 2)
            {
                state = -2;
            }
        }

        if (!cm.invulnerable)
        {
            state = -3;
        }

        // Le bouclier rapetisse
        if (state == -3)
        {
            bubulle.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (bubulle.localScale.x <= 0)
                state = 0;
        }
        */
    }
}
