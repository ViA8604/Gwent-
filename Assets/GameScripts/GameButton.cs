using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public string sidename;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void GoToGameSn ()
    {
        SceneManager.LoadScene("ChooseFactionScene");
    }
    public void GoToGameRedSn ()
    {
        SceneManager.LoadScene("RedrawScene");
        DontDestroyOnLoad(gameObject);
    }
    public void GoToCreatorSn ()
    {
        SceneManager.LoadScene("EditorScene");
        DontDestroyOnLoad(gameObject);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
