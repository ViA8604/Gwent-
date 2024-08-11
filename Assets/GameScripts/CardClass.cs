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
        public Player player;
        public bool isdragging;
        public GameObject actualzone;
        Material material;
        protected float newCardWidth = 0.2194931f;
        protected float newcardHeight = 0.2167f;
        protected float newCardLength = 1.7259f;
        public Faction faction;
        public Camera activecamera;
        private Vector3 offset;

        // Declare screenPoint and draggedposition as fields
        private Vector3 screenPoint;
        public Vector3 initialLocalPosition;


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
            Image image = gameObject.GetComponent<Image>();

            if (renderer != null && image != null)
            {
                renderer.material = material;
                image.material = material;
            }

        }

        void Start()
        {
            initialLocalPosition = transform.localPosition;
        }

        void Update()
        {
            if (selected)
            {
                material.SetColor("_BorderColor", faction.darkborder);
                SetActiveCamera();
                if (activecamera != null)
                {
                    // Calculate the offset only when the card is selected
                    offset = transform.position - activecamera.ScreenToWorldPoint(
                        new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)
                    );
                }
            }
            else
            {
                material.SetColor("_BorderColor", faction.normalcolor);

            }
        }
        void OnMouseDown()
        {
            selected = !selected;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position); // Obtiene la posición del objeto en coordenadas de pantalla
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); // Calcula el desplazamiento
            initialLocalPosition = transform.localPosition;
        }
        void OnMouseUp()
        {
            if (isdragging)
            {
                isdragging = false;
                selected = false;
                if (actualzone != null && actualzone.GetComponent<CombatZone>().cmbtype == cmbtype && player.gameObject.tag == actualzone.tag)
                {
                    gameObject.transform.SetParent(actualzone.transform);
                    actualzone = null;
                    player.alreadyplayed = true;
                }
                else
                {
                    // Check if the card has moved
                    Vector3 currentLocalPosition = transform.localPosition;
                    if (currentLocalPosition != initialLocalPosition)
                    {
                        // Card has moved, return it to the initial local position
                        transform.localPosition = initialLocalPosition;
                    }
                }
            }
        }
        void SetActiveCamera()
        {
            if (gameObject.scene.name == "RedrawScene" || cmbtype == combatype.Leader)
            {
                activecamera = null;
            }
            else
            {
                GameObject crdgameobj = GameObject.Find("CardGameObj");
                CardGameScene cardGame = crdgameobj.GetComponent<CardGameScene>();
                activecamera = cardGame.dragcamera;

            }
        }

        void OnMouseDrag()
        {
            if (activecamera != null)
            {
                isdragging = true;
                // Mientras se mantiene pulsado el botón izquierdo del ratón y se mueve:
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); // Obtiene la posición del cursor en coordenadas del mundo
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset; // Calcula la nueva posición del objeto
                transform.position = currentPosition; // Actualiza la posición del objeto
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