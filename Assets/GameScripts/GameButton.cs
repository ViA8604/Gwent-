using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToGameSn ()
    {
        SceneManager.LoadScene("ChooseFactionScene");
    }
    public void GoToGameRedSn ()
    {
        SceneManager.LoadScene("RedrawScene");
    }
    public void GoToCreatorSn ()
    {
        SceneManager.LoadScene("EditorScene");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
