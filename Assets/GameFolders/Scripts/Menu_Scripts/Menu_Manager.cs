using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private string _gameSceneName = "GameScene";
    private bool _settingsOpen;

    private void Start()
    {
        _mainCanvas.SetActive(!_settingsOpen);
        _settingsCanvas.SetActive(_settingsOpen);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
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
