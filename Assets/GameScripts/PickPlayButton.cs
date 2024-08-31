using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickPlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button PlayButton;
    public Button EditorButton;
    public Button CreateButton;
    GameButton gameButton;
    Button InstPlay;
    Button InstEdit;
    Button Crows;
    Button Suns;
    GameButton side;
    void Start()
    {
        Crows = GameObject.Find("SelectCrows").GetComponent<Button>();
        Suns = GameObject.Find("SelectSuns").GetComponent<Button>();
        CreateButton = GameObject.Find("CreateB").GetComponent<Button>();
        side = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>();
        gameButton = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>();


    }
    public void ShowButtons()
    {

        GameObject canvas = GameObject.Find("Canvas");
        InstPlay = Instantiate(PlayButton, canvas.transform);
        InstEdit = Instantiate(EditorButton, canvas.transform);
        InstPlay.onClick.AddListener(gameButton.GoToGameRedSn);
        InstEdit.onClick.AddListener(gameButton.GoToCreatorSn);

    }

    public void CreationButton()
    {
        side.yoursidename = "Custom";
        side.othersidename = "Suns";
        gameButton.GoToCreatorSn();
    }
    // Update is called once per frame
    void Update()
    {
        Crows.onClick.AddListener(() =>
    {
        side.yoursidename = "Crows";
        side.othersidename = "Suns";
        // Perform desired action when Crows button is clicked
    });

        Suns.onClick.AddListener(() =>
        {
            side.yoursidename = "Suns";
            side.othersidename = "Crows";
            // Perform desired action when Suns button is clicked
        });

        CreateButton.onClick.AddListener(() =>
            {
                side.yoursidename = "Custom";
                side.othersidename = "Suns";
            });
    }
}

