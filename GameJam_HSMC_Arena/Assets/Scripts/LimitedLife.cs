using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour
{
    public float life;

    public Effect onHit;
    public Effect onExpire;

    public GameObject spawnSource;

    float timer = 0.0f;
    // Use this for initialization
	void Start ()
    {
    	
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > life)
        {
            if (onExpire)
            {
                onExpire.ApplyEffect(gameObject, transform.position);
            }
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(onHit)
        { 
            if (other.GetComponent<Enemy>())
            {
                onHit.ApplyEffect(spawnSource, other.gameObject);
                Destroy(gameObject);
            }
            else if(other.gameObject.CompareTag("Decor"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3();
                GetComponent<Rigidbody>().isKinematic = true;
                Destroy(gameObject, 5);
            }
        }
    }

}
