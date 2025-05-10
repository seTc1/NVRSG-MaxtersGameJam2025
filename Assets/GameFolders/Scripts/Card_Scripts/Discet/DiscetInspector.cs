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

    private JobDiscet_Controller currentDiscet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var discet = other.GetComponent<JobDiscet_Controller>();
        if (discet == null) return;
        currentDiscet = discet;
        inspectWindow.SetActive(true);
        var data = discet.GetType()
                         .GetField("_jobDiscetData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                         ?.GetValue(discet) as JobDiscet_Data;
        jobName.text = data._JobName;
        jobDescription.text = data._JobDescription;
        responsibilitySlider.value = data._responsibility;
        responsibilityValue.text = data._responsibility + "/10";
        communicationSlider.value = data._communication;
        communicationValue.text = data._communication + "/10";
        stressSlider.value = data._stressResistance;
        stressValue.text = data._stressResistance + "/10";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (currentDiscet == null) return;
        other.transform.position = Vector2.MoveTowards(
            other.transform.position,
            discetSlotPoint.position,
            attractionSpeed * Time.deltaTime
        );
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<JobDiscet_Controller>() == currentDiscet) 
        {
            inspectWindow.SetActive(false);
            currentDiscet = null;
        }
    }
}
