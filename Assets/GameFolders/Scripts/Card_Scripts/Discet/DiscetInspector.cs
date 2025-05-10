using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscetInspector : MonoBehaviour
{
    [Header("=== References ===")]
    [SerializeField] private Transform discetSlotPoint;
    [SerializeField] private GameObject inspectWindow;
    [SerializeField] private TMP_Text jobName;
    [SerializeField] private TMP_Text jobDescription;

    [Header("=== Responsibility ===")]
    [SerializeField] private TMP_Text responsibilityValue;
    [SerializeField] private Slider responsibilitySlider;

    [Header("=== Communication ===")]
    [SerializeField] private TMP_Text communicationValue;
    [SerializeField] private Slider communicationSlider;

    [Header("=== Stress Resistance ===")]
    [SerializeField] private TMP_Text stressValue;
    [SerializeField] private Slider stressSlider;

    [Header("=== Attraction Settings ===")]
    [SerializeField] private float attractionSpeed = 5f;

    private JobDiscet_Controller discetInZone;
    private bool isInspecting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (discetInZone != null) return;

        var candidate = other.GetComponent<JobDiscet_Controller>();
        if (candidate == null) return;

        discetInZone = candidate;
    }

    private void Update()
    {
        if (discetInZone == null) return;

        if (!isInspecting && !discetInZone._isDragging)
        {
            StartInspection(discetInZone);
        }

        if (isInspecting)
        {
            discetInZone.transform.position = Vector3.MoveTowards(
                discetInZone.transform.position,
                discetSlotPoint.position,
                attractionSpeed * Time.deltaTime
            );
        }
    }

    private void StartInspection(JobDiscet_Controller discet)
    {
        isInspecting = true;

        var field = typeof(JobDiscet_Controller).GetField("_jobDiscetData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var data = field?.GetValue(discet) as JobDiscet_Data;
        if (data == null) return;

        inspectWindow.SetActive(true);

        jobName.text = data._JobName;
        jobDescription.text = data._JobDescription;

        responsibilitySlider.value = data._responsibility;
        responsibilityValue.text = data._responsibility + "/10";

        communicationSlider.value = data._communication;
        communicationValue.text = data._communication + "/10";

        stressSlider.value = data._stressResistance;
        stressValue.text = data._stressResistance + "/10";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var exited = other.GetComponent<JobDiscet_Controller>();
        if (exited == discetInZone)
        {
            discetInZone = null;
            isInspecting = false;
            inspectWindow.SetActive(false);
        }
    }
}
