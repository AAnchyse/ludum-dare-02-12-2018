using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask floorMask;

    float moveSpeed;
    float y_ref;

    Vector3 delta = Vector3.zero;
    public Vector3 mousePosition;
    Ray ray;       
    RaycastHit floorHit;
    Rigidbody playerRigidbody;

    public float b_moveSpeed;
    public float maxHealth;

    protected Dictionary<Behaviour, int> allBehaviours;

    protected List<BehaviourController> timedBehaviours;
    protected List<BehaviourController> expired;
    public float currentHealth;
    public bool invulnerable;
    public float cooldownMultiplier = 1.0f;

    Rigidbody body;

    // Use this for initialization
    void Start ()
    {
        playerRigidbody = GetComponentInChildren<Rigidbody>();
        moveSpeed = b_moveSpeed;
        y_ref = transform.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CalculateMousePosition();
        Move();
        LookMousePositon();
	}

    private void Move()
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");

        delta.x = dx;
        delta.z = dz;

        if(dx*dx + dz*dz > 1)
        {
            delta.Normalize();
        }

        transform.position += delta * moveSpeed * Time.deltaTime;
    }

    void CalculateMousePosition()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out floorHit, 50.0f, floorMask))
        {
            mousePosition = floorHit.point;
            mousePosition.y = transform.position.y;
        }
    }

    void LookMousePositon()
    {
        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
        Vector3 playerToMouse = mousePosition - transform.position;
        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        // Set the player's rotation to this new rotation.
        playerRigidbody.MoveRotation(newRotation);
    }


    public void addBehaviour(Behaviour b)
    {
        Debug.Log("Ajout du comportement " + b.name);
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

    public Vector3 GetMousePosition()
    {
        return mousePosition;
    }
}

public class BehaviourController
{
    public readonly Behaviour b;
    public float remainingTime;

    public BehaviourController(Behaviour b)
    {
        this.b = b;
        remainingTime = b.duree;
    }

    public bool Expired()
    {
        return remainingTime < 0;
    }

    public void decreaseTimer(float dt)
    {
        remainingTime -= dt;
    }
}

