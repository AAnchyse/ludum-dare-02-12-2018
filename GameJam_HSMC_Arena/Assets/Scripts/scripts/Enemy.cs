using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float damage = 10;
    public float vitesse = 5;
    public float delayBtwAttack = 1;
    public float rotationSpeed = 3;
    public AudioClip deathSound;
    public AudioClip woundedSound;
    public AudioClip tronconneuse;
    public AudioSource tronconneuseSource;

    public Animator animator;

    private AudioSource ads;

    public WaveGenerator gameManager;

    CharacteristicsManager playerStats;

    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent agt;
    float timer;
    bool playerInRange;
    Vector3 lastPosition;
    public float range = 1;
    float rangeSqr;

    bool sink = false;
    float sinkspeed = -1.2f;
    float timerBeforeMoving;

    // Use this for initialization
    void Start () {
        timerBeforeMoving = delayBtwAttack;
        tronconneuseSource.clip = tronconneuse;
        tronconneuseSource.loop = true;
        tronconneuseSource.Play();
        playerStats = FindObjectOfType<CharacteristicsManager>();
        gameManager = FindObjectOfType<WaveGenerator>();
        if (playerStats)
        {
            playerTransform = playerStats.transform;
        }
        agt = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agt.speed = vitesse;
        agt.stoppingDistance = range;
        rangeSqr = range * range;
        ads = GetComponent<AudioSource>();
	}

    bool startPlayDeath = false;
    // Update is called once per frame
    void Update ()
    {
        if(sink)
        {
            if (!startPlayDeath)
            {
                animator.SetBool("Dead", true);
                ads.clip = deathSound;
                ads.Play();
                startPlayDeath = true;
            }
            return;
        }
        timer += Time.deltaTime;
        timerBeforeMoving += Time.deltaTime;
        if (playerStats)
        {
            if (health > 0 && playerStats.currentHealth > 0)
            {
                if ((playerTransform.position - transform.position).sqrMagnitude < rangeSqr)
                {
                    playerInRange = true;
                }
                else
                {
                    playerInRange = false;
                }
                if (timer > delayBtwAttack && playerInRange)
                {
                    Attack();
                    timer = 0;
                    timerBeforeMoving = 0;
                }
                if (!playerInRange && timerBeforeMoving>delayBtwAttack)
                {
                    animator.SetBool("Attacking", false);
                    Vector3 newPosition = new Vector3(playerTransform.position.x, 1, playerTransform.position.z);
                    agt.SetDestination(newPosition);
                }
            }
        }
        
	}

    void Attack() {
        animator.SetBool("Attacking", true);
        playerStats.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= (damage);
        if(health <= 0)
        {
            Destroy(gameObject, 3f);
            sink = true;
            GetComponent<CapsuleCollider>().enabled = false;
            agt.enabled = false;
            gameManager.OnMinionDeath(this);
        }
        else
        {

            ads.clip = woundedSound;
            ads.Play();
        }
    }
}
