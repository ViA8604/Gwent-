using System.Collections;
using System.Collections.Generic;
using GwentPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Modulador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 1; i < 6; i++)
        //{
            PrefabGenerator(3.ToString(), "CrowsM");
      //  }



    }

    // Update is called once per frame
    void Update()
    {

    }
    static void PrefabGenerator(string cname, string fname)
    {
        GameObject card = new GameObject(cname);
        card.AddComponent<LeaderCard>();
        SpriteRenderer sr = card.AddComponent<SpriteRenderer>();
        Sprite cardimg = Resources.Load<Sprite>("CardImg" + "/" + fname + "/" + cname);
        sr.sprite = cardimg;
        string prefabPath = "Assets/MyPrefabs/Card" + cname + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(card, prefabPath);
        //DestroyImmediate(card);
    }
}
