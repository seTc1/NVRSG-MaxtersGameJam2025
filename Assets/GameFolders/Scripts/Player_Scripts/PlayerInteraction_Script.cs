using UnityEngine;

public class PlayerInteraction_Script : MonoBehaviour
{
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _lookOnMonitorCam;

    [SerializeField] private GameObject _noiseCanvas;
    [SerializeField] private Animator _screenNoiseAnimator;
    [SerializeField] private GameObject[] _monitorCams;
    [Header("=== Debug ===")] 
    [SerializeField] private bool _lookingOnMonitors;


    public void ChangeLook()
    {
        _lookingOnMonitors = !_lookingOnMonitors;
        _mainCam.SetActive(!_mainCam.activeSelf);
        _lookOnMonitorCam.SetActive(!_lookOnMonitorCam.activeSelf);
    }

    public void MonitorClicked(int _monitorID)
    {
        if (_lookingOnMonitors)
        {
            _noiseCanvas.SetActive(true);
            _screenNoiseAnimator.SetTrigger("noise");
            _lookOnMonitorCam.SetActive(false);
            _monitorCams[_monitorID].SetActive(true);
        }
    }
}
