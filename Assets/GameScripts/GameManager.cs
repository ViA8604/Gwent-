using UnityEngine;
using UnityEngine.SceneManagement;

namespace GwentPro
{
    public class GameManager : MonoBehaviour
    {
        GameObject Player1Obj;
        GameObject Player2Obj;
        static Player Player1;
        static Player Player2;
        GameButton gameButton;
        bool sceneChanged = false;
        int turncounter;
        public bool setupComplete;
        public bool gamestarted;
        CardGameScene cardGame;
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
            GameObject chooseinfo = GameObject.FindGameObjectWithTag("GameController");
            gameButton = chooseinfo.GetComponent<GameButton>();

            SceneManager.sceneLoaded += OnSceneLoaded;
            turncounter = 0;
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
                if (Player1Obj != null && Player1.alreadyset && Player2Obj != null && Player2.alreadyset)
                {
                    SceneManager.LoadScene("CardGameScene");
                    DontDestroyOnLoad(gameObject);
                    DontDestroyOnLoad(Player1Obj);
                    DontDestroyOnLoad(Player2Obj);
                    sceneChanged = true;
                }
            }
            if (sceneChanged && !gamestarted)
            {
                Debug.Log("Called CardGame function");
                CardGameScene(); // Call the CardGameScene function
                gamestarted = true;
            }

            if (gamestarted)
            {
                if (Player1.cardplayed || Player2.cardplayed)
                {
                    ChangeTurn();
                }
            }
        }



        public void CreatePlayers()
        {
            if (Player1Obj == null)
            {
                CreatePlayer("Player1", out Player1Obj, out Player1, gameButton.yoursidename);
                GameBuilder(Player1);
            }

            if (Player1Obj != null && Player1.alreadyset && Player2Obj == null)
            {
                CreatePlayer("Player2", out Player2Obj, out Player2, gameButton.othersidename);
                GameBuilder(Player2);
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
            cardGame.player1 = Player1;
            cardGame.player2 = Player2;


        }

        void ChangeTurn()
        {
           
           /* if (turncounter == 0)
            {
                Player2.cardplayed = false;     //If you restore the cardplayed, the cards become playeble again.
            }
            else if (turncounter == 1)
            {
                Player1.cardplayed = false;
            }
            cardGame.ChangeCamera();
            turncounter = (turncounter + 1) % 2; // Alternates between 0 & 1
            */
        }
    }

}