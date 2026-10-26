using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public void BUTTON_PlayButton()
    {

        Loader.Load(Loader.Scene.CarGame);

    }

    public void BUTTON_Quit()
    {
        Loader.Quit();
    }

    public void BUTTON_MainMenuButton()
    {

        Loader.Load(Loader.Scene.MainMenu);

    }

}
