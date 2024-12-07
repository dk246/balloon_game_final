using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of the balloon's movement
    public Vector2 moveDirection = new Vector2(1, 0); // Initial movement direction

    private float screenWidth;
    private float screenHeight;

    public Vector3 initialSize = new Vector3(1, 1, 1); // Initial size of the balloon
    public Vector3 targetSizeIncrement = new Vector3(0.5f, 0.5f, 0.5f); // Amount to increase size
    private int sizeChangeCount = 0; // Counter for size changes
    private bool isChangingSize = false; // Flag to ensure size changes smoothly

    [SerializeField]private int life_balloon = 20;

    public GameObject particle;


    
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Calculate the screen bounds in world space
        Camera mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
        transform.localScale = initialSize; // Set initial size
        StartCoroutine(SizeChangeRoutine()); // Start the size change routine

    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
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

    IEnumerator SizeChangeRoutine()
    {
        while (sizeChangeCount < 3)
        {
            if (!isChangingSize)
            {
                isChangingSize = true;

                // Determine the new size
                Vector3 newSize = transform.localScale + targetSizeIncrement;

                // Smoothly interpolate to the new size over 15 seconds
                float elapsedTime = 0f;
                Vector3 originalSize = transform.localScale;

                while (elapsedTime < 1f)
                {
                    transform.localScale = Vector3.Lerp(originalSize, newSize, elapsedTime / 1f);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Ensure final size is set
                transform.localScale = newSize;

                // Update size change count and reset flag
                sizeChangeCount++;
                isChangingSize = false;
            }
            
            yield return new WaitForSeconds(3f); // Wait for 15 seconds before the next size change
        }

        audioSource.Play();
        Instantiate(particle, transform.position, Quaternion.identity);
        game_controller.balloonPops -= 1;
        game_controller.CurrentBalloonCount -= 1;

        // Destroy the balloon after 4 size changes
        StartCoroutine("destroyDelay");
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "bullet")
        {
            game_controller.CurrentBalloonCount -= 1;
            if (sizeChangeCount == 1)
            {
                life_balloon = 20; 
                Destroy(coll.gameObject);
            }
            else if (sizeChangeCount == 2)
            {
                life_balloon = 15;
                Destroy(coll.gameObject);
            }
            else if (sizeChangeCount == 3)
            {
                life_balloon = 5;
                Destroy(coll.gameObject);
            }
            audioSource.Play();
            game_controller.score += life_balloon;
            Instantiate(particle, transform.position, Quaternion.identity);

            StartCoroutine("destroyDelay");

        }
    }

    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
