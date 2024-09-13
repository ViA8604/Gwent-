using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public string lineNumberText = "Línea: 0"; 
    public string columnNumberText = "Columna: 0";
    // Start is called before the first frame update
    void Start()
    {
        inputField = gameObject.GetComponent<TMP_InputField>();
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
        inputField.textComponent.color = Color.white;
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

private void OnValueChanged(string text)
{
    int row = 0;
    int column = 0;

    // Calcular la línea y columna actual
    string[] lines = text.Split('\n');
    int caretPosition = inputField.caretPosition;
    int totalChars = 0;
    foreach (string line in lines)
    {
        if (caretPosition <= totalChars + line.Length)
        {
            column = caretPosition - totalChars;
            break;
        }
        totalChars += line.Length + 1;
        row++;
    }

    // Mostrar la línea y columna actual
    lineNumberText = "Línea: " + row.ToString();
    columnNumberText = "Columna: " + column.ToString();
}

}
