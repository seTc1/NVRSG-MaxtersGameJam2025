using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction_Script : MonoBehaviour
{
    [SerializeField] private GameObject _outerCam;
    [SerializeField] private GameObject _lookOnMonitorCam;

    [SerializeField] private float _noiseDelay;
    [SerializeField] private GameObject _noiseCanvas;
    [SerializeField] private Animator _screenNoiseAnimator;
    [SerializeField] private GameObject[] _monitorCams;
    
    [SerializeField] private GameObject[] _mainCams;

    [SerializeField] private GameObject[] _monitorCanvases;
    [Header("=== Debug ===")] 
    [SerializeField] private bool _lookingOnMonitors;


    public void ChangeLook()
    {
        _lookingOnMonitors = !_lookingOnMonitors;
        _outerCam.SetActive(!_outerCam.activeSelf);
        _lookOnMonitorCam.SetActive(!_lookOnMonitorCam.activeSelf);
    }

    public void MonitorClicked(int _monitorID)
    {
        if (_lookingOnMonitors)
        {
            _screenNoiseAnimator.SetTrigger("noise");
            _lookOnMonitorCam.SetActive(false);
            _monitorCams[_monitorID].SetActive(true);
            switch (_monitorID)
            {
                case 0:
                    StartCoroutine(GoToStats());
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }

    public void ReturnToChair()
    {
        _screenNoiseAnimator.SetTrigger("noise");
        StartCoroutine(ReturnDelay());
    }

    private IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(_noiseDelay);   
        _lookingOnMonitors = true;
        _mainCams[0].SetActive(true);
        _mainCams[1].SetActive(false);
        foreach (GameObject _canvas in _monitorCanvases)
        {
            _canvas.SetActive(false);
        }
        _lookOnMonitorCam.SetActive(true);
    }

    private IEnumerator GoToStats()
    {
        yield return new WaitForSeconds(_noiseDelay);   
        _lookingOnMonitors = false;
        _monitorCams[0].SetActive(false);
        _monitorCanvases[0].SetActive(true);
        _mainCams[0].SetActive(false);
        _mainCams[1].SetActive(true);
    }
    
}
