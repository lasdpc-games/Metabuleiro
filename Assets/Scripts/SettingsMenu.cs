using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    float currentVolume;

    Resolution[] resolutions;

    void Start(){
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        AudioManager.GetInstance().Play("ButtonOther");
        
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void LoadSettings(int currentResolutionIndex){
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        fullScreenToggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("VolumePreference"))
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
    }

    public void SetVolume (float volume){
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("VolumePreference", volume); 
    }

    public void SetFullscreen(bool isFullscreen){
	    Screen.fullScreen = isFullscreen;
	    PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
    }

    public void SetResolution(int resolutionIndex){
	    Resolution resolution = resolutions[resolutionIndex];
	    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }
}