using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GwentPro;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class CombatZone : MonoBehaviour
    {
        public List<GameObject> CardsInScene;
        public CardClass.combatype cmbtype;
        public TMPro.TextMeshProUGUI PointsCounter;

        // Start is called before the first frame update
        void Start()
        {
            CardsInScene = new List<GameObject>();
            PointsCounter = SetPointCounter(gameObject.tag);
        }

        void Update()
        {
        }

        public void AddCardPoints(GameObject card)
        {
            int sum = Convert.ToInt32(PointsCounter.text) + card.GetComponent<CardClass>().cardpoint;
            PointsCounter.text = sum.ToString();
        }
        public TextMeshProUGUI SetPointCounter(string tag)
        {
            if (PointsCounter == null)
            {
                TextMeshProUGUI[] Objs = GameObject.FindObjectsOfType<TextMeshProUGUI>();
                foreach (TextMeshProUGUI obj in Objs)
                {
                    if (obj.tag == tag)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            CardClass card = collision.gameObject.GetComponent<CardClass>();
            if (card != null)
            {
                card.actualzone = gameObject;
            }

        }

        void OnCollisionStay2D(Collision2D collision)
        {
            CardClass card = collision.gameObject.GetComponent<CardClass>();
            if (card != null)
            {
                card.actualzone = gameObject;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            CardClass card = collision.gameObject.GetComponent<CardClass>();
            if (card != null)
                card.actualzone = null;
        }

        public void ClearZone()
        {
            foreach (Transform child in gameObject.transform)
            {
                foreach (Component component in child.GetComponents<Component>())
                {
                    if (component is UnityEngine.UI.Image || component is RawImage)
                    {
                        Destroy(component);
                    }
                }
                Destroy(child.gameObject);
            }
        }
    }
}
