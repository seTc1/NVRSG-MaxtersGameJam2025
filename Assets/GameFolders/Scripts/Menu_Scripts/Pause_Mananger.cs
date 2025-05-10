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
        //fullscreen
        // resolution
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
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log(isFullscreen);
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
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
        
    }
    public void ExitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}