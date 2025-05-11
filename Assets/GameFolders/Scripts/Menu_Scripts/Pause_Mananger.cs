using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Pause_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _maincanvas;
    [SerializeField] private GameObject _pausecanvas;
    [SerializeField] private GameObject _settingsCanvas;

    private bool _pauseOpen;
    private bool _settingsOpen;

    [SerializeField] public TMP_Dropdown resolutionDropdown;
    [SerializeField] public AudioMixer audioMixer;

    Resolution[] resolutions;

    private void Start()
    {
        _maincanvas.SetActive(!_pauseOpen);
        _pausecanvas.SetActive(_pauseOpen);
        _settingsCanvas.SetActive(_settingsOpen);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        int savedResolution = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        int qualityIndex = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());

        Screen.SetResolution(resolutions[savedResolution].width, resolutions[savedResolution].height, isFullscreen);
        resolutionDropdown.value = savedResolution;
        resolutionDropdown.RefreshShownValue();
        Screen.fullScreen = isFullscreen;
        QualitySettings.SetQualityLevel(qualityIndex);

        if (PlayerPrefs.HasKey("MasterVolume")) audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        if (PlayerPrefs.HasKey("MusicVolume")) audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        if (PlayerPrefs.HasKey("SFXVolume")) audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseWindow();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SettingsWindow()
    {
        _settingsOpen = !_settingsOpen;
        _pauseOpen = !_pauseOpen;
        _pausecanvas.SetActive(_pauseOpen);
        _settingsCanvas.SetActive(_settingsOpen);
    }

    public void PauseWindow()
    {
        _pauseOpen = !_pauseOpen;
        _pausecanvas.SetActive(_pauseOpen);
        _maincanvas.SetActive(!_pauseOpen);
        Time.timeScale = _pauseOpen ? 0 : 1;
    }

    public void ExitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
