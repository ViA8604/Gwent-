using System;
using System.Collections;
using System.Collections.Generic;
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
        bool turn;
        GameObject DeckObj;
        Deck deck;


        void Start()
        {
            Path_to_data();
            DeckObj = new GameObject(fname + "Deck");
            deck = DeckObj.AddComponent<Deck>();
            deck.path_to_data = path_to_data;

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
