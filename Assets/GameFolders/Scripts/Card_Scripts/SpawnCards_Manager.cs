using System.Collections;
using UnityEngine;
using Zenject;

public class SpawnCards_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private WordCard_Data[] _cardDataAvaible;
    
    [SerializeField] private Transform _spawnTransform;
    
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
        for (int i = 0; i < _playerStats._jobCardsCount; i++)
        {
            GameObject spawnedCard = Instantiate(_cardPrefab, _spawnTransform.position, Quaternion.identity);
            spawnedCard.transform.parent = GameObject.FindWithTag("cardsCanvas").transform;
            spawnedCard.transform.localScale = Vector3.one;

            Rigidbody2D rb = spawnedCard.GetComponent<Rigidbody2D>();
            float angle = Random.Range(0f, 90f);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            float force = Random.Range(800f, 1200f);
            rb.AddForce(direction * force);
            rb.linearDamping = 2f;

            yield return new WaitForSeconds(1f);
        }
    }


}
