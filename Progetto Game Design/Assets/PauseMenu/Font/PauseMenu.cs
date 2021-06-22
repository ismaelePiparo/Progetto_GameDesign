using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] public GameObject Pause_Panel;
    [SerializeField] public GameObject Controls_Panel;
    [SerializeField] public GameObject Leave_Button;

    public void Awake()
    {
        if(SceneManager.GetActiveScene().name=="Livello_1" || SceneManager.GetActiveScene().name == "Accampamento"  || SceneManager.GetActiveScene().name == "BossFinale")
        {
            Leave_Button.SetActive(true);
        }
        else
        {
            Leave_Button.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused)
            {
                Cursor.visible = false;
                Resume();
                CloseControlsPanel();            
            }
            else {
                Cursor.visible = true;
                Pause();
            }
        }

    }

    public void Resume() {
        Cursor.visible = false;
        Pause_Panel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        Pause_Panel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadControls() {
        Controls_Panel.SetActive(true);
    }

    public void QuitGame() {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void CloseControlsPanel(){
        Controls_Panel.SetActive(false);
    }

    public void Leave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
