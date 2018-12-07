using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {
    public Slider volumeSlider;
    public Button apply;
    public Toggle isQwerty;
    public GameObject buttonPlay;
    public GameObject buttonOption;
    public GameObject prefab;

    public AudioSource music;
    PlayerController pc;

    [HideInInspector] public GameSettings gameSettings;
    private GameObject[] panel;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    private void OnEnable()
    {
        pc = prefab.GetComponent<PlayerController>();
        panel = GameObject.FindGameObjectsWithTag("OptionPanel");
        gameSettings = new GameSettings();
        volumeSlider.onValueChanged.AddListener(delegate { onMusicVolumeChange(); });
        apply.onClick.AddListener(delegate { onApplyButtonClick(); });
        isQwerty.onValueChanged.AddListener(delegate { onToggleChange(); });
    }

    public void onMusicVolumeChange()
    {
        music.volume = gameSettings.musicVolume = volumeSlider.value;
    }
    

    public void onApplyButtonClick()
    {
        saveSettings();
        buttonPlay.GetComponent<BoxCollider>().enabled = true;
        buttonOption.GetComponent<BoxCollider>().enabled = true;
        foreach (GameObject p in panel)
        {
            p.SetActive(false);
        }
    }

    public void saveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        Debug.LogWarning(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void loadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(Application.persistentDataPath + "/gamesettings.json");
        volumeSlider.value = gameSettings.musicVolume;
        pc.isQWERTY = gameSettings.isQwerty;
    }

    public void onToggleChange()
    {
        gameSettings.isQwerty =  pc.isQWERTY = !gameSettings.isQwerty;
    }
}
