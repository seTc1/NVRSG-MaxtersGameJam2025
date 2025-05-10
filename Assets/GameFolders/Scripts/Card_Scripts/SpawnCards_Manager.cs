using System.Collections;
using UnityEngine;
using UnityEditor;
using Zenject;

public class SpawnCards_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private WordCard_Data[] _cardDataAvaible;

    [SerializeField] private Transform _spawnTransform;

    [SerializeField] private float _spawnDelay;

    [SerializeField] private GameObject _releaseCardsButton;
    
    [Header("=== Card Throw Settings ===")]
    [SerializeField] private float _minThrowAngle = 20f;
    [SerializeField] private float _maxThrowAngle = 160f;

    private PlayerStats_Manager _playerStats;

    [Inject]
    private void Construct(PlayerStats_Manager playerStats)
    {
        _playerStats = playerStats;
    }

    public void ReleaseCards()
    {
        StartCoroutine(ReleaseCardsRoutine());
    }

    private IEnumerator ReleaseCardsRoutine()
    {
        _releaseCardsButton.SetActive(false);
        int _cardsCount = _playerStats._jobCardsCount;
        _playerStats._jobCardsCount = 0;
        for (int i = 0; i < _cardsCount; i++)
        {
            GameObject spawnedCard = Instantiate(_cardPrefab, _spawnTransform.position, Quaternion.identity);
            spawnedCard.transform.SetParent(GameObject.FindWithTag("cardsCanvas").transform, false);
            spawnedCard.transform.position = _spawnTransform.position;
            spawnedCard.transform.localScale = Vector3.one;
            spawnedCard.GetComponent<DragCard_Controller>().InsertData(_cardDataAvaible[Random.Range(0, _cardDataAvaible.Length)]);

            Rigidbody2D rb = spawnedCard.GetComponent<Rigidbody2D>();
            float angle = Random.Range(_minThrowAngle, _maxThrowAngle);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            float force = Random.Range(400f, 1600f);
            rb.AddForce(direction * force);
            rb.linearDamping = 2f;

            yield return new WaitForSeconds(_spawnDelay);
        }
        _releaseCardsButton.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnTransform == null) return;

        float radius = 2f;
        int segments = 20;

        Vector3 origin = _spawnTransform.position;

#if UNITY_EDITOR
        Handles.color = Color.yellow;
#else
    Gizmos.color = Color.yellow;
#endif

        float step = (_maxThrowAngle - _minThrowAngle) / segments;

        for (int i = 0; i < segments; i++)
        {
            float angleA = _minThrowAngle + step * i;
            float angleB = _minThrowAngle + step * (i + 1);

            Vector3 pointA = origin + Quaternion.Euler(0, 0, angleA) * Vector3.right * radius;
            Vector3 pointB = origin + Quaternion.Euler(0, 0, angleB) * Vector3.right * radius;

#if UNITY_EDITOR
            Handles.DrawLine(pointA, pointB);
#else
        Gizmos.DrawLine(pointA, pointB);
#endif
        }

        Vector3 dirMin = Quaternion.Euler(0, 0, _minThrowAngle) * Vector3.right * radius;
        Vector3 dirMax = Quaternion.Euler(0, 0, _maxThrowAngle) * Vector3.right * radius;

#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawLine(origin, origin + dirMin);
        Handles.DrawLine(origin, origin + dirMax);
#else
    Gizmos.color = Color.red;
    Gizmos.DrawLine(origin, origin + dirMin);
    Gizmos.DrawLine(origin, origin + dirMax);
#endif
    }
}
