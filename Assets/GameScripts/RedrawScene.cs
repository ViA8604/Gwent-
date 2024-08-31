using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GwentPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GwentPro
{
    public class RedrawScene : MonoBehaviour
    {

        public Player player;
        public GameObject Positioner;
        public Button SelectButton;
        public RawImage image;
        public bool Picked;
        // Start is called before the first frame update
        void Start()
        {
            Positioner = GameObject.FindWithTag("Player");
            SelectButton = GameObject.Find("ChooseButton").GetComponent<Button>();
            SetBackImage();

            // Add the onClick listener here to ensure it's added only once
            if (SelectButton != null)
            {
                SelectButton.onClick.AddListener(() =>
                {
                    Picked = true;
                    player.alreadyset = true;

                });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        void SetBackImage()
        {
            GameObject rawObj = GameObject.Find("RawImage");
            if (rawObj != null)
            {
                image = rawObj.GetComponent<RawImage>();

                if (player.fname == "Crows")
                {
                    image.texture = Resources.Load<Texture2D>("Backgrounds/CrowsRedraw");
                }
                else if (player.fname == "Suns")
                {
                    image.texture = Resources.Load<Texture2D>("Backgrounds/SunsRedraw");
                }
                else
                {
                    image.texture = Resources.Load<Texture2D>("Backgrounds/NeutralRedraw");
                }
            }
        }

    }
}