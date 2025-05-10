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
    
    [SerializeField] private Slider _workerSliders;
    [SerializeField] private Slider _peopleSlider;
    
    
    private PlayerStats_Manager _playerStats;

    [Inject]
    private void Construct(PlayerStats_Manager playerStats)
    {
        _playerStats = playerStats;
    }

    private void Start()
    {
        _peopleSlider.maxValue = _playerStats._allPeople;
        _workerSliders.maxValue = _playerStats._allPeople;
        _timeLeftSlider.maxValue = _playerStats._playerSecMaxTimer;
    }

    private void Update()
    {
        _ideaCardsValue.text = _playerStats._jobCardsCount.ToString();
        _timeLeftText.text = "ВРЕМЕНИ ОСТАЛОСЬ: " + _playerStats._currentTimer.ToString("00");
        _timeLeftSlider.value = _playerStats._currentTimer;
        _peopleSlider.value = _playerStats._peopleAlive;
        _workerSliders.value = _playerStats._peopleWorking;
    }
}
