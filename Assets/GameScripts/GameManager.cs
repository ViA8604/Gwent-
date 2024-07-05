using UnityEngine;

namespace GwentPro
{
  public class GameManager : MonoBehaviour
  {
    GameObject Player1Obj;
    GameObject Player2Obj;
    static Player Player1;
    static Player Player2;
    GameButton gameButton;
    int turncounter;
    void Start()
    {
      GameObject chooseinfo = GameObject.FindGameObjectWithTag("GameController");
      gameButton = chooseinfo.GetComponent<GameButton>();

      CreatePlayer("Player1", out Player1Obj, out Player1);
      GameBuilder();


    }


    public void CreatePlayer(string playerName, out GameObject playerObj, out Player player)
    {
      playerObj = new GameObject(playerName);
      player = playerObj.AddComponent<Player>();
      player.fname = gameButton.sidename;
      playerObj.tag = player.name;
    }
    public static void GameBuilder()
    {
      RedrawScene(Player1);
    }

    public static void RedrawScene(Player player)
    {
      GameObject RedrawObj = new GameObject("RedrawObj");
      RedrawScene redrawScene = RedrawObj.AddComponent<RedrawScene>();
      redrawScene.player = player;
    }
    /*  public bool CheckIsYourTurn(string tagobject)
      {

      }
    */
  }

}
