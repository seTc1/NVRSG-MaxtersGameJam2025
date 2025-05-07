using System;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    
    private bool _settingsOpen;

    private void Start()
    {
        _mainCanvas.SetActive(!_settingsOpen);
        _settingsCanvas.SetActive(_settingsOpen);
    }

    public void StartGame()
    {
        
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
