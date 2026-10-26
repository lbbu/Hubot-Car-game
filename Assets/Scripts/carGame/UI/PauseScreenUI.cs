using UnityEngine;

public class PauseScreenUI : MonoBehaviour
{

    [SerializeField] private GameObject pauseScreen;

    private void Start()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void BUTTON_Quit()
    {
        Loader.Quit();
    }

    public void BUTTON_MainMenu()
    {

        Loader.Load(Loader.Scene.MainMenu);

    }

    public void BUTTON_Resume()
    {

        //hide pause screen, resume the timer, resume the game
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                //hide pause screen, resume the timer, resume the game
                Time.timeScale = 1.0f;
                pauseScreen.SetActive(false);

            }
            else
            {
                //show pause screen, stop the timer, stop the game
                pauseScreen.SetActive(true);
                Time.timeScale = 0.0f;
            }

        }
    }


}
