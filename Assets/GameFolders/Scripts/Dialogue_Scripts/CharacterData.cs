using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public GameObject _characterPreview;
    public AudioClip[] characterSounds;
    public string characterName;
}
