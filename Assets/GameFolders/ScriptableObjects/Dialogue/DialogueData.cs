using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueEntry
    {
        public CharacterData _characterData;
        [TextArea(3, 10)]
        public string dialogueText;
        public float textSpeed = 0.65f;
        public float _soundPitch = 1;
        public int phraseEventID;
        
    }
    
    public int startEventID;
    public int endEventID;
    public DialogueEntry[] dialogueEntries;
}
