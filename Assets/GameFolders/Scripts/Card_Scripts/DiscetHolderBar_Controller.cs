using UnityEngine;

public class DiscetHolderBar_Controller : MonoBehaviour
{
    public static DiscetHolderBar_Controller Instance { get; private set; }

    [SerializeField] private GameObject[] _holderBarPoints;
    public GameObject[] HolderBarPoints => _holderBarPoints;

    private void Awake()
    {
        Instance = this;
    }
}