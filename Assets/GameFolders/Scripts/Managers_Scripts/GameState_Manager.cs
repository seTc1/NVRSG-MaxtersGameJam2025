using System;
using UnityEngine;

public class GameState_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _fadeCanvasObject;

    public void LooseGame()
    {
        _fadeCanvasObject.GetComponent<Animator>().SetTrigger("fadeIn");
    }
}
