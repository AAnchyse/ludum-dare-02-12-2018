using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum Direction { FORWARD, BACKWARD, STRAFFE_LEFT, STRAFFE_RIGHT}

public class PlayerController : MonoBehaviour
{
    public LayerMask floorMask;
    [HideInInspector] public bool isQWERTY = true;

    Vector3 delta = Vector3.zero;
    [HideInInspector] public Vector3 mousePosition;
    Ray ray;       
    RaycastHit floorHit;
    Rigidbody playerRigidbody;

    protected Dictionary<Behaviour, int> allBehaviours;

    CharacteristicsManager cm;

    protected List<BehaviourController> timedBehaviours;
    protected List<BehaviourController> expired;

    public float straffeSpeedMultiplier;
    public float backSpeedMultiplier;
    public Animator animator;

    Rigidbody body;

    public GameObject model;

    GameSettings gs;

    private void OnEnable()
    {
        gs = FindObjectOfType<SettingsManager>().gameSettings;
    }


    // Use this for initialization
    void Start ()
    {
        isQWERTY = gs.isQwerty;
        animator = GetComponentInChildren<Animator>();
        timedBehaviours = new List<BehaviourController>();
        expired = new List<BehaviourController>();
        cm = GetComponent<CharacteristicsManager>();

        playerRigidbody = GetComponentInChildren<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CalculateMousePosition();
        Move();
        LookMousePositon();
        RefreshBehaviours();

    }
    
    private void RefreshBehaviours()
    {
        expired.Clear();

        foreach (BehaviourController bc in timedBehaviours)
        {
            // On peut skipper des frames pour l'optimisation
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
    }

    private void Move()
    {
        float dx;
        float dz;
        if (isQWERTY)
        {
            Debug.LogWarning("Go");
             dx = Input.GetAxis("Horizontal");
             dz = Input.GetAxis("Vertical");
        }
        else{

            Debug.LogWarning("Go non FR");
            dx = Input.GetAxis("Horizontal_non_French");
             dz = Input.GetAxis("Vertical_non_French");
        }
        Vector3 pos = -transform.position + GetMousePosition();
        delta = new Vector3(dx, 0, dz);

        if (dx * dx + dz * dz < 0.1f)
        {
            animator.SetBool("Walking", false);
            return;
        }

        if (dx * dx + dz * dz > 1)
        {
            delta.Normalize();
        }
  //      Vector3 pos = -transform.position + GetMousePosition();
        pos.y = 0;
        pos.Normalize();
        Direction d;
        float mult = 1.0f;
        float angle =Vector3.SignedAngle( pos, delta, Vector3.up);

        animator.SetBool("Walking", true);

        if (Mathf.Abs(angle) < 60)
        {
            d = Direction.FORWARD;
            animator.SetInteger("Direction", 0);
            mult = 1;
        }

        // Quaternion.Euler(0, Vector.SignedAngle(Vector3.right, direction, Vector3.up), 0)

        else if (Mathf.Abs(angle) < 130)
        {
            animator.SetInteger("Direction", (int) Mathf.Sign(angle));
            d = angle > 0 ? Direction.STRAFFE_RIGHT : Direction.STRAFFE_LEFT;
            mult = straffeSpeedMultiplier;
        }

        else
        {
            animator.SetInteger("Direction", 2);
            d = Direction.BACKWARD;
            mult = backSpeedMultiplier;
        }

        transform.position += delta * cm.moveSpeed * Time.deltaTime * mult;
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
        // On vérifie que le Comportement n'est pas supérieur a sa limite d'empilement.
        Debug.Log("Gain de l'effet " + b.name);
        // Si le modificateur a une durée limitée dans le temps on ajoute un Gestionnaire de temps
        if (b.timed)
        {
            timedBehaviours.Add(new BehaviourController(b));
        }
        // On ajoute tous les flags à ajouter à l'unité
        if (b.giveInvulnerability)
        {
            cm.invulnerable = true;
        }

        // On multiplie les différentes caractéristiques de l'unité
        cm.delayMultiplier *= b.fireRateMultiplier;
    }

    public void RemoveBehaviour(Behaviour b)
    {
        Debug.Log("Perte de l'effet " + b.name);
        if(b.desactivation != null)
        { 
            b.desactivation.ApplyEffect(gameObject, gameObject.transform.position);
        }
        if (b.giveInvulnerability)
        {
            cm.invulnerable = false;
        }

        // On divise les différentes caractéristiques de l'unité
        cm.delayMultiplier /= b.fireRateMultiplier;
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

