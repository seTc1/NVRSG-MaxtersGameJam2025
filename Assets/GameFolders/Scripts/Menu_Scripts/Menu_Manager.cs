using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private string _gameSceneName = "GameScene"; // Set your scene name in the Inspector

    private bool _settingsOpen;
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    [SerializeField] public AudioMixer audioMixer;
    Resolution[] resolutions;
    private void Start()
    {
        _mainCanvas.SetActive(!_settingsOpen);
        _settingsCanvas.SetActive(_settingsOpen);
        //fullscreen
        Screen.fullScreen = false;
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

    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
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
    public void SettingsWindow()
    {
        _settingsOpen = !_settingsOpen;
        _mainCanvas.SetActive(!_settingsOpen);
        _settingsCanvas.SetActive(_settingsOpen);
    }

    public void ExitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}