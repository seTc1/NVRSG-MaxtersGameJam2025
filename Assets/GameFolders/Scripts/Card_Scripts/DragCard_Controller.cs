using System.Linq;
using TMPro;
using UnityEngine;

public class DragCard_Controller : MonoBehaviour
{
    [Header("=== Card Data ===")]
    [SerializeField] public WordCard_Data _wordCardData;
    [SerializeField] private TMP_Text _cardText;

    [Header("=== Drag Settings ===")]
    [SerializeField] private Camera _checkCamera;
    private bool _isDragging;
    private Vector3 offset;

    [Header("=== Combine Data ===")]
    
    [SerializeField] private JobCombine_Data _jobCombineData;

    private void Start()
    {
        _cardText.text = _wordCardData._cardAbility;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (var hit in colliders)
        {
            if (hit.gameObject == gameObject) continue;

            var otherCard = hit.GetComponent<DragCard_Controller>();
            if (otherCard != null)
            {
                TryCombineWith(otherCard);
                break;
            }
        }
    }

    private void Update()
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

    private void TryCombineWith(DragCard_Controller other)
    {
        int[] currentIDs = new[] { _wordCardData._cardID, other._wordCardData._cardID };

        foreach (var recipe in _jobCombineData._JobRecipes)
        {
            if (recipe.combineID.Length != 2) continue;

            if (recipe.combineID.OrderBy(x => x).SequenceEqual(currentIDs.OrderBy(x => x)))
            {
                
                GameObject _spawnedDiscet =  Instantiate(_jobCombineData._jobDiscetPrefab, transform.position, Quaternion.identity);
                _spawnedDiscet.transform.parent = GameObject.FindWithTag("cardsCanvas").transform;
                _spawnedDiscet.transform.localScale = Vector3.one; 
                _spawnedDiscet.GetComponent<JobDiscet_Controller>().InsertData(recipe._jobData);
                Debug.Log("COMBINED INTO: " + recipe.jobName);
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }
        }
    }

}
