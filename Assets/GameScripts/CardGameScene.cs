using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using Unity.VisualScripting;



namespace GwentPro
{
    public class CardGameScene : MonoBehaviour
    {
        public GameObject BoardObj;
        public Player player1;
        public Player player2;
        public SkipButton SkipButton;
        TextMeshProUGUI[] Counters;
        Canvas canvas;
        public TextMeshProUGUI WinnerSign;
        public CompilerHandler Compiler;
        List<GameObject> ZonesPlayer1;
        List<GameObject> ZonesPlayer2;
        GameObject LeaderObj1;
        GameObject LeaderObj2;
        public GameObject DeckObj;
        public Camera dragcamera;
        bool cardGameScenefound;
        bool cameramoved;
        bool winTextActive;
        // Start is called before the first frame update
        void Start()
        {
            dragcamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas = GameObject.FindFirstObjectByType<Canvas>();
            AddCounters();
            BoardObj = GameObject.Find("Board");
            ZonesPlayer1 = new List<GameObject>();
            ZonesPlayer2 = new List<GameObject>();
            FillZones();
            WinnerSign = Resources.Load<TextMeshProUGUI>("GamePrefabs/WinnerSign");
            player1.PlayerZones = ZonesPlayer1;
            player1.crdgameobj = gameObject;
            player2.crdgameobj = gameObject;
            player2.PlayerZones = ZonesPlayer2;
            LeaderObj1 = player1.GetGOByName("Leader", ZonesPlayer1);
            LeaderObj2 = player2.GetGOByName("Leader", ZonesPlayer2);
        }

        //Camera position: (954.00, 518.70, -96.90)
        //Camera rotation: (-0.05680, 0.00000, 0.00000, 0.99839)

        //Camera position: (954.10, 582.20, -88.20)
        //Camera rotation: (0.00024, 0.13996, -0.99016, 0.00048)

        // Update is called once per frame
        void AddCounters()
        {
            Counters = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        }
        public int GetCardScore(Player player)
        {
            //Recorre los contadores
            foreach (TextMeshProUGUI obj in Counters)
            {
                if (obj.tag == player.tag)
                {
                    return Convert.ToInt32(obj.text);
                }
            }
            return 0;
        }
        public void SetCardScore(Player player, int value)
        {
            foreach (TextMeshProUGUI obj in Counters)
            {
                if (obj.tag == player.tag)
                {
                    obj.text = value.ToString();
                }
            }
        }

        public void ShowWinnerText(string message)
        {
            WinnerSign.text = message;
            RotateText(Instantiate(WinnerSign, canvas.transform), new Vector2(27.4f, 0), new Vector2(-22.7f, 0));
            winTextActive = true;
        }

        void DestroyWinText()
        {
            Destroy(GameObject.Find("WinnerSign(Clone)"));
        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "CardGameScene" && !cardGameScenefound)
            {
                player1.FillDeck();
                player2.FillDeck();
                SkipButton = GameObject.Find("SkipObj").GetComponent<SkipButton>();

                cardGameScenefound = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (winTextActive)
                {
                    DestroyWinText();
                }
            }
        }


        void FillZones()
        {
            if (BoardObj != null)
            {
                if (BoardObj.transform.childCount != 0)
                {
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
                SkipButton.MoveButton(!cameramoved);
            }
            else
            {
                dragcamera.transform.position = new Vector3(954.00f, 518.70f, -96.90f);
                dragcamera.transform.rotation = new Quaternion(-0.05680f, 0.00000f, 0.00000f, 0.99839f);
                SkipButton.MoveButton(!cameramoved);
            }
            RotateCounters();
           // MoveLeaderPosition();
            cameramoved = !cameramoved;
        }

        void RotateCounters()
        {
            RotateText(Counters[0], new Vector2(-14.3f, -20.2f), new Vector2(-72.9f, -5.5f));
            RotateText(Counters[1], new Vector2(-23.4f, 6.3f), new Vector2(-63.6f, 24.9f));
        }
        void RotateText(TextMeshProUGUI textMesh, Vector2 normalpos, Vector2 rotatedpos)
        {
            RectTransform rect = textMesh.GetComponent<RectTransform>();
            if (cameramoved)
            {
                rect.localRotation = Quaternion.Euler(0, 0, 0);
                rect.localPosition = normalpos;
            }
            else
            {
                rect.localRotation = Quaternion.Euler(0, 0, 180);
                rect.localPosition = rotatedpos;

            }
        }
        void MoveLeaderPosition()
        {
            if (cameramoved)
            {
                LeaderObj1.transform.position = new Vector3(0f, 8.3f, 10f);
                LeaderObj2.transform.position = new Vector3(-1.2f, -0.2f, 10f);
            }
            else
            {
                LeaderObj1.transform.position = new Vector3(0f, 0f, 10f);
                LeaderObj2.transform.position = new Vector3(-1.2f, 6.8f, 10f);
            }
        }

    }
}