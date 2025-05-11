using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EffectSpawner : MonoBehaviour, IEffectSpawner
{
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private GameObject[] _statusEffects;  
    [SerializeField] private AudioClip[] _effectSounds;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SpawnEffect(int effectID)
    {
        Debug.Log("Spawning effect " + effectID);
        if (effectID >= 0 && effectID < _statusEffects.Length && _statusEffects[effectID] != null)
        {
            Vector2 randomAnchoredPos = new Vector2(
                Random.Range(0, 500),
                Random.Range(0, 500)
            );
            if (_statusEffects[effectID] != null)
            {
                _audioSource.PlayOneShot(_effectSounds[effectID]);
            }
            GameObject effectInstance = Instantiate(_statusEffects[effectID], _canvasRect);
            effectInstance.GetComponent<RectTransform>().anchoredPosition = randomAnchoredPos;

            Destroy(effectInstance, 2f);
        }
    }
}