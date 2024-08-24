using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                CreatePlayer("Player1", out Player1Obj, out Players[0], gameButton.yoursidename);
                GameBuilder(Players[0]);
            }

            if (Player1Obj != null && Players[0].alreadyset && Player2Obj == null)
            {
                CreatePlayer("Player2", out Player2Obj, out Players[1], gameButton.othersidename);
                GameBuilder(Players[1]);
                setupComplete = true;
            }
        }
        public void CreatePlayer(string playerName, out GameObject playerObj, out Player player, string side)
        {
            playerObj = new GameObject(playerName);
            player = playerObj.AddComponent<Player>();
            player.fname = side;
            playerObj.tag = player.name;
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
                cardGame.ShowWinnerText(Players[0].fname + " ganan \n la partidaa");
            }
            else if (Players[0].Rounds < Players[1].Rounds)
            {
                cardGame.ShowWinnerText(Players[1].fname + " ganan \n la partidaa");
            }
            else
            {
                cardGame.ShowWinnerText("EMPATEEE");
            }
        }
    }
}