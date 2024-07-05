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
    void Start()
    {
        Crows = GameObject.Find("SelectCrows").GetComponent<Button>();
        Suns = GameObject.Find("SelectSuns").GetComponent<Button>();
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
        Debug.Log("Crows button clicked");
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>().sidename = "Crows";
        // Perform desired action when Crows button is clicked
    });

        Suns.onClick.AddListener(() =>
        {
            Debug.Log("Suns button clicked");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>().sidename = "Suns";
            // Perform desired action when Suns button is clicked
        });
    }
}
