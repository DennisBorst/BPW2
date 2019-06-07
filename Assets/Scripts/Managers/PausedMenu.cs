using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PausedMenu : MonoBehaviour
{

    public GameObject ui;
    private Character character;
    private BowScript bow;

    private void Start()
    {
        character = FindObjectOfType<Character>();
        bow = FindObjectOfType<BowScript>();
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
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                character.enabled = false;
                bow.enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                character.enabled = true;
                bow.enabled = true;
            }
        }
    }

    public void MainMenu(string levelName)
    {
        Toggle();
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
