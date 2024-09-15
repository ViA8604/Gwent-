using System.Collections;
using System.Collections.Generic;
using GwentPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckView : MonoBehaviour
{
    string DeckName;
    public Button ReturnButton;

    // Start is called before the first frame update
    void Start()
    {
        DeckName = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>().yoursidename;
        ShowDeckCards();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowDeckCards()
    {
        List<GameObject> deckToShow = CardModerator.PrefabsList(DeckName);
        GameObject Content = GameObject.FindGameObjectWithTag("Respawn");
        foreach (var item in deckToShow)
        {
            GameObject deckInst = Instantiate(item, Content.transform);
            deckInst.transform.localScale = new Vector3(44f, 44f, 4f);
            deckInst.transform.SetParent(Content.transform);
        }
    }

}
