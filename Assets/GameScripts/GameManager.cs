using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GwentCompiler;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace GwentPro
{
    public class GameManager : MonoBehaviour
    {
        static Player[] Players;
        public Player currentPlayer => Players[currentTurn];
        GameObject Player1Obj;
        GameObject Player2Obj;
        public CardGameScene cardGame;
        GameButton gameButton;
        public SkipButton skipbutton;
        int currentTurn;
        int roundsCounter;
        bool lastPlayerSkipped;
        bool sceneChanged = false;
        public bool setupComplete;
        public bool gamestarted;
        public GameObject CompilerZone;
        private static GameManager instance;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            Players = new Player[2];
            currentTurn = 0;
            roundsCounter = 0;

            GameObject chooseinfo = GameObject.FindGameObjectWithTag("GameController");
            gameButton = chooseinfo.GetComponent<GameButton>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "CardGameScene")
            {
                // Create the CardGameScene object in the loaded scene
                GameObject canvas = GameObject.Find("Canvas");
                gameObject.transform.SetParent(canvas.transform);
                Player1Obj.transform.SetParent(canvas.transform);
                Player2Obj.transform.SetParent(canvas.transform);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("CardGameScene"));
                CardGameScene();
            }
        }

        public void Update()
        {
            if (!setupComplete)
            {
                CreatePlayers();
            }

            if (setupComplete && !sceneChanged)
            {
                if (Player1Obj != null && Players[0].alreadyset && Player2Obj != null && Players[1].alreadyset)
                {
                    SceneManager.LoadScene("CardGameScene");
                    DontDestroyThisObj();
                    sceneChanged = true;
                }
            }
            if (sceneChanged && !gamestarted)
            {
                CardGameScene(); // Call the CardGameScene function
                gamestarted = true;
            }

            if (gamestarted && SceneManager.GetActiveScene().name == "CardGameScene")
            {
                if (currentPlayer.alreadyplayed)
                {
                    HorizontalLayoutGroup layoutGroup = currentPlayer.GetGOByName("Hand", currentPlayer.PlayerZones).GetComponent<HorizontalLayoutGroup>();


                    currentPlayer.alreadyplayed = false;
                    lastPlayerSkipped = false;
                    ChangeTurn();
                }

                skipbutton = cardGame.SkipButton;
                if (skipbutton != null)
                {
                    if (skipbutton.Clicked)
                    {
                        currentPlayer.Skippedturns++;
                        skipbutton.Clicked = false;
                        if (lastPlayerSkipped)
                        {
                            EndRound();
                        }
                        lastPlayerSkipped = true;
                        ChangeTurn();
                    }
                }

            }
        }
        public void CreatePlayers()
        {
            if (Player1Obj == null)
            {
                CreatePlayer("Player1", out Player1Obj, out Players[0], gameButton.yoursidename, 0);
                GameBuilder(Players[0]);
            }

            if (Player1Obj != null && Players[0].alreadyset && Player2Obj == null)
            {
                CreatePlayer("Player2", out Player2Obj, out Players[1], gameButton.othersidename, 1);
                GameBuilder(Players[1]);
                setupComplete = true;
            }
        }
        public void CreatePlayer(string playerName, out GameObject playerObj, out Player player, string side, int id)
        {
            playerObj = new GameObject(playerName);
            player = playerObj.AddComponent<Player>();
            player.path_to_data = side;
            playerObj.tag = player.name;
            player.id = id;
        }

        public static void GameBuilder(Player player)
        {
            RedrawScene(player);
        }
        void DontDestroyThisObj()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(Player1Obj);
            DontDestroyOnLoad(Player2Obj);
        }

        public static void RedrawScene(Player player)
        {
            GameObject RedrawObj = new GameObject("RedrawObj");
            RedrawScene redrawScene = RedrawObj.AddComponent<RedrawScene>();
            redrawScene.player = player;
        }

        public void CardGameScene()
        {
            // Find the canvas in the new scene
            GameObject canvas = GameObject.Find("Canvas");

            // Create the CardGameObj in the new scene
            GameObject CardGameObj = new GameObject("CardGameObj");
            CardGameObj.transform.SetParent(canvas.transform);


            // Add the CardGameScene component to the CardGameObj
            cardGame = CardGameObj.AddComponent<CardGameScene>();

            // Assign the players to the card game object
            cardGame.player1 = Players[0];
            cardGame.player2 = Players[1];
        }

        void ChangeTurn()
        {
            cardGame.ChangeCamera();
            currentTurn = (currentTurn + 1) % 2;
        }

        void EndRound()
        {
            GetRoundWinner();
            SetNextRound();
            if (roundsCounter >= 3)
            {
                EndGame();
            }
        }
        void SetNextRound()
        {
            foreach (var player in Players)
            {
                player.Skippedturns = 0;
                player.CleanPlayerZones();
                cardGame.SetCardScore(player, 0);
                player.Add2Hand();
            }
            roundsCounter++;
            lastPlayerSkipped = false;
        }

        void GetRoundWinner()
        {
            int pointsP1 = cardGame.GetCardScore(Players[0]);
            int pointsP2 = cardGame.GetCardScore(Players[1]);

            if (pointsP1 > pointsP2)
            {
                cardGame.ShowWinnerText(Players[0].fname + "  ganan");
                Players[0].Rounds++;
            }
            else if (pointsP1 < pointsP2)
            {
                cardGame.ShowWinnerText(Players[1].fname + "  ganan");
                Players[1].Rounds++;
            }
            else
            {
                cardGame.ShowWinnerText("EMPATE");
            }
        }


        void EndGame()
        {
            if (Players[0].Rounds > Players[1].Rounds)
            {
                cardGame.ShowWinnerText("Jugador 1" + " gana \n la partidaa");
            }
            else if (Players[0].Rounds < Players[1].Rounds)
            {
                cardGame.ShowWinnerText("Jugador 2" + " gana \n la partidaa");
            }
            else
            {
                cardGame.ShowWinnerText("EMPATEEE");
            }
            SceneManager.LoadScene("EndGameScene");
        }

        // CompilerAccessMethods ______________________________________________________________________________________________________________________________________________________________________________________        

        public int GetCurrentPlayerNum()
        {
            return currentTurn;
        }

        public int NotCurrentPlayerID()
        {
            return (currentTurn + 1) % 2;
        }
        public GameObject GetZone(ZoneObj zoneObj)
        {
            Debug.Log(zoneObj.ZoneName);
            Player player = Players[zoneObj.PlayerID];

            CompilerZone = player.GetGOByName(zoneObj.ZoneName, player.PlayerZones);
            if(zoneObj.ZoneName == "Board")
            {
                CompilerZone = GameObject.Find("Board");
            }
            return CompilerZone;
        }

        public List<CardClass> GetZoneCardList(ZoneObj zoneObj)
        {
            GameObject zone = GetZone(zoneObj);

            List<CardClass> list = new List<CardClass>();

            if (zone.transform.childCount == 0)
            {

                Debug.LogError($"No cards in the {zoneObj.ZoneName}");
            }

            foreach (Transform child in zone.transform)
            {
                if(child.GetComponent<CardClass>() != null)
                {
                list.Add(child.gameObject.GetComponent<CardClass>());
                }
                else 
                {
                    Debug.LogError($"No card in {zoneObj.ZoneName}");
                    Debug.Log($"Llamando a GetZoneCardList en {child.name}");
                    list.AddRange(GetZoneCardList(new ZoneObj(child.name, zoneObj.PlayerID)));
                }
            }
            return list;
        }

        private List<CardClass> GetFieldCardList(int playerID)
        {
            List<CardClass> fieldCards = new List<CardClass>();
            ZoneObj[] zones = new ZoneObj[] { new ZoneObj("Melee", playerID) , new ZoneObj("Range" , playerID) , new ZoneObj("Siege" , playerID)};
            foreach (var zone in zones)
            {
                fieldCards.Concat(GetZoneCardList(zone));
            }
            return fieldCards;
        }

        public string GetACardProperty(string cardName, ZoneObj zone, string property)
        {
            CardClass card = SearchCard(cardName, zone);
            return card.GetCardProperty(property);
        }

        public void SetACardProperty(string cardName, ZoneObj zone, string property, string value)
        {
            CardClass card = SearchCard(cardName, zone);
            card.SetCardProperty(property, value);
        }

        private CardClass SearchCard(string cardName, ZoneObj zone)
        {
            List<CardClass> cards = GetZoneCardList(zone);

            foreach (var card in cards)
            {
                if (card.name == cardName)
                {
                    return card;
                }
            }
            throw new InvalidOperationException($"Card {cardName} not found in the zone {zone.ZoneName}");
        }

        public Player GetPlayer(int number)
        {
            return Players[number];
        }

        public void ShuffleInstantiatedZone(ZoneObj zone)
        {
            HorizontalLayoutGroup layoutGroup = GetZone(zone).GetComponent<HorizontalLayoutGroup>();
            List<Transform> HandCards = new List<Transform>();
            for (int i = 0; i < layoutGroup.transform.childCount; i++)
            {
                HandCards.Add(layoutGroup.transform.GetChild(i));
            }

            HandCards = HandCards.OrderBy(x => UnityEngine.Random.value).ToList();

            for (int i = 0; i < HandCards.Count; i++)
            {
                HandCards[i].SetSiblingIndex(i);
            }
        }

        public List<CompilerCard> GetZoneCardNames(ZoneObj zone)
        {
            List<CardClass> cards = GetZoneCardList(zone);
            List<CompilerCard> cardnames = new List<CompilerCard>();

            foreach (var item in cards)
            {   
                cardnames.Add(new CompilerCard(item.name, zone));
            }
            return cardnames;
        }

        public CompilerCard PopCard(ZoneObj zone)
        {
            GameObject takefrom = GetZone(zone);

            ZoneObj backBoardZone = new ZoneObj("BackTable", zone.PlayerID);

            CardClass card = takefrom.transform.GetChild(0).gameObject.GetComponent<CardClass>();
            card.gameObject.transform.SetParent(GetZone(backBoardZone).transform);

            return new CompilerCard(card.name, backBoardZone);
        }

        public void RemoveCard(string cardName, ZoneObj zone)
        {
            CardClass card = SearchCard(cardName, zone);
            Destroy(card.gameObject);
        }

        public CompilerCard PushCard(CompilerCard startcard, ZoneObj endzone)
        {
            CardClass card = SearchCard(startcard.cardName, startcard.zone);
            GameObject actualZone = GetZone(endzone);
    
            ZoneObj actualZoneObj = new ZoneObj(actualZone.name, endzone.PlayerID);
            card.gameObject.transform.SetParent(actualZone.transform);
            card.cardGame = card.player.crdgameobj.GetComponent<CardGameScene>();

            return new CompilerCard(card.name, actualZoneObj);
        }

        public CompilerCard SendBottomCard(CompilerCard startcard, ZoneObj endzone)
        {
            CardClass card = SearchCard(startcard.cardName, startcard.zone);
            GameObject actualZone = GetZone(endzone);

            ZoneObj actualZoneObj = new ZoneObj(actualZone.name, endzone.PlayerID);
            card.gameObject.transform.SetParent(actualZone.transform);
            card.gameObject.transform.SetAsFirstSibling();

            return new CompilerCard(card.name, actualZoneObj);
        }
    }
}