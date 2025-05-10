using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EffectSpawner : MonoBehaviour, IEffectSpawner
{
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private GameObject[] _statusEffects;

    public void SpawnEffect(int effectID)
    {
        Debug.Log("Spawning effect " + effectID);
        if (effectID >= 0 && effectID < _statusEffects.Length && _statusEffects[effectID] != null)
        {
            Vector2 randomAnchoredPos = new Vector2(
                Random.Range(0, 500),
                Random.Range(0, 500)
            );

            GameObject effectInstance = Instantiate(_statusEffects[effectID], _canvasRect);
            effectInstance.GetComponent<RectTransform>().anchoredPosition = randomAnchoredPos;

            Destroy(effectInstance, 2f);
        }
    }
}