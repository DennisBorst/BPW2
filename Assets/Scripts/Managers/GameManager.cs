using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float restartDelay;
    [SerializeField] private bool menu;
    [SerializeField] private Animator anim;
   

    private void Start()
    {
        if (!menu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            anim.SetTrigger("FadeIn");
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            anim.SetTrigger("Fade");
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(RespawnTime());
    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
