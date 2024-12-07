using System.Collections;
using UnityEngine;

public class player_Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the sprite
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isLoaded = true;
    public GameObject objectToSpawn;
    private Animator anim;
    private Vector2 facingDirection = Vector2.right; // Default facing direction

    public game_controller controller;

    public GameObject gameoverPanel;


    public AudioClip shootSound;  
    public AudioClip collideSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameoverPanel.SetActive(false);
    }

    void Update()
    {
        // Get input from arrow keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update the facing direction based on the player's movement
        if (movement != Vector2.zero)
        {
            facingDirection = movement.normalized; // Normalize to get a unit vector
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }



        if (Input.GetMouseButtonDown(0) && isLoaded) // Left mouse button
        {
            audioSource.PlayOneShot(shootSound);
            // Set the direction for the object to spawn
            objectToSpawn.GetComponent<pin_controll>().direction = facingDirection;

            // Determine spawn position
            Vector3 position = gameObject.transform.position;

            // Instantiate the object
            Instantiate(objectToSpawn, position, Quaternion.identity);
            anim.SetBool("isShoot", true);
            isLoaded = false;
            StartCoroutine("delayBullet");
        }
        else
        {
            anim.SetBool("isShoot", false);
        }
    }

    void FixedUpdate()
    {
        // Move the sprite
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
       if(coll.gameObject.tag == "enemy")
       {
            audioSource.PlayOneShot(collideSound);
            gameoverPanel.SetActive(true);
            StartCoroutine("delayPlayerDeaad");
        } 
    }

    IEnumerator delayBullet()
    {
        yield return new WaitForSeconds(1);
        isLoaded = true;
    }

    IEnumerator delayPlayerDeaad()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        controller.enabled = false;
    }
}
