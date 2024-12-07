using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of the balloon's movement
    public Vector2 moveDirection = new Vector2(1, 0); // Initial movement direction

    private float screenWidth;
    private float screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        // Calculate the screen bounds in world space
        Camera mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the balloon is outside the screen bounds and reverse direction
        Vector3 position = transform.position;

        if (position.x > screenWidth || position.x < -screenWidth)
        {
            moveDirection.x = -moveDirection.x; // Flip horizontal direction
            position.x = Mathf.Clamp(position.x, -screenWidth, screenWidth); // Keep within bounds
        }

        if (position.y > screenHeight || position.y < -screenHeight)
        {
            moveDirection.y = -moveDirection.y; // Flip vertical direction
            position.y = Mathf.Clamp(position.y, -screenHeight, screenHeight); // Keep within bounds
        }

        transform.position = position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
