using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausedMenu : MonoBehaviour
{

    public GameObject ui;
    private Character character;
    private BowScript bow;
    private Moving moving;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        bow = FindObjectOfType<BowScript>();
        moving = FindObjectOfType<Moving>();
    }

    void Update()
    {
        if (ui != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
            {
                Toggle();
            }
        }
    }

    public void Toggle()
    {

        if (ui != null)
        {
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                moving.enabled = false;
                character.enabled = false;
                bow.enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                moving.enabled = true;
                character.enabled = true;
                bow.enabled = true;
            }
        }
    }

    public void MainMenu(string levelName)
    {
        Toggle();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
