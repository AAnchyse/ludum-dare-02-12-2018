using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TP : MonoBehaviour
{
    Material mat;
    Renderer rend;
    Transform tra;

  //  public ParticleSystem tp;
    //public ParticleSystem shock;
    public ParticleSystem aura;
    public LayerMask floorMask;

    public float dureeTransparence = 0.5f;
    public float duree = 2f;

    public Transform bubulle;

    float transparence = 0f;
    int state = 0;

    Vector3 deplacement;
    Vector3 direction;
    float vitesseDeplacement;
    Vector3 mousePosition;
    float distance = 0;
    float parcouru = 0;

    float timer;

    // Use this for initialization
    void Start()
    {
        mat = GetComponentInChildren<Renderer>().material;
        rend = GetComponentInChildren<Renderer>();
        tra = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

  /*      //////////////////
        // Onde de choc //
        //////////////////
        if (Input.GetKeyDown(KeyCode.A) && state == 0)
        {
            ParticleSystem newParticleSystem = Instantiate(shock, tra.position, Quaternion.Euler(90,0,0)) as ParticleSystem;
            Destroy(newParticleSystem.gameObject, 3f);
        }
*/
        //////////////
        // Bouclier //
        //////////////
        if (Input.GetKeyDown(KeyCode.R) && state == 0)
        {
            state = -1;
        }

        // Le bouclier grandit
        if (state == -1)
        {
            bubulle.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            if (bubulle.localScale.x >= 2)
            {
                timer = 0;
                state = -2;
            }
        }

        // On attend un moment
        if (state == -2)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
                state = -3;
        }

        // Le bouclier rapetisse
        if (state == -3)
        {
            bubulle.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            if (bubulle.localScale.x <= 0)
                state = 0;
        }
        /*
        ///////////////////
        // Teleportation //
        ///////////////////
        if (Input.GetKeyDown(KeyCode.E) && state == 0)
        {
            // On fait un raycast
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            // Si le raycast rencontre le sol
            if (Physics.Raycast(ray, out floorHit, 50.0f, floorMask))
            {
                state = 1;

                mousePosition = floorHit.point;
                mousePosition.y = transform.position.y;

                deplacement = mousePosition - tra.position;
                deplacement.y = 0.0f;
                distance = deplacement.magnitude;
                vitesseDeplacement = (distance) / duree;
                direction = deplacement.normalized;

                // On instancie l'effet de particule
                var velocityOverLifetime = tp.velocityOverLifetime;
                velocityOverLifetime.speedModifierMultiplier = vitesseDeplacement;
                Quaternion quater = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.right, direction, Vector3.up), 0);
                ParticleSystem newParticleSystem = Instantiate(tp, tra.position, quater) as ParticleSystem;

                // Destruction programmée de l'effet de particule
                Destroy(newParticleSystem.gameObject, 3f);
            }
        }

        // On fait disparaitre la cible avec un fade
        if (state == 1)
        {
            transparence += Time.deltaTime / dureeTransparence;
            //mat.SetFloat("Vector1_E587E27B", transparence);
            rend.enabled = false;
            if (transparence >= 1)
            {
                rend.shadowCastingMode = ShadowCastingMode.Off;
                state = 2;
            }
        }

        // la camera suit le cube (invisible)
        if (state == 2)
        {
            tra.Translate(direction * vitesseDeplacement * Time.deltaTime);
            parcouru += (direction * vitesseDeplacement * Time.deltaTime).magnitude;

            // On passe a l'étape suivante si on est assez proche de l'objectif
            if (parcouru >= distance)
            {
                state = 3;
                parcouru = 0;
            }
        }

        // On refait reapparaitre la cible avec un fade
        if (state == 3)
        {
            transparence -= Time.deltaTime / dureeTransparence;
            //mat.SetFloat("Vector1_E587E27B", transparence);
            rend.enabled = true;
            if (transparence <= 0)
            {
                state = 0;
                rend.shadowCastingMode = ShadowCastingMode.On;
            }
        }

        /////////////
        // Artemis //
        /////////////
        if (Input.GetKeyDown(KeyCode.R) && state == 0)
        {
            ParticleSystem newParticleSystem = Instantiate(aura, tra.position, Quaternion.Euler(-90, 0, 0)) as ParticleSystem;
            Destroy(newParticleSystem.gameObject, 10f);
            state = 10;
            timer = 0;
        }

        if (state ==10)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
                state = 0;
        }
    */

    }
}
