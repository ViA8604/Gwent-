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
        public List<GameObject> mazo1 = new List<GameObject>();
        public Faction deckfaction;

        void Start()
        {
            DeckBuilder(deckfaction);
        }




        void DeckBuilder(Faction deckfaction)
        {
            
        }

        static void PrefabGenerator(string cname, string fname)
        {
            GameObject card = new GameObject(cname);
            SpriteRenderer sr = card.AddComponent<SpriteRenderer>();
            Sprite cardimg = Resources.Load<Sprite>("CardImg" + "/" + fname + "/" + cname);
            sr.sprite = cardimg;
            string prefabPath = "Assets/MyPrefabs/Card" + cname + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(card, prefabPath);
            DestroyImmediate(card);
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
          
    }
}
