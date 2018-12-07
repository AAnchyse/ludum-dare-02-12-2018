using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {

    public Ability ab;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Image>().sprite = ab.aIcon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
