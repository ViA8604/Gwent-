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
    public class Deck : MonoBehaviour   // Represents a deck of cards in the game when added to a GameObject.
    {
        public Faction deckfaction;
        public string path_to_data;
        RedrawScene redrawScene;       // Reference to the RedrawScene object
        bool timetochange;
        public GameObject LeaderCard;
        public List<GameObject> prefabsList;
        List<GameObject> showedcards;
        public List<GameObject> hand = new List<GameObject>();


        void Start()
        {
            prefabsList = CardModerator.PrefabsList(path_to_data);
            redrawScene = GameObject.Find("RedrawObj").GetComponent<RedrawScene>();
            LeaderCard = PopCard(prefabsList);
            showedcards = DeckBuilder(prefabsList);
        }

        void Update()
        {
            if (redrawScene.Picked && timetochange)
            {
                showedcards = FinalDeck(showedcards);
                DestroyHand(showedcards);
                redrawScene.player.alreadyset = true;
                redrawScene.Picked = false;
                timetochange = false;
            }
        }


        List<GameObject> DeckBuilder(List<GameObject> prefabsList)
        {
            // Builds the initial deck of cards.
            Shuffle(prefabsList);
            return MakeHand(prefabsList);
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

        List<GameObject> MakeHand(List<GameObject> prefabsList)
        {
            List<GameObject> scenecards = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                if (prefabsList.Count > 0)
                {
                    GameObject displayedCard = PopCard(prefabsList);
                    hand.Add(displayedCard);
                    List<Vector3> positions = new List<Vector3>(); // Create a list to store positions
                    GetPosition(out positions); // Pass the list of positions
                    if (positions.Count > 0)
                    {
                        Vector3 position = positions[i % positions.Count]; // Get the position based on index and available positions
                        GameObject instantiatedCard = Instantiate(displayedCard, position, Quaternion.identity);
                        ResizeInstance(instantiatedCard);
                        scenecards.Add(instantiatedCard);
                    }
                    else
                    {
                        Debug.LogWarning("No positions found. Unable to instantiate the card.");
                    }
                }
            }
            timetochange = true;
            return scenecards;
        }

        List<GameObject> FinalDeck(List<GameObject> collection)
        {
            List<GameObject> selectedCards = new List<GameObject>();
            List<Vector3> positions = new List<Vector3>();

            if (collection.Count != 0)
            {
                foreach (GameObject item in collection)
                {
                    if (PeekCardInvoked(item) && selectedCards.Count < 2)
                    {
                        selectedCards.Add(item);
                        positions.Add(item.transform.position);
                    }
                }

                foreach (GameObject selectedCard in selectedCards)
                {
                    int i = collection.IndexOf(selectedCard);
                    collection.Remove(selectedCard);
                    Destroy(selectedCard);

                    GameObject newCard = PopCard(prefabsList);

                    // Instantiate new card and set position
                    GameObject instantiatedCard = Instantiate(newCard, positions[selectedCards.IndexOf(selectedCard)], Quaternion.identity);
                    ResizeInstance(instantiatedCard);
                    collection.Add(instantiatedCard);
                    // Add the new card to the hand list (prefabs only)
                    hand.RemoveAt(i);
                    hand.Add(newCard);
                }

                return collection;
            }
            else
            {
                return collection;
            }
        }

        void GetPosition(out List<Vector3> positions)
        {
            positions = new List<Vector3>(); // List to store positions

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "RedrawScene")
            {
                GameObject[] positioners = GameObject.FindGameObjectsWithTag("Player"); // Find all GameObjects with the "Player" tag
                foreach (GameObject positioner in positioners)
                {
                    positions.Add(positioner.transform.position); // Add the position of each GameObject to the list
                }

                if (positions.Count == 0)
                {
                    Debug.LogWarning("No GameObjects with the 'Player' tag found.");
                }
            }
        }


        public GameObject PopCard(List<GameObject> Mazo)
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
                instance.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
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

        static void DestroyHand(List<GameObject> hand)
        {
            foreach (GameObject card in hand)
            {
                Destroy(card);
            }
        }

    }

}
