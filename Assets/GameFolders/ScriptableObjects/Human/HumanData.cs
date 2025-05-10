using UnityEngine;

[CreateAssetMenu(fileName = "HumanData", menuName = "Scriptable Objects/HumanData")]
public class HumanData : ScriptableObject
{
    [Header("=== Identity ===")]
    public string characterName;

    [Header("=== Skills ===")]
    [Range(1, 10)] public int responsibility;
    [Range(1, 10)] public int communication;
    [Range(1, 10)] public int stressResistance;

    [Header("=== Timers ===")]
    public float burnoutTimerMax = 70f;
    public float idleTimerMax = 90f;
}