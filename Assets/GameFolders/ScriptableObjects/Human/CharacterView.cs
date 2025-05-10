using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Header("=== UI ===")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Slider idleSlider;
    [SerializeField] private GameObject _infoCanvas;
    
    [Header("=== Responsibility ===")]
    [SerializeField] private TMP_Text responsibilityValue;
    [SerializeField] private Slider responsibilitySlider;

    [Header("=== Communication ===")]
    [SerializeField] private TMP_Text communicationValue;
    [SerializeField] private Slider communicationSlider;

    [Header("=== Stress Resistance ===")]
    [SerializeField] private TMP_Text stressValue;
    [SerializeField] private Slider stressSlider;


    private CharacterInstance instance;

    private static CharacterView currentOpened;

    public void Bind(CharacterInstance character)
    {
        instance = character;
        nameText.text = character.Data.characterName;

        idleSlider.maxValue = character.Data.idleTimerMax;
        idleSlider.value = character.IdleTimer;

        responsibilitySlider.maxValue = 10;
        responsibilitySlider.value = character.Data.responsibility;
        responsibilityValue.text = character.Data.responsibility + "/10";

        communicationSlider.maxValue = 10;
        communicationSlider.value = character.Data.communication;
        communicationValue.text = character.Data.communication + "/10";

        stressSlider.maxValue = 10;
        stressSlider.value = character.Data.stressResistance;
        stressValue.text = character.Data.stressResistance + "/10";
        
        _spriteRenderer.sprite = character._humanSprite;
    }


    private void Update()
    {
        if (instance != null)
            idleSlider.value = instance.IdleTimer;
    }

    public CharacterInstance GetInstance()
    {
        return instance;
    }
    
    public void CloseInfoCanvas()
    {
        _infoCanvas.SetActive(false);
        if (currentOpened == this)
        {
            currentOpened = null;
        }
    }


    private void OnMouseDown()
    {
        if (currentOpened != null && currentOpened != this)
        {
            currentOpened._infoCanvas.SetActive(false);
        }

        bool isActive = _infoCanvas.activeSelf;
        _infoCanvas.SetActive(!isActive);

        currentOpened = _infoCanvas.activeSelf ? this : null;
    }
}