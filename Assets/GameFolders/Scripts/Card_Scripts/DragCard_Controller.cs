using System.Linq;
using TMPro;
using UnityEngine;

public class DragCard_Controller : DraggableObject
{
    [Header("=== Card Data ===")]
    [SerializeField] public WordCard_Data _wordCardData;
    [SerializeField] private TMP_Text _cardText;

    [Header("=== Combine Data ===")]
    [SerializeField] private JobCombine_Data _jobCombineData;

    private void Start()
    {
        _cardText.text = _wordCardData._cardAbility;
    }

    public void InsertData(WordCard_Data wordCardData)
    {
        _wordCardData = wordCardData;
        _cardText.text = _wordCardData._cardAbility;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

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

    private void TryCombineWith(DragCard_Controller other)
    {
        int[] currentIDs = new[] { _wordCardData._cardID, other._wordCardData._cardID };

        foreach (var recipe in _jobCombineData._JobRecipes)
        {
            if (recipe.combineID.Length != 2) continue;

            if (recipe.combineID.OrderBy(x => x).SequenceEqual(currentIDs.OrderBy(x => x)))
            {
                GameObject _spawnedDiscet = Instantiate(_jobCombineData._jobDiscetPrefab, transform.position, Quaternion.identity);
                _spawnedDiscet.transform.SetParent(GameObject.FindWithTag("cardsCanvas").transform, false);
                _spawnedDiscet.transform.localScale = Vector3.one;
                _spawnedDiscet.GetComponent<JobDiscet_Controller>().InsertData(recipe._jobData, recipe.jobName);
                Debug.Log("COMBINED INTO: " + recipe.jobName);
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }
        }
    }
}