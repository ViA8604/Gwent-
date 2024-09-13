using System;
using System.Collections;
using System.Collections.Generic;
using GwentCompiler;
using GwentPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompileText : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI CompilationEnding;
    public TextMeshProUGUI GuideLines;
    InputManager inputInfo;
    public Button PlayButton;
    CompilerHandler Compiler;
    string textToCompile;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        textToCompile = inputField.text;
        PlayButton.enabled = false;
        Compiler = GameObject.FindGameObjectWithTag("Compiler").GetComponent<CompilerHandler>();
        inputInfo = inputField.GetComponent<InputManager>();
        DontDestroyOnLoad(Compiler);
    }

    public void Compile()
    {
        string tag = GameObject.Find("GameScenes").GetComponent<GameButton>().yoursidename;
        try
        {
            Compiler.mycompiler = new GwentCompiler.GwentCompiler(inputField.text, tag);
            CompilationResults(true);
        }
        catch (Exception e)
        {
            CompilationEnding.text = e.Message;
            CompilationResults(false);
        }

    }

    public void CompilationResults(bool success)
    {
        if (success)
        {
            CompilationEnding.text = "Compilation Successful" ;
            PlayButton.enabled = true;
        }
        else
        {
            PlayButton.enabled = false;
        }
    }
    void Update()
    {
        GuideLines.text = inputInfo.lineNumberText + "\n" + inputInfo.columnNumberText;
    }
}
