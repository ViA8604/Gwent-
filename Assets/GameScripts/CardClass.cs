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
        Material material;
        protected float newCardWidth = 0.2194931f;
        protected float newcardHeight = 0.2167f;
        protected float newCardLength = 1.7259f;
        public Faction faction;

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
        public void Awake()
        {
            ResizeCardObj();
            faction = new Faction(gameObject.tag);
            Material materialTemplate = Resources.Load<Material>("material/BorderCard");
            if (materialTemplate == null)
            {
                Debug.Log("Material not loaded");
                return;
            }

            material = new Material(materialTemplate);
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