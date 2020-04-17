using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //Play - K-391, AlanWalker
    public void Play(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    //Quit - Cashmere Cat, Ariana Grande
    public void Quit()
    {
        Application.Quit();
    }

    public void OpenScreenShotsFolder()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            // Windows only (TESTED)
            System.Diagnostics.Process.Start("explorer.exe", "/root," + ScreenShotManager.GetScreenShotsFolderPath());
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            // Mac only (UNTESTED)
            System.Diagnostics.Process.Start("open", ScreenShotManager.GetScreenShotsFolderPath());
        }
    }
}
