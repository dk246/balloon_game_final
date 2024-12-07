using UnityEngine;

public class volumeManager : MonoBehaviour
{
    public static volumeManager Instance;
    public float Volume = 1.0f; // Default volume

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
