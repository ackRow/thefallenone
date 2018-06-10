using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsLink : MonoBehaviour {

    Toggle fullscreenToggle;
    Toggle showfps;
    Dropdown resolutiondd;
    Dropdown textqualdd;
    Dropdown antiadd;
    Dropdown languagedd;
    Slider audiovolume;
    Button Save;

    Text FPS;

    public AudioSource audiosource;
    public Resolution[] resolutions;
    GameSettings gameSettings;

	// Use this for initialization
	void Start () {
        fullscreenToggle = GameObject.Find("FullScreen").GetComponent<Toggle>();
        showfps = GameObject.Find("FPS_toggle").GetComponent<Toggle>();
        resolutiondd = GameObject.Find("Resolution").GetComponent<Dropdown>();
        textqualdd = GameObject.Find("Texture").GetComponent<Dropdown>();
        antiadd = GameObject.Find("Anti_Aliasing").GetComponent<Dropdown>();
        languagedd = GameObject.Find("Languages").GetComponent<Dropdown>();
        audiovolume = GameObject.Find("Audio").GetComponent<Slider>();
        FPS = GameObject.Find("FPS_display").GetComponent<Text>();
        Save = GameObject.Find("Save").GetComponent<Button>();
    }
	
    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnfullscreenToggle(); });
        showfps.onValueChanged.AddListener(delegate { OnfpsToggle(); });
        resolutiondd.onValueChanged.AddListener(delegate { OnresolutionChange(); });
        textqualdd.onValueChanged.AddListener(delegate { OntextqualChange(); });
        antiadd.onValueChanged.AddListener(delegate { OnantiaChange(); });
        languagedd.onValueChanged.AddListener(delegate { OnlanguageChange(); });
        audiovolume.onValueChanged.AddListener(delegate { OnaudioVolumeChange(); });
        Save.onClick.AddListener(delegate { OnSaveButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutiondd.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        try
        {
            LoadSettings();
        }
        catch
        {

        }
    }

    public void OnfullscreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnfpsToggle()
    {
        gameSettings.showfps = showfps.isOn;
        if (showfps.isOn)
            FPS.color = new Color(0, 0, 0, 1);
        else
            FPS.color = new Color(0, 0, 0, 0);
    }

    public void OnresolutionChange()
    {
        Screen.SetResolution(resolutions[resolutiondd.value].width, resolutions[resolutiondd.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutiondd.value;
    }

    public void OntextqualChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textqual = textqualdd.value;
    }

    public void OnantiaChange()
    {
        QualitySettings.antiAliasing = gameSettings.antia = (int) Mathf.Pow(2f, antiadd.value);
    }

    public void OnlanguageChange()
    {
        languagedd.options[languagedd.value].text = gameSettings.language;
    }

    public void OnaudioVolumeChange()
    {
        gameSettings.audioVolume = audiovolume.value;
    }

    public void OnSaveButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));


        audiovolume.value = gameSettings.audioVolume;
        antiadd.value = gameSettings.antia;
        textqualdd.value = gameSettings.textqual;
        resolutiondd.value = gameSettings.resolutionIndex;

        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;

        showfps.isOn = gameSettings.showfps;
        languagedd.options[languagedd.value].text = gameSettings.language;

        resolutiondd.RefreshShownValue();
    }
}
