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
    Button Crows;
    Button Suns;
    GameButton side;
    void Start()
    {
        Crows = GameObject.Find("SelectCrows").GetComponent<Button>();
        Suns = GameObject.Find("SelectSuns").GetComponent<Button>();
        side = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>();

    }
    public void ShowButtons()
    {

        GameObject canvas = GameObject.Find("Canvas");
        InstPlay = Instantiate(PlayButton, canvas.transform);
        InstEdit = Instantiate(EditorButton, canvas.transform);
        GameButton gameButton = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>();

        InstPlay.onClick.AddListener(gameButton.GoToGameRedSn);
        InstEdit.onClick.AddListener(gameButton.GoToCreatorSn);

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
    }
}
