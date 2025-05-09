using UnityEngine;
using TMPro;

public class JobDiscet_Controller : DraggableObject
{
    [Header("=== Discet Settings ===")]
    [SerializeField] public JobDiscet_Data _jobDiscetData;
    [SerializeField] private TMP_Text _displayName;

    [Header("=== Snapping Settings ===")]
    [SerializeField] private float snapDistance = 0.5f;
    [SerializeField] private float returnDistance = 1.5f;

    private Vector3 _lastSnapPosition;
    private Transform _lastSnapParent;

    public void InsertData(JobDiscet_Data _insertedData)
    {
        _jobDiscetData = _insertedData;
        _displayName.text = _jobDiscetData._JobName;
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        _lastSnapPosition = transform.position;
        _lastSnapParent = transform.parent;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

        GameObject closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (var point in DiscetHolderBar_Controller.Instance.HolderBarPoints)
        {
            float dist = Vector3.Distance(transform.position, point.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestPoint = point;
            }
        }

        if (closestDistance <= snapDistance)
        {
            transform.position = closestPoint.transform.position;
            transform.SetParent(closestPoint.transform);
            return;
        }

        if (Vector3.Distance(transform.position, _lastSnapPosition) <= returnDistance)
        {
            transform.position = _lastSnapPosition;
            transform.SetParent(_lastSnapParent);
            return;
        }

        GameObject canvas = GameObject.FindWithTag("cardsCanvas");
        if (canvas != null)
        {
            transform.SetParent(canvas.transform);
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.position = _lastSnapPosition;
            transform.SetParent(_lastSnapParent);
        }
    }
}
