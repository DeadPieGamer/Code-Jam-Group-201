using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Felix.Settings
{
    public class AudioVolumeinator : MonoBehaviour
    {
        [SerializeField, Tooltip("Name of the value to be changed")] private string valueName;
        [Space(8f), Header("Don't change the following")]
        [SerializeField, Tooltip("The base value of the volume")] private float baseValue = 1f;
        [SerializeField, Tooltip("Slider reference to change that to represent the loaded value")] private Slider mySlider;
        [SerializeField, Tooltip("Inputfield to represent value as #")] private TMP_InputField inputField;
        [SerializeField, Tooltip("Inputfield placeholder text, where to show #")] private TextMeshProUGUI inputValue;
        [SerializeField, Tooltip("Text field to write name of slider")] private TextMeshProUGUI nameField;
        [SerializeField, Tooltip("The mixer which will have its volume changed")] private AudioMixer mixer;

        /// <summary>
        /// Changes mixer volume and updates input field
        /// </summary>
        /// <param name="newVolume"></param>
        public void ChangeMixerVolume(float newVolume)
        {
            // Clamping
            newVolume = newVolume > 1f ? 1f : newVolume;
            newVolume = newVolume < 0.0001f ? 0.0001f : newVolume;

            UpdateInputField(newVolume);

            // Updates newVolume to one that works for the audio
            newVolume = Mathf.Log10(newVolume) * 20;
            mixer.SetFloat(valueName, newVolume);
        }

        /// <summary>
        /// Changes mixer volume and updates slider
        /// </summary>
        /// <param name="inputString"></param>
        public void ChangeMixerVolume(string inputString)
        {
            float inputAsNum = float.Parse(inputString) / 100f;

            // Clamping
            inputAsNum = inputAsNum > 1f ? 1f : inputAsNum;
            inputAsNum = inputAsNum < 0.0001f ? 0.0001f : inputAsNum;

            UpdateFields(inputAsNum);
            Debug.LogFormat("Input is: {0}", inputAsNum);

            // Updates newVolume to one that works for the audio
            float newVolume = Mathf.Log10(inputAsNum) * 20;

            mixer.SetFloat(valueName, newVolume);
        }

        /// <summary>
        /// Saves the volume level to a Player Pref
        /// </summary>
        private void SaveSettings()
        {
            PlayerPrefs.SetFloat(valueName, mySlider.value);
        }

        /// <summary>
        /// Loads the set value and makes visuals represent it
        /// </summary>
        private void LoadSettings()
        {
            UpdateFields(PlayerPrefs.GetFloat(valueName, baseValue));
        }

        /// <summary>
        /// Updates all fields to show the new value
        /// </summary>
        /// <param name="newValue"></param>
        private void UpdateFields(float newValue)
        {
            UpdateSlider(newValue);
            UpdateInputField(newValue);
        }

        /// <summary>
        /// Updates value written in Input Field
        /// </summary>
        private void UpdateInputField(float newValue)
        {
            inputField.text = "";
            inputValue.text = string.Format("{0:0}%", newValue * 100);
        }

        /// <summary>
        /// Updates value showed on Slider
        /// </summary>
        /// <param name="newValue"></param>
        private void UpdateSlider(float newValue)
        {
            mySlider.value = newValue;
        }

        // Start is called before the first frame update
        private void Start()
        {
            LoadSettings();
            // Set the name of the slider
            nameField.text = valueName;
        }

        // Whenever this becomes disabled, such as the menu being closed, save the settings
        private void OnDisable()
        {
            SaveSettings();
        }
    }
}
