using System;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [Header("=== References ===")]
    [SerializeField] private GameObject _clickIconCanvas;

    private Camera _mainCamera;
    private Outline _outlineController;
    private bool _isHovered;

    private void Start()
    {
        _mainCamera = Camera.main;
        _outlineController = GetComponentInParent<Outline>();
        _outlineController.enabled = false;
        _clickIconCanvas.SetActive(false);
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            if (!_isHovered)
            {
                OnHoverEnter();
            }
            UpdateCanvasRotation();
        }
        else
        {
            if (_isHovered)
            {
                OnHoverExit();
            }
        }
    }

    private void OnHoverEnter()
    {
        _isHovered = true;
        _outlineController.enabled = true;
        _clickIconCanvas.SetActive(true);
    }

    private void OnHoverExit()
    {
        _isHovered = false;
        _outlineController.enabled = false;
        _clickIconCanvas.SetActive(false);
    }

    private void UpdateCanvasRotation()
    {
        Vector3 directionToCamera = _mainCamera.transform.position - _clickIconCanvas.transform.position;
        _clickIconCanvas.transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}