using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GwentPro;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class RedrawScene : MonoBehaviour
    {

        public Player player;
        public GameObject Positioner;
        public RawImage image;
        // Start is called before the first frame update
        void Start()
        {
            Positioner = GameObject.FindGameObjectWithTag("Player");
            SetBackImage();
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