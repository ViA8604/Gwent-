using System;
using System.Collections;
using System.Collections.Generic;
using GwentPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Compiler : MonoBehaviour
{
    bool sceneLoaded;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CardGameScene" && !sceneLoaded)
        {
            GameObject canvas = GameObject.Find("Canvas");
            gameObject.transform.SetParent(canvas.transform);
            GameObject.Find("CardGameObj").GetComponent<CardGameScene>().Compiler = this;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            sceneLoaded = true;
        }
    }

   
}
