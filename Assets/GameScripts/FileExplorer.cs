using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.IO;
using TMPro;

public class FileExplorer : MonoBehaviour
{
    string fileContent;
    bool fileloaded;
    public TMP_InputField inputField;

    void SetInputFieldText()
    {
        if (fileloaded)
        {
            inputField.text = fileContent;
        }
    }
    public void OpenFileBrowser()
    {
        // Ruta inicial del explorador de archivos
        string initialPath = Path.Combine(Application.dataPath, "");

        // Abre un diálogo de archivo para cargar un archivo .txt o .json
        FileBrowser.ShowLoadDialog(OpenFileBrowserCallback, null, FileBrowser.PickMode.Files, false, initialPath, null, "Seleccione un archivo .txt o .json", "Cargar");
    }
    void OpenFileBrowserCallback(string[] paths)
    {
        if (paths.Length > 0)
        {
            // Seleccionaste un archivo, puedes procesarlo aquí
            string filePath = paths[0];
            fileContent = File.ReadAllText(filePath);
            fileloaded = true;

            SetInputFieldText();
        }
    }



    public void SaveFile()
    {
        string filePath = Path.Combine(Application.dataPath, "test.txt");
        File.WriteAllText(filePath, fileContent);
        Debug.Log("Archivo guardado correctamente");
    }
}