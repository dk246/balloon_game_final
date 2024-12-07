using UnityEngine;

public class pin_controll : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the pin's movement
    public Vector2 direction; // Start moving to the right

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        // Move the pin horizontally


        // Reverse direction when reaching screen edges
        if (transform.position.x > 30) // Assuming a screen width of 16 units
        {
            Destroy(gameObject);
        }
    }
}
