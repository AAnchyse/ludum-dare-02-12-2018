using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum typeButton {
    Play,
    Option,
    About,
}

public class ButtonMenu : MonoBehaviour {
    public typeButton button;
    public Camera cam;
    GameObject[] panel;
    public GameObject buttonPlay;
    public GameObject buttonOption;

    public SettingsManager sm;


    private Rect WindowRect = new Rect((Screen.width / 2) - 100, Screen.height / 2, 200, 200);
    private float volume = 1.0f;

    private void OnEnable()
    {
        panel = GameObject.FindGameObjectsWithTag("OptionPanel");
        foreach(GameObject p in panel)
        {
            p.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseUp() {
        switch (button)
        {
            case typeButton.Play:
                Application.LoadLevel("GameScene");
                DontDestroyOnLoad(sm);
                break;
            case typeButton.Option:
                foreach (GameObject p in panel)
                {
                    p.SetActive(true);
                    buttonPlay.GetComponent<BoxCollider>().enabled = false;
                    buttonOption.GetComponent<BoxCollider>().enabled = false;
                }
                break;
            default:
                break;
        }
    }
    
}
