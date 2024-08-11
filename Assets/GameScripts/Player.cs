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
        List<GameObject> CardsInScene;

        void Start()
        {
            Path_to_data();
            AssignPlayerDeck();
        }
        void Update()
        {
            if (PlayerZones != null && !displayedhand)
            {
                Debug.Log("Went into the display hand task");
                SetHand();
                displayedhand = true;
            }
        }

        void Path_to_data()
        {
            if (fname == "Crows")
            {
                path_to_data = "CrowData";
            }
            else
            {
                path_to_data = "SunData";
            }
        }


        void AssignPlayerDeck()
        {
            DeckObj = new GameObject(fname + "Deck");
            DeckObj.transform.SetParent(transform); // Set the parent of DeckObj to the current GameObject
            deck = DeckObj.AddComponent<Deck>();
            deck.path_to_data = path_to_data;
        }


        void SetHand()
        {
            GameObject handObj = GetGOByName("Hand", PlayerZones);
            GameObject leaderObj = GetGOByName("LeaderCard", PlayerZones);
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
            //The issue with the leader card is probably here
            GameObject leaderinst = Instantiate(deck.LeaderCard, leaderObj.transform);
            leaderinst.transform.SetParent(leaderObj.transform);
            leaderinst.transform.localScale = new Vector3(4.35f, 4.35f, 3.35f);


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
