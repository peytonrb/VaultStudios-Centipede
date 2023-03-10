using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    public float speed = 10f;
    public GameObject projectile;
    Vector2 fireDirection = new Vector2(0, 1);
    private int lives = 3;
    private Vector2 startPosition;
    public GameObject player;
    public GameObject heart1, heart2, heart3;
    private int totalScore = 0;
    public TextMeshProUGUI scoreText;
    private int round = 0;

    // Audio
    private AudioSource source;
    public AudioClip bubble;
    public AudioClip hurt;

    void Start() {
        round++;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);

        source = GetComponent<AudioSource>();

        changeScore(totalScore);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
            source.PlayOneShot(bubble);
        }
    }

    void FixedUpdate() {

        // accessing Input Manager to allow player controls
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x = position.x + speed * moveDirection.x * Time.deltaTime; // frame independent
        position.y = position.y + speed * moveDirection.y * Time.deltaTime;
        transform.position = position;

        rigidbody.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Spider" || collision.collider.tag == "Centipede") {
            lives--;
            source.PlayOneShot(hurt);

            if (lives > 0) {
                transform.position = startPosition;
    
                if (lives == 2) {
                    heart1.gameObject.SetActive(false);
                } else if (lives == 1) {
                    heart2.gameObject.SetActive(false);
                }

            } else {
                heart3.gameObject.SetActive(false);
                Destroy(gameObject);
                SceneManager.LoadScene(2);
            }
        }
    }

    public void changeScore(int score) {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public int getRound() {
        return round;
    }
}
