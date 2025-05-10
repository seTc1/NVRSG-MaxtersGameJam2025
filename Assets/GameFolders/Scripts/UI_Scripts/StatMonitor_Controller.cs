using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatMonitor_Controller : MonoBehaviour
{
    [SerializeField] private Slider _timeLeftSlider;
    [SerializeField] private TMP_Text _timeLeftText;
    [SerializeField] private TMP_Text _ideaCardsValue;

    private PlayerStats_Manager _playerStats;

    [Inject]
    private void Construct(PlayerStats_Manager playerStats)
    {
        _playerStats = playerStats;
    }

    private void Start()
    {
        _timeLeftSlider.maxValue = _playerStats._playerSecMaxTimer;
    }

    private void Update()
    {
        _ideaCardsValue.text = _playerStats._jobCardsCount.ToString();
        _timeLeftText.text = "ВРЕМЕНИ ОСТАЛОСЬ: " + _playerStats._currentTimer.ToString("00");
        _timeLeftSlider.value = _playerStats._currentTimer;
    }
}
