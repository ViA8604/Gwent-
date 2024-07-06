using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu_Control : MonoBehaviour
{
    public RawImage backImage;
    public RawImage upImage;
    public RawImage settingsImage;
    public Button playbutton;
    public Button optionbutton;
    public Button exitbutton;
    // Start is called before the first frame update
    void Start()
    {
            // Desactiva el Raw Image
            upImage.gameObject.SetActive(false);
            playbutton.gameObject.SetActive(false);
            optionbutton.gameObject.SetActive(false);
            exitbutton.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Desactiva el Raw Image
            upImage.gameObject.SetActive(true);
            playbutton.gameObject.SetActive(true);
            optionbutton.gameObject.SetActive(true);
            exitbutton.gameObject.SetActive(true);
        }
        optionbutton.onClick.AddListener(() =>
        {
            settingsImage.gameObject.SetActive(true);
        });
    }
}
