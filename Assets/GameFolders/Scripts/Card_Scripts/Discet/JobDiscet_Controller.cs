using System;
using UnityEngine;
using TMPro;
using System.Linq;
using Zenject;

public class JobDiscet_Controller : DraggableObject
{
    [Header("=== Discet Settings ===")]
    [SerializeField] private JobDiscet_Data _jobDiscetData;
    [SerializeField] private TMP_Text _displayName;

    [Header("=== Snapping Settings ===")]
    [SerializeField] private float snapDistance = 0.5f;
    [SerializeField] private float returnDistance = 1.5f;

    [Header("=== Character Drop Settings ===")]
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private LayerMask characterLayer;

    private Vector3 _lastSnapPosition;
    private Transform _lastSnapParent;
    private bool _wasInHotbar;
    private DiscetHolderBar_Controller _discetHolderBar;

    private void Start()
    {
        _discetHolderBar = FindFirstObjectByType<DiscetHolderBar_Controller>().GetComponent<DiscetHolderBar_Controller>();
    }

    public void InsertData(JobDiscet_Data insertedData, string visibleName)
    {
        _jobDiscetData = insertedData;
        _displayName.text = visibleName;
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        _lastSnapPosition = transform.position;
        _lastSnapParent = transform.parent;
        _wasInHotbar = _discetHolderBar.HolderBarPoints.Contains(_lastSnapParent?.gameObject);
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

        // 1. Попадание на персонажа
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, dropDistance, characterLayer);
        foreach (var hit in hits)
        {
            var view = hit.GetComponent<CharacterView>();
            if (view != null)
            {
                view.GetInstance().AssignJob();
                Destroy(gameObject);
                return;
            }
        }

        // 2. Снаппинг в хотбар
        GameObject closestPoint = null;
        float closestDistance = Mathf.Infinity;
        foreach (var point in DiscetHolderBar_Controller.Instance.HolderBarPoints)
        {
            if (point.transform.childCount > 1) continue;
            float dist = Vector3.Distance(transform.position, point.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestPoint = point;
            }
        }
        if (closestPoint != null && closestDistance <= snapDistance)
        {
            transform.position = closestPoint.transform.position;
            transform.SetParent(closestPoint.transform);
            return;
        }

        // 3. Возврат в хотбар при небольшом отлёте
        if (_wasInHotbar && Vector3.Distance(transform.position, _lastSnapPosition) <= returnDistance)
        {
            transform.position = _lastSnapPosition;
            transform.SetParent(_lastSnapParent);
            return;
        }

        // 4. Перемещение на канвас
        var humanCanvas = GameObject.FindWithTag("humanCanvas");
        if (humanCanvas != null)
        {
            humanCanvas.GetComponentInParent<CharacterView>().GetInstance().AssignJob();
            Destroy(gameObject);
        }
        else
        {
            var canvas = GameObject.FindWithTag("cardsCanvas");
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
}
