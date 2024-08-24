using System.Collections;
using System.Collections.Generic;
using GwentPro;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompileText : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button PlayButton;
    GameObject Compiler;
    string textToCompile;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        textToCompile = inputField.text;
        PlayButton.enabled = false;
        Compiler = GameObject.FindGameObjectWithTag("Compiler");
        DontDestroyOnLoad(Compiler);
    }

    public void Compile()
    {
        Debug.Log("Compiling");
        //Si no hay problemas de compilación
        CompilationResults(true);
    }

    public void CompilationResults(bool success)
    {
        if(success)
        {
            PlayButton.enabled = true;
        }
    }
}
