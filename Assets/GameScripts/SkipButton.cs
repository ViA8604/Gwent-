using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class SkipButton : MonoBehaviour
    {
        public bool Clicked;
        public Button skipButtonObj;
        // Start is called before the first frame update
        void Start()
        {
            skipButtonObj = GameObject.Find("SkipButton").GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {
            skipButtonObj.onClick.AddListener(() =>
            {
                Clicked = true;
            });
        }
        
        public void MoveButton(bool cameramoved)
        {
            if (cameramoved)
            {
                skipButtonObj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                skipButtonObj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
