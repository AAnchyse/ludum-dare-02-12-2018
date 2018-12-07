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
        allIcons = new ImageScript[am.abilities.Length-1];
        Debug.Log(am.allAbilities.Length + " contre " + am.abilities.Length + " contre " + allIcons.Length);
        for (int i = 1; i < am.abilities.Length; i++)
        {
         //   if (Application.systemLanguage == SystemLanguage.French && i != 1)
           // {
                if (i == 0)
                {
                    GameObject last = GameObject.Instantiate(ButtonPrefab, transform);
                    allIcons[i] = last.GetComponent<ImageScript>();
                    allIcons[i].SetAbility(am.abilities[i]);
                }
                else
                {
                    GameObject last = GameObject.Instantiate(ButtonPrefab, transform);
                    allIcons[i-1] = last.GetComponent<ImageScript>();
                    allIcons[i-1].SetAbility(am.abilities[i]);
                }
        //    }
         /*   if(Application.systemLanguage != SystemLanguage.French && i != 0)
            {
                GameObject last = GameObject.Instantiate(ButtonPrefab, transform);
                allIcons[i-1] = last.GetComponent<ImageScript>();
                allIcons[i-1].SetAbility(am.abilities[i]);
            }*/
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
        }
    }

    public void LockAbility(int i)
    {
        allIcons[i].baseIcon.color = new Color(.6f, .6f, .6f);
        
    }

    public void UnlockAbility(int i)
    {
        allIcons[i].baseIcon.color = new Color(1, 1, 1);
    }

   public void ChangeRatio(int i, float f)
    {
        allIcons[i].contour.fillAmount = Mathf.Clamp01(f);

    }
}
