using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [Header("=== Drag Settings ===")]
    private Camera _checkCamera;
    private bool _isDragging;
    private Vector3 offset;

    protected virtual void OnMouseDown()
    {
        if (_checkCamera == null)
            _checkCamera = GameObject.FindGameObjectWithTag("2Dcam")?.GetComponent<Camera>();

        if (_checkCamera == null)
        {
            Debug.LogError("Camera with tag '2Dcam' not found!");
            return;
        }

        _isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
    }

    protected virtual void OnMouseUp()
    {
        _isDragging = false;
    }

    protected virtual void Update()
    {
        if (_isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(_checkCamera.WorldToScreenPoint(transform.position).z);
        return _checkCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}