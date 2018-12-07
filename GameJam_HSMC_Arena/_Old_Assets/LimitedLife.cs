using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour {
    public float life;
	
    // Use this for initialization
	void Start () {
        Destroy(gameObject, life);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
