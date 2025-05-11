using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _fadeCanvasObject;

    public void LooseGame()
    {
        Debug.Log("Loose Game");
        _fadeCanvasObject.GetComponent<Animator>().SetTrigger("fadeIn");
        StartCoroutine(loose());
    }

    public void WinGame()
    {
        Debug.Log("win Game");
        _fadeCanvasObject.GetComponent<Animator>().SetTrigger("fadeIn");
        StartCoroutine(win());
    }

    private IEnumerator loose()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator win()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("EndScene");
    }
}
