using System;
using System.Collections;
using System.Collections.Generic;
using GwentPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompilerHandler : MonoBehaviour
{
    bool sceneLoaded;
    public GameManager gameManager;
    public GwentCompiler.GwentCompiler mycompiler;

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
        if (gameManager != null)
        {
            mycompiler.SetGameManager(gameManager);
        }
    }

    public void ActivateEffect(string cardName)
    {
        GwentCompiler.CompilerUtils.LaunchEffect(cardName);
    }

   
}
