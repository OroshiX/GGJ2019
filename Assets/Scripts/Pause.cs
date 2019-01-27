using UnityEngine;

public class Pause : MonoBehaviour {
    public static bool gameIsPaused = false;
    [SerializeField]
    GameObject pauseMenuUI;
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        if (gameIsPaused)
        {
            Resume();
        } else
        {
            GamePause();
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void GamePause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
