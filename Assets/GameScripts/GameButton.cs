using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public string yoursidename;
    public string othersidename;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GoToMenuSn()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToGameSn()
    {
        SceneManager.LoadScene("ChooseFactionScene");
        DontDestroyOnLoad(gameObject);
    }
    public void GoToGameRedSn()
    {
        SceneManager.LoadScene("RedrawScene");
        DontDestroyOnLoad(gameObject);
    }
    public void GoToCreatorSn()
    {
        SceneManager.LoadScene("EditorScene");
        DontDestroyOnLoad(gameObject);
    }

    public void GoToDeckViewScene()
    {
        SceneManager.LoadScene("DeckViewScene");
        DontDestroyOnLoad(gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
