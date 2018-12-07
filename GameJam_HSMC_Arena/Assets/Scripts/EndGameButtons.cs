using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameButtons : MonoBehaviour {

    public Button restart;
    public Button mainMenu;

	// Use this for initialization
	private void OnEnable() {
        restart.onClick.AddListener(delegate { onRestartButtonClick(); });
        mainMenu.onClick.AddListener(delegate { onMenuButtonClick(); });
	}
	
    public void onRestartButtonClick()
    {
        Application.LoadLevel("GameScene");
    }

    public void onMenuButtonClick()
    {
        Application.LoadLevel("MainMenu");
    }

}
