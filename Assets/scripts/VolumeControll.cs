using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        // Initialize slider value from VolumeManager
        volumeSlider.value = volumeManager.Instance.Volume;

        // Add listener to detect slider value change
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        volumeManager.Instance.Volume = value;

        // Adjust audio for all AudioSources
        AudioListener.volume = value; // Controls overall volume
    }
}
