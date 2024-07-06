using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace GwentPro
{
    public class ResolutionScript : MonoBehaviour
    {
        public GameObject Dropdown;
        public Button SetButton;
        public TMP_Dropdown resolutionDropdown;

        void Start()
        {
            resolutionDropdown = Dropdown.GetComponent<TMP_Dropdown>();
            // Add listener for when the dropdown value changes
            resolutionDropdown.onValueChanged.AddListener(delegate
            {
                OnResolutionChange(resolutionDropdown);
            });
        }

        void Update ()
        {
            SetButton.onClick.AddListener(() =>
            {
                GameObject rawImage = SetButton.transform.parent.GameObject();
                //rawImage.gameObject.SetActive(fal)
            });
        }

        void OnResolutionChange(TMP_Dropdown dropdown)
        {
            // Get the selected resolution value from the dropdown
            string selectedResolution = dropdown.options[dropdown.value].text;

            // Split the selected resolution string into width and height
            string[] resolutionParts = selectedResolution.Split('x');

            // Check if the resolution string has the expected format
            if (resolutionParts.Length == 2 && int.TryParse(resolutionParts[0], out int width) && int.TryParse(resolutionParts[1], out int height))
            {
                // Set the game resolution based on the selected width and height
                Screen.SetResolution(width, height, Screen.fullScreen);
            }
            else
            {
                Debug.LogError("Invalid resolution format: " + selectedResolution);
            }
        }
    }
}
