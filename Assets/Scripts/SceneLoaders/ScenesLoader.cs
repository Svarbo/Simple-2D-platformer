using UnityEngine;
using IJunior.TypedScenes;

public class ScenesLoader : MonoBehaviour
{
    public void LoadGame()
    {
        Game.Load();
    } 

    public void LoadMainMenu()
    {
        MainMenu.Load();
    }

    public void LoadAuthorsInformation()
    {
        AuthorsInformation.Load();
    }
}
