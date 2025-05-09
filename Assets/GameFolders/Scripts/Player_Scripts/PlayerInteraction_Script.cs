using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction_Script : MonoBehaviour
{

    [Header("=== Камеры ===")]
    [SerializeField] private GameObject _outerCamera;
    [SerializeField] private GameObject monitorLookCamera;
    [SerializeField] private GameObject[] monitorCameras;
    [SerializeField] private GameObject[] mainCameras;

    [Header("=== Экраны и интерфейс ===")]
    [SerializeField] private float noiseDelay;
    [SerializeField] private Animator screenNoiseAnimator;
    [SerializeField] private GameObject[] monitorCanvases;
    [SerializeField] private GameObject[] monitorEnvironments;

    [Header("=== Отладка ===")] 
    [SerializeField] private bool isLookingAtMonitors;

    public void ChangeLook()
    {
        isLookingAtMonitors = !isLookingAtMonitors;
        _outerCamera.SetActive(!_outerCamera.activeSelf);
        monitorLookCamera.SetActive(!monitorLookCamera.activeSelf);
    }

    public void MonitorClicked(int monitorID)
    {
        if (isLookingAtMonitors)
        {
            screenNoiseAnimator.SetTrigger("noise");
            monitorLookCamera.SetActive(false);
            monitorCameras[monitorID].SetActive(true);

            switch (monitorID)
            {
                case 0:
                    StartCoroutine(SwitchToStatsView());
                    break;
                case 1:
                    StartCoroutine(SwitchToManage());
                    break;
                case 2:
                    StartCoroutine(SwitchToCraft());
                    break;
            }
        }
    }

    public void ReturnToChair()
    {
        screenNoiseAnimator.SetTrigger("noise");
        StartCoroutine(DelayReturnToChair());
    }

    private IEnumerator DelayReturnToChair()
    {
        yield return new WaitForSeconds(noiseDelay);   
        isLookingAtMonitors = true;
        mainCameras[0].SetActive(true);
        mainCameras[1].SetActive(false);

        foreach (GameObject canvas in monitorCanvases)
        {
            canvas.SetActive(false);
        }
        foreach (GameObject _enviroment in monitorEnvironments)
        {
            _enviroment.SetActive(false);
        }
        monitorLookCamera.SetActive(true);
    }

    private IEnumerator SwitchToCraft()
    {
        yield return new WaitForSeconds(noiseDelay); 
        isLookingAtMonitors = false;
        monitorCameras[2].SetActive(false);
        monitorCanvases[2].SetActive(true);
        monitorEnvironments[2].SetActive(true);
        mainCameras[0].SetActive(false);
        mainCameras[1].SetActive(true);
    }

    private IEnumerator SwitchToManage()
    {
        yield return new WaitForSeconds(noiseDelay);
        isLookingAtMonitors = false;
        monitorCameras[1].SetActive(false);
        monitorCanvases[1].SetActive(true);
        monitorEnvironments[1].SetActive(true);
        mainCameras[0].SetActive(false);
        mainCameras[1].SetActive(true);
    }
    private IEnumerator SwitchToStatsView()
    {
        yield return new WaitForSeconds(noiseDelay);   
        isLookingAtMonitors = false;
        monitorCameras[0].SetActive(false);
        monitorCanvases[0].SetActive(true);
        monitorEnvironments[0].SetActive(true);
        mainCameras[0].SetActive(false);
        mainCameras[1].SetActive(true);
    }
}
