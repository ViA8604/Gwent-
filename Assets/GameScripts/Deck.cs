using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GwentPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class Deck : MonoBehaviour
    {

        public List<GameObject> mazo = new List<GameObject>();
        public Faction deckfaction;
        public string path_to_data;
        List<GameObject> prefabsList;


        void Start()
        {
            prefabsList = CardModerator.PrefabsList(path_to_data);

            DeckBuilder(prefabsList);
        }




        void DeckBuilder(List<GameObject> prefabsList)
        {
            Shuffle(prefabsList);
            MakeHand();

        }



        static void Shuffle(List<GameObject> mazo1)
        {
            System.Random alt = new System.Random();
            int n = mazo1.Count;
            while (n > 1)
            {
                n--;
                int chngednum = alt.Next(n + 1);
                GameObject valor = mazo1[chngednum];
                mazo1[chngednum] = mazo1[n];
                mazo1[n] = valor;
            }
        }

        public List<GameObject> MakeHand()
        {
            List<GameObject> hand = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                if (prefabsList.Count > 0)
                {
                    GameObject displayedCard = PopCard(prefabsList);
                    Vector3 position;
                    GetPosition(out position);
                    GameObject instantiatedCard = Instantiate(displayedCard, position, Quaternion.identity);
                    ResizeInstance(instantiatedCard);
                    hand.Add(instantiatedCard);
                }
            }
            return hand;
        }
        void GetPosition(out Vector3 position)
        {
            position = new Vector3(0, 0, 0); // Default assignment

            /*if (SceneManager.GetActiveScene().name == "RedrawScene")
            {
                GameObject positioner = GameObject.Find("RedrawObj").GetComponent<RedrawScene>().Positioner;
                if (positioner != null)
                {
                    for (int i = 0; i < positioner.transform.childCount; i++)
                    {
                        position = positioner.transform.GetChild(i).transform.position;

                    }
                }
                else
                {
                    Debug.LogWarning("RedrawObj not found or does not have RedrawScene component attached.");
                }
          */  }
        

        public static GameObject PopCard(List<GameObject> Mazo)
        {
            GameObject card = Mazo[0];
            Mazo.Remove(Mazo[0]);
            return card;
        }
        public static void ResizeInstance(GameObject instance)
        {
            //Para cartas con distinto tamaño
            CardClass checktype = instance.GetComponent<CardClass>();
            if (checktype.crdtype == CardClass.cardtype.Special)
            {
                // Reduce la escala a la mitad para el tipo especial
                instance.transform.localScale = new Vector3(0.08f, 0.13f, 0.2f);
            }
            else if (checktype.crdtype == CardClass.cardtype.Special && checktype.cmbtype == CardClass.combatype.Special)
            {
                instance.transform.localScale = new Vector3(0.12f, 0.2f, 0.2f);
            }
            else
            {
                // Reduce la escala a la mitad para los demás tipos
                instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }


        public static bool PeekCardInvoked(GameObject cardtocheck)
        {
            CardClass cardcomp = cardtocheck.GetComponent<CardClass>(); //OHE
            if (cardcomp.selected == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
