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
        public string path_to_data;
        GameObject DeckObj;
        public Deck deck;
        public bool alreadyset;
        public bool alreadyplayed;
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
            GameObject handObj = GetGOByName("Hand", PlayerZones);
            foreach (GameObject item in deck.hand)
            {
                GameObject cardinst = Instantiate(item, handObj.transform);
                cardinst.GetComponent<CardClass>().player = this;
                cardinst.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                cardinst.transform.SetParent(handObj.transform);

                CardsInScene = new List<GameObject>
                {
                    cardinst
                };
            }
            GameObject LeaderObj = GetGOByName("Leader", PlayerZones);
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject leaderinst = Instantiate(deck.LeaderCard, canvas.transform);
            LeaderCard leaderc = leaderinst.GetComponent<LeaderCard>();
            leaderc.NewCardHeight = 1.6f;
            leaderc.NewCardLength = 0.5f;
            leaderc.transform.SetParent(LeaderObj.transform);

        }

        public void Add2Hand()
        {
            GameObject handObj = GetGOByName("Hand", PlayerZones);
            for (int i = 0; i < 2; i++)
            {
                if (deck.prefabsList.Count > 0 & handObj.transform.childCount < 10)
                {
                    GameObject cardinst = Instantiate(deck.PopCard(deck.prefabsList), handObj.transform);
                    cardinst.GetComponent<CardClass>().player = this;
                    cardinst.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                    cardinst.transform.SetParent(handObj.transform);
                    CardsInScene.Add(cardinst);
                }
            }
        }

        GameObject GetGOByName(string name, List<GameObject> collection)
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
