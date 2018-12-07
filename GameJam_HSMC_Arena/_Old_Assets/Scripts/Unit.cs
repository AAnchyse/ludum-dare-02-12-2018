using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float b_moveSpeed;
    public float maxHealth;
    
    // Principalement utilisée pour le joueur
    public Ability[] abilities;
    protected float[] cooldownTimer;

    protected Dictionary<Behaviour, int> allBehaviours;

    protected List<BehaviourController> timedBehaviours;
    protected List<BehaviourController> expired;
    protected float moveSpeed;
    public float currentHealth;
    public bool invulnerable;
    float cooldownMultiplier = 1.0f;


    GameObject target = null;
    Rigidbody body;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        foreach (BehaviourController bc in timedBehaviours)
        {
            bc.decreaseTimer(Time.deltaTime);
            if (bc.Expired())
            {
                expired.Add(bc);
            }
        }
        foreach (BehaviourController bc in expired)
        {
            timedBehaviours.Remove(bc);
            RemoveBehaviour(bc.b);
        }
        expired.Clear();
    }

    public void addBehaviour(Behaviour b)
    {
        // On vérifie que le Comportement n'est pas supérieur a sa limite d'empilement.

        // Si le modificateur a une durée limitée dans le temps on ajoute un Gestionnaire de temps
        if (b.timed)
        {
            timedBehaviours.Add(new BehaviourController(b));
        }
        // On ajoute tous les flags à ajouter à l'unité
        if (b.giveInvulnerability)
        {
            invulnerable = true;
        }

        cooldownMultiplier *= b.fireRateMultiplier;
    }

    public void RemoveBehaviour(Behaviour b)
    {
        if (b.giveInvulnerability)
        {
            invulnerable = false;
        }
        cooldownMultiplier /= b.fireRateMultiplier;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}