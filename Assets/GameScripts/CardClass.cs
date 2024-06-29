using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GwentPro
{
    public class CardClass : MonoBehaviour
    {
        public string cardname;
        public int cardpoint;
        public bool selected;
        public cardtype crdtype;
        public combatype cmbtype;
        public bool playable;
        public bool isdragging;
        public bool alreadyplayed;
        public Material material;
        public float newCardWidth = 0.2194931f;
        public float newcardHeight = 0.2167f;
        public float newCardLength = 1.7259f;
        Faction faction;

        public virtual float NewCardWidth
        {
            get { return newCardWidth; }
            set { newCardWidth = value; }
        }

        public virtual float NewCardLength
        {
            get { return newCardLength; }
            set { newCardLength = value; }
        }
        public virtual float NewCardHeight
        {
            get { return newcardHeight; }
            set { newCardWidth = value; }
        }
        void Start()
        {
            faction = new Faction("Crows");
            gameObject.AddComponent<BoxCollider2D>();
            ResizeCardObj();
            material = GameObject.Find("material").GetComponent<SpriteRenderer>().material;
            // Resources.Load<Material>("Material/BorderCard.mat");
            if (material == null)
            {
                Debug.Log("Material no cargado");
            }
            Debug.Log("Parece que cargó algo");
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }

            void Update()
            {
                if (selected)
                {
                    material.SetColor("_BorderColor", faction.darkborder);
                }
                else
                {
                    material.SetColor("_BorderColor", faction.normalcolor);
                }
            }   
        void OnMouseDown()
        {
            selected = !selected;
        }
        void OnMouseUp()
        {
            if (isdragging)
            {
                isdragging = false;
                selected = false;
            }
        }

        protected void ResizeCardObj()
        {
            Vector3 scale = gameObject.transform.localScale;
            scale.x = newCardWidth;
            scale.y = newcardHeight;
            scale.z = newCardLength;
            gameObject.transform.localScale = scale;
        }
        [System.Serializable]
        public enum combatype
        {
            Melee,
            Range,
            Siege,
            Leader,
            Special


        }

        [System.Serializable]
        public enum cardtype
        {
            Gold,
            Silver,
            Special

        }




    }
}