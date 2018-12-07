using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    Ability ability;

    public Image cooldownIcon;
    public Image baseIcon;
    public Image contour;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetAbility(Ability a)
    {
        ability = a;
        cooldownIcon.sprite = a.aIcon;
        baseIcon.sprite = a.aIcon;
        contour.sprite = a.aIcon;
    }
}
