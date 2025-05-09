using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class PrinterObject_Controller : MonoBehaviour
{
    [Header("=== UI Elements ===")]
    [SerializeField] private GameObject _printerUI;
    [SerializeField] private GameObject _mainCircle;

    [Header("=== Cameras ===")]
    [SerializeField] private GameObject _outerCam;
    [SerializeField] private GameObject _printerCam;

    [Header("=== Values ===")]
    [SerializeField] private float _increaseValue;
    [SerializeField] private float _decraseValue;

    private bool _isLookingAtMonitors;
    private Image _circleImage;
    private float _timer;

    private PlayerStats_Manager _playerStats;

    [Inject]
    private void Construct(PlayerStats_Manager playerStats)
    {
        _playerStats = playerStats;
    }
    private void Start()
    {
        _circleImage = _mainCircle.GetComponent<Image>();
        _circleImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (!_isLookingAtMonitors) return;

        _timer += Time.deltaTime;
        if (_timer >= 0.1f)
        {
            _timer = 0f;
            _circleImage.fillAmount = Mathf.Clamp01(_circleImage.fillAmount - _decraseValue);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _circleImage.fillAmount = Mathf.Clamp01(_circleImage.fillAmount + _increaseValue);
        }

        if (_circleImage.fillAmount >= 1f)
        {
            _circleImage.fillAmount = 0f;
            _playerStats._jobCardsCount++;  
        }
    }

    public void ChangeLook()
    {
        _circleImage.fillAmount = 0;
        _isLookingAtMonitors = !_isLookingAtMonitors;
        _outerCam.SetActive(!_outerCam.activeSelf);
        _printerCam.SetActive(!_printerCam.activeSelf);
        _printerUI.SetActive(_isLookingAtMonitors);
    }
}