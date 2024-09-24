using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GwentPro
{
    public class Player : MonoBehaviour
    {
        public string fname;
        public int id;
        public string path_to_data;
        public GameObject DeckObj;
        public Deck deck;
        public bool alreadyset;
        public bool alreadyplayed;
        public GameObject crdgameobj;
        public List<GameObject> PlayerZones;
        bool displayedhand;
        public int Skippedturns;
        public int Rounds;
        List<GameObject> CardsInScene;

        void Start()
        {
            AssignPlayerDeck();
        }
        void Update()
        {
            if (PlayerZones != null && !displayedhand)
            {
                SetHand();
                displayedhand = true;
            }
        }



        void AssignPlayerDeck()
        {
            DeckObj = new GameObject("Deck");
            DeckObj.transform.SetParent(transform); // Set the parent of DeckObj to the current GameObject
            deck = DeckObj.AddComponent<Deck>();
            deck.path_to_data = path_to_data;
        }


        void SetHand()
        {
            GameObject crdgameobj = GameObject.Find("CardGameObj");
            GameObject handObj = GetGOByName("Hand", PlayerZones);
            foreach (GameObject item in deck.hand)
            {
                GameObject cardinst = Instantiate(item, handObj.transform);
                CardClass cardC = cardinst.GetComponent<CardClass>();
                cardC.player = this;
                cardC.cardGame = crdgameobj.GetComponent<CardGameScene>();
                cardC.SetDictProp();
                cardinst.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                cardinst.transform.SetParent(handObj.transform);

                CardsInScene = new List<GameObject>
                {
                    cardinst
                };
            }

            GameObject LeaderObj = GetGOByName("Leader", PlayerZones);
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject leaderinst = Instantiate(deck.LeaderCard, LeaderObj.transform);
            CardClass leaderC = leaderinst.GetComponent<CardClass>();
            leaderC.player = this;
            leaderC.cardGame = crdgameobj.GetComponent<CardGameScene>();
            leaderC.SetDictProp();
            leaderinst.transform.localScale = new Vector3(1.25f, 1.3f, 1.6f);
            leaderinst.transform.SetParent(LeaderObj.transform);

        }

        public void Add2Hand()
        {
            GameObject handObj = GetGOByName("Hand", PlayerZones);
            for (int i = 0; i < 2; i++)
            {
                if (deck.prefabsList.Count > 0 & handObj.transform.childCount < 10)
                {
                    GameObject cardinst = Instantiate(Deck.PopCard(deck.prefabsList), handObj.transform);
                    cardinst.GetComponent<CardClass>().player = this;
                    cardinst.GetComponent<CardClass>().cardGame = crdgameobj.GetComponent<CardGameScene>();
                    cardinst.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                    cardinst.transform.SetParent(handObj.transform);
                    CardsInScene.Add(cardinst);
                }
            }
        }
        public GameObject GetGOByName(string name, List<GameObject> collection)
        {
            foreach (GameObject go in collection)
            {
                if (go.name.Contains(name))
                {
                    return go;
                }
            }
            return null;
        }

        public void FillDeck()
        {
            DeckObj = GetGOByName("Deck", PlayerZones);
           
            foreach (var item in deck.prefabsList)
            {
                GameObject deckInst = Instantiate(item, DeckObj.transform);
                CardClass deckC = deckInst.GetComponent<CardClass>();
                deckC.player = this;
                deckC.cardGame = crdgameobj.GetComponent<CardGameScene>();
                deckC.SetDictProp();
                deckInst.transform.localScale = new Vector3(1.44f, 1.44f, 1.44f);
                deckInst.transform.SetParent(DeckObj.transform);
            }
            Destroy(deck.gameObject);
        }

        public void CleanPlayerZones()
        {
            foreach (GameObject go in PlayerZones)
            {
                if (go.name.Contains("Zone"))
                {
                    go.GetComponent<CombatZone>().ClearZone();
                }
            }

        }
    }
    public class Faction
    {
        public string factionname;
        public Color normalcolor;
        public Color darkborder;

        public Faction(string factionname)
        {
            this.factionname = factionname;
            if (factionname == "Crows")
            {
                normalcolor = new Color(12f / 255f, 35f / 255f, 179f / 255f);
                darkborder = new Color(28f / 255f, 42f / 255f, 71f / 255f);
            }
            else if (factionname == "Suns")
            {
                normalcolor = new Color(5f / 255f, 153f / 255f, 54f / 255f);
                darkborder = Color.black;
            }
            else
            {
                normalcolor = Color.black;
                darkborder = Color.grey;
            }
        }
    }
}
