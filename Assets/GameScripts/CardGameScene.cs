using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace GwentPro
{
    public class CardGameScene : MonoBehaviour
    {
        GameObject BoardObj;
        public Player player1;
        public Player player2;
        public SkipButton SkipButton;
        public List<GameObject> ZonesPlayer1;
        List<GameObject> ZonesPlayer2;
        public Camera dragcamera;
        bool cameramoved;
        // Start is called before the first frame update
        void Start()
        {
            dragcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            BoardObj = GameObject.Find("Board");
            ZonesPlayer1 = new List<GameObject>();
            ZonesPlayer2 = new List<GameObject>();
            FillZones();
            player1.PlayerZones = ZonesPlayer1;
            player2.PlayerZones = ZonesPlayer2;
        }

        //Camera position: (954.00, 518.70, -96.90)
        //Camera rotation: (-0.05680, 0.00000, 0.00000, 0.99839)

        //Camera position: (954.10, 582.20, -88.20)
        //Camera rotation: (0.00024, 0.13996, -0.99016, 0.00048)

        // Update is called once per frame
        void Update()
        {

        }


        void FillZones()
        {
            if (BoardObj != null)
            {
                if (BoardObj.transform.childCount != 0)
                {
                    Debug.Log("boardObj got children");
                    foreach (Transform child in BoardObj.transform)
                    {
                        if (child.tag == "Player1")
                        {
                            ZonesPlayer1.Add(child.gameObject);
                        }
                        else if (child.tag == "Player2")
                        {
                            ZonesPlayer2.Add(child.gameObject);
                        }
                    }
                }
            }
        }

        public void ChangeCamera()
        {
            if (!cameramoved)
            {
                dragcamera.transform.position = new Vector3(954.10f, 582.20f, -88.20f);
                dragcamera.transform.rotation = new Quaternion(0.00024f, 0.13996f, -0.99016f, 0.00048f);
            }
            else
            {
                dragcamera.transform.position = new Vector3(954.00f, 518.70f, -96.90f);
                dragcamera.transform.rotation = new Quaternion(-0.05680f, 0.00000f, 0.00000f, 0.99839f);
            }
            cameramoved = !cameramoved;
        }
    }
}