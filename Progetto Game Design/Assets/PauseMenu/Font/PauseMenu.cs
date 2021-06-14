using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] public GameObject Pause_Panel;
    [SerializeField] public GameObject Controls_Panel;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }

    }

    public void Resume() {
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
}
