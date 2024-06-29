using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickPlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button PlayButton;
    public Button EditorButton;
    Button InstPlay;
    Button InstEdit;
    void Start()
    {
        
    }
    public void ShowButtons()
    {
        GameObject canvas = GameObject.Find("Canvas");
        InstPlay = Instantiate(PlayButton, canvas.transform);
        InstEdit = Instantiate(EditorButton,canvas.transform);
        GameButton gameButton = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>();    
        
        InstPlay.onClick.AddListener(gameButton.GoToGameRedSn);
        InstEdit.onClick.AddListener(gameButton.GoToCreatorSn);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
