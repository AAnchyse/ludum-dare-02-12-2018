using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public AbilityManager am;
    public GameObject ButtonPrefab;
    /*
    public Image[] cooldownImage;
    public Image[] iconImage;
    public Image[] extImage;
    */

    ImageScript[] allIcons;
    int frameSkip = 0;
    int N = 3;

    // Use this for initialization
    void Start ()
    {
        // Debug.Log("Taille du tableau : " + cooldownImage.Length);
        allIcons = new ImageScript[am.abilities.Length];
        for (int i = 0; i < am.abilities.Length; i++)
        {
            GameObject last = GameObject.Instantiate(ButtonPrefab, transform);
            allIcons[i] = last.GetComponent<ImageScript>();
            allIcons[i].SetAbility(am.abilities[i]);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        frameSkip++;
        if(frameSkip == 3)
        {
            RefreshUI();
            frameSkip = 0;
        }
	}

    public void RefreshUI()
    {
        for(int i = 0; i < allIcons.Length ; i ++)
        {
            allIcons[i].cooldownIcon.fillAmount = am.getCooldownRate(i);
            /*
            extImage[i].fillAmount = pc.get(i);*/

            if (am.getCooldownRate(i) > 0)
                allIcons[i].baseIcon.color = new Color(.6f, .6f, .6f);
            else
                allIcons[i].baseIcon.color = new Color(1, 1, 1);
        }
    }
}
