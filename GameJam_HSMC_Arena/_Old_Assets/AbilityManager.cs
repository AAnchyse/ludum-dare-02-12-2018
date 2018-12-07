using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    PlayerController pc;

    // Principalement utilisée pour le joueur
    public Ability[] abilities;
    protected float[] cooldownTimer;

    GameObject target = null;

    // Use this for initialization
    void Start ()
    {
        pc = GetComponent<PlayerController>();

        cooldownTimer = new float[abilities.Length];
        for (int i = 0; i < cooldownTimer.Length; i++)
        {
            cooldownTimer[i] = 0.0f;
        }
    }
	
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
                if (cooldownTimer[i] < 0)
                {
                    // On applique tous les effets de la capacité.
                    foreach (Effect e in abilities[i].effects)
                    {
                        if(abilities[i].castOnSelf)
                        {
                            e.Apply(gameObject, gameObject, transform.position);
                        }
                        else
                        {
                            e.Apply(gameObject, target, pc.GetMousePosition());
                        }
                    }
                    cooldownTimer[i] = abilities[i].aCooldown;
                }
            }
        }

        // Mise à jour des cooldowns
        for (int i = 0; i < cooldownTimer.Length; i++)
        {
            cooldownTimer[i] -= Time.deltaTime;
        }
    }

    public float getCooldownRate(int i)
    {
        return Mathf.Clamp01(cooldownTimer[i] / abilities[i].aCooldown);
    }
}
