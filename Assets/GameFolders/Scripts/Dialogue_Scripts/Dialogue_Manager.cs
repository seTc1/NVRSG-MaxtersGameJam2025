using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    [Header("=== UI Elements ===")]
    [SerializeField] private float _animSpeedMiltiplier;
    [SerializeField] private GameObject _dialogueEntry;
    [SerializeField] private GameObject _skipBox;

    [Header("=== Transforms ===")]
    [SerializeField] private Transform _characterPreviewSpawnTransform;

    [Header("=== Text Settings ===")]
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private TMP_Text _namingText;

    private GameObject _characterPreviewObject;
    private int _currentDialogueIndex;
    private bool _isTyping;
    private bool _isPhraseCasted;
    private DialogueData _currentDialogueData;
    private AudioClip[] _characterSounds;
    private AudioSource _audioSource;
    private GameEvent_Manager _gameEventManager;

    [Inject]
    private void Construct(GameEvent_Manager gameEventManager)
    {
        _gameEventManager = gameEventManager;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _dialogueEntry.SetActive(false);
    }

    private void Update()
    {
        _skipBox.SetActive(!_isPhraseCasted);
        if (_dialogueEntry.activeSelf && !_isPhraseCasted)
        {
            _characterPreviewObject.GetComponent<Animator>().SetBool("talking", _isTyping);
            if (Input.GetKeyDown(KeyCode.X) && !_isTyping)
            {
                ShowNextDialogueEntry();
                _audioSource.Stop();
            }
            else if (Input.GetKeyDown(KeyCode.X) && _isTyping)
            {
                StopAllCoroutines();
                _dialogueText.text = _currentDialogueData.dialogueEntries[_currentDialogueIndex].dialogueText;
                _isTyping = false;
                _currentDialogueIndex++;
            }
        }
    }
    public void StartDialogue(DialogueData dialogueData)
    {
        _isPhraseCasted = false;
        _currentDialogueData = dialogueData;
        _currentDialogueIndex = 0;
        _dialogueEntry.SetActive(true);
        TriggerGameEvent(dialogueData.startEventID);

        ShowNextDialogueEntry();
    }
    
    

    private void ShowNextDialogueEntry()
    {
        if (_currentDialogueIndex < _currentDialogueData.dialogueEntries.Length)
        {
            var dialogueEntry = _currentDialogueData.dialogueEntries[_currentDialogueIndex];
            RefreshCharacterPreview(dialogueEntry._characterData._characterPreview);
            _namingText.text = dialogueEntry._characterData.characterName;
            _characterSounds = dialogueEntry._characterData.characterSounds;
            StopAllCoroutines();
            TriggerGameEvent(dialogueEntry.phraseEventID);
            StartCoroutine(TypeDialogueWrapper(dialogueEntry.dialogueText, dialogueEntry.textSpeed, _characterSounds, dialogueEntry._soundPitch));
        }
        else
        {
            EndDialogue();
        }
    }

    public void SetCastDialogue(DialogueData dialogueData)
    {
        _currentDialogueData = dialogueData;
    }

    public void CastPhrase(int phraseIndex)
    {
        _isPhraseCasted = true;
        _dialogueEntry.SetActive(true);
        TriggerGameEvent(_currentDialogueData.startEventID);
        if (phraseIndex < _currentDialogueData.dialogueEntries.Length)
        {
            var dialogueEntry = _currentDialogueData.dialogueEntries[phraseIndex];
            RefreshCharacterPreview(dialogueEntry._characterData._characterPreview);
            _namingText.text = dialogueEntry._characterData.characterName;
            _characterSounds = dialogueEntry._characterData.characterSounds;
            StopAllCoroutines();
            TriggerGameEvent(dialogueEntry.phraseEventID);
            StartCoroutine(TypePhraseWrapper(dialogueEntry.dialogueText, dialogueEntry.textSpeed, _characterSounds, dialogueEntry._soundPitch));
        }
    }

    private IEnumerator TypeText(string text, float speed, AudioClip[] charSounds, float soundPitch)
    {
        _isTyping = true;
        float animationSpeed = _animSpeedMiltiplier / speed;
        _characterPreviewObject.GetComponent<Animator>().speed = animationSpeed;
        _dialogueText.text = "";
        string richTextBuffer = "";
        bool insideTag = false;

        for (int i = 0; i < text.Length; i++)
        {
            char letter = text[i];
            if (letter == '<')
            {
                insideTag = true;
            }
            richTextBuffer += letter;
            _dialogueText.text = richTextBuffer;
            if (letter == '>' && insideTag)
            {
                insideTag = false;
            }
            if (!insideTag)
            {
                if (letter == '.')
                {
                    _characterPreviewObject.GetComponent<Animator>().speed = 0;
                    yield return new WaitForSeconds(0.3f);
                }
                else if (letter == ',')
                {
                    _characterPreviewObject.GetComponent<Animator>().speed = 0;
                    yield return new WaitForSeconds(0.2f);
                }
                else if (letter == ' ')
                {
                    _characterPreviewObject.GetComponent<Animator>().speed = 0;
                }
                else 
                {
                    _characterPreviewObject.GetComponent<Animator>().speed = animationSpeed;
                    if (charSounds != null && charSounds.Length > 0)
                    {
                        int randomIndex = Random.Range(0, charSounds.Length);
                        if (_currentDialogueData.dialogueEntries[_currentDialogueIndex]._characterData._isPitchable)
                        {
                            _audioSource.pitch = soundPitch + Random.Range(-0.1f, 0.1f);
                        }
                        _audioSource.PlayOneShot(charSounds[randomIndex]);
                    }
                    yield return new WaitForSeconds(speed);
                }
            }
        }
        _isTyping = false;
        _characterPreviewObject.GetComponent<Animator>().speed = 1;
    }



    private IEnumerator TypePhraseWrapper(string text, float speed, AudioClip[] charSounds, float soundPitch)
    {
        yield return StartCoroutine(TypeText(text, speed, charSounds, soundPitch));
        StartCoroutine(EndPhrase());
    }

    private IEnumerator TypeDialogueWrapper(string text, float speed, AudioClip[] charSounds, float soundPitch)
    {
        yield return StartCoroutine(TypeText(text, speed, charSounds, soundPitch));
        _currentDialogueIndex++;
    }

    private IEnumerator EndPhrase()
    {
        yield return new WaitForSeconds(3);
        EndDialogue();
    }

    public void EndDialogue()
    {
        
        StopAllCoroutines();
        if (_characterPreviewObject != null)
        {
            Destroy(_characterPreviewObject);
        }
        _isPhraseCasted = false;
        TriggerGameEvent(_currentDialogueData.endEventID);
        _dialogueEntry.SetActive(false);
    }

    private void RefreshCharacterPreview(GameObject characterPreviewPrefab)
    {
        if (_characterPreviewObject != null)
        {
            Destroy(_characterPreviewObject);
        }
        _characterPreviewObject = Instantiate(characterPreviewPrefab, _characterPreviewSpawnTransform).gameObject;
    }

    private void TriggerGameEvent(int eventId)
    {
        if (eventId > 0)
        {
            _gameEventManager.TriggerEvent(eventId);
        }
    }
}
