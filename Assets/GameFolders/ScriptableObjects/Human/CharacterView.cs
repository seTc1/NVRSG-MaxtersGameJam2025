using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Slider idleSlider;

    private CharacterInstance instance;

    public void Bind(CharacterInstance character)
    {
        instance = character;
        nameText.text = character.Data.characterName;
        idleSlider.maxValue = character.Data.idleTimerMax;
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
}