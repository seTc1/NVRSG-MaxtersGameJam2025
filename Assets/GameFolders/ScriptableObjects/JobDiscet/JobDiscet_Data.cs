using UnityEngine;

[CreateAssetMenu(fileName = "JobDiscet (1)", menuName = "CardsData/JobDiscet_Data")]
public class JobDiscet_Data : ScriptableObject
{
    [Header("=== Job Info ===")]
    public string _JobName;
    public string _JobDescription;

    [Header("=== Skills ===")]
    [Range(0, 10)] public int _responsibility;
    [Range(0, 10)] public int _communication;
    [Range(0, 10)] public int _stressResistance;
}