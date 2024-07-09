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
        public Toggle MusicToggle;
        public Toggle ScreenToggle;
        public TMP_Dropdown resolutionDropdown;

        void Start()
        {
            resolutionDropdown = Dropdown.GetComponent<TMP_Dropdown>();
            // Add listener for when the dropdown value changes
            resolutionDropdown.onValueChanged.AddListener(delegate
            {
                OnResolutionChange(resolutionDropdown);
            });

            // Add listener for when the ScreenToggle value changes
            ScreenToggle.onValueChanged.AddListener(delegate
            {
                OnScreenToggleChange(ScreenToggle);

                // Initialize the screen mode based on the initial value of ScreenToggle
                OnScreenToggleChange(ScreenToggle);
            });
        }

        void Update()
        {
            SetButton.onClick.AddListener(() =>
            {
                GameObject rawImage = SetButton.transform.parent.GameObject();
                rawImage.gameObject.SetActive(false);
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

        void OnScreenToggleChange(Toggle toggle)
        {
            Screen.fullScreen = toggle.isOn;
        }
    }
}
