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
        string fname;
        bool turn;
        GameObject deckhold;        //TAg


        void Start()
        {

            Faction faction = new Faction(fname);

        }


        public void CreateDeck(Faction faction)
        {
            deckhold = new GameObject("Deckhold");
            Deck DeckComp = deckhold.AddComponent<Deck>();
            DeckComp.deckfaction = faction;

        }


    }


    public class Faction
    {
        public string factionname;
        public Color normalcolor;
        public Color darkborder;

        public Faction(string factionname)
        {
            factionname = this.factionname;
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
