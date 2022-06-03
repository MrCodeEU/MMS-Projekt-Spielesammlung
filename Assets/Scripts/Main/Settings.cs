using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Settings : MonoBehaviour
{
    public new AudioMixer audio;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullScreen;
    float currentVolume;
    Resolution[] resolutions;
    String[] qualityLevels;

    /// <summary>
    /// Start is called before the first frame update
    /// Handles the loading of resolutions supported by the monitor
    /// </summary>
    void Start()
    {
        resolutionDropdown.ClearOptions(); // clear previously loaded Resolutions
        List<string> options = new List<string>();
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray(); // Only show distinntc resolutions (no duplicates due to diffrent Framerates)
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
        //Select last resolution (highest) and add all options to dropdown
        int currentResolutionIndex = options.Count - 1;
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        // Save selected Resolution
        LoadSettigns(currentResolutionIndex);
        //Get all quality settings
        qualityDropdown.ClearOptions();
        List<String> quality = new List<String>();
        qualityLevels = QualitySettings.names;
        for (int i = 0; i < qualityLevels.Length; i++)
        {
            string option = qualityLevels[i];
            quality.Add(option);
        }
        //add all settings to dropdown and select last one
        qualityDropdown.AddOptions(quality);
        qualityDropdown.RefreshShownValue();
        qualityDropdown.value = qualityLevels.Length - 1;
    }

    /// <summary>
    /// Sets the selected volume of slider
    /// </summary>
    public void SetVolume()
    {
        audio.SetFloat("Music", Mathf.Log10(volumeSlider.value) * 25);
        currentVolume = volumeSlider.value;
    }
    /// <summary>
    /// Set fullscreen depending on toggle
    /// </summary>
    /// <param name="fullscreen">Value of toggle</param>
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    /// <summary>
    /// Sets the selected resoltuion
    /// </summary>
    public void SetResolution()
    {
        int resolutionIndex = resolutionDropdown.value;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log(resolution);
    }

    /// <summary>
    /// Sets the selected quality
    /// </summary>
    public void SetQuality()
    {
        int qualityIndex = qualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Exits the application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Save all currently selected settings
    /// </summary>
    public void SaveSettigns()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", currentVolume);
    }

    /// <summary>
    /// Loads the previously saved settings
    /// </summary>
    /// <param name="currentResolutionIndex"></param>
    public void LoadSettigns(int currentResolutionIndex)
    {
        qualityDropdown.value = PlayerPrefs.HasKey("QualitySettingPreference") ? PlayerPrefs.GetInt("QualitySettingPreference") : 3;

        resolutionDropdown.value = PlayerPrefs.HasKey("ResolutionPreference") ? PlayerPrefs.GetInt("ResolutionPreference") : currentResolutionIndex;

        Screen.fullScreen = PlayerPrefs.HasKey("FullscreenPreference") ? Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference")) : false;

        volumeSlider.value = PlayerPrefs.HasKey("VolumePreference") ? PlayerPrefs.GetFloat("VolumePreference") : 0.5f;
    }

    /// <summary>
    /// Loads the game selection scene
    /// </summary>
    public void GoToGameSelectScene()
    {
        SceneManager.LoadScene(0);
    }
}
