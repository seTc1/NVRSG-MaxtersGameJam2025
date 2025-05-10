using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{
    [Header("=== References ===")]
    [SerializeField] private GameObject _clickIconCanvas;
    [SerializeField] public bool _canInteract;
    
    [Header("=== Events ===")]
    [SerializeField] private UnityEvent _onClick;

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
        if (!_canInteract) {return;}
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            if (!_isHovered)
            {
                OnHoverEnter();
            }

            UpdateCanvasRotation();

            if (Input.GetMouseButtonDown(0))
            {
                _onClick?.Invoke();
            }
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
        if (!_canInteract) {return;}
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