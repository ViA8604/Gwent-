using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResetScripts : MonoBehaviour
{
    string scriptName;
    string filePath;
    void Start()
    {
        scriptName = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameButton>().yoursidename;
        filePath = "Assets/GameScripts/" + scriptName + ".json";
    }
    public void ResetScript()
    {
        File.WriteAllText(filePath, "");
        if (scriptName == "Crows" || scriptName == "Suns")
        {
            string fileContent = File.ReadAllText($"Assets/Resources/GamePrefabs/{scriptName}.json");
            File.WriteAllText(filePath, fileContent);
        }
    }
}
