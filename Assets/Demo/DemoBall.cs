using UnityEngine;
using System.Collections;
using System.Net.Mime;
using TMPro;
using System.Collections.Generic;

public class BallController3D : MonoBehaviour
{
    public float speed = 10f; 
    private Rigidbody rb; 
    private Vector3 direction; 
    private bool isWaiting = false; 
    public TextMeshProUGUI LeftScore;
    public TextMeshProUGUI RightScore;
    public Vector2 score = new Vector2(0, 0);
    
    public List<AudioSource> audioSources;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartMovement();
        LeftScore.text = "0";
        RightScore.text = "0";
        
    }

    void Update()
    {
        if (!isWaiting && (transform.position.x > 13 || transform.position.x < -13))
        {
            StartCoroutine(ResetBall());
        }
        LeftScore.text = score.x.ToString();
        RightScore.text = score.y.ToString();
    }

    void StartMovement()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-0.5f, 0.5f)).normalized;
        rb.linearVelocity = direction * speed;
    }

    IEnumerator ResetBall()
    {
        isWaiting = true; 
        rb.linearVelocity = Vector3.zero;
        speed = 10f;
        if (transform.position.x > 13)
        {
            score.y++;
        }
        else
        {
            score.x++;
        }
        audioSources[0].Play();
        LeftScore.text = score.x.ToString();
        RightScore.text = score.y.ToString();
        transform.position = new Vector3(0, 1.26f, 0);
        yield return new WaitForSeconds(2);
        StartMovement();
        isWaiting = false; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            audioSources[2].Play();
            
            float hitPosition = (transform.position.z - collision.transform.position.z) / collision.collider.bounds.size.z;
            direction = new Vector3(-direction.x, 0, hitPosition).normalized;
            speed += 1.5f;
            rb.linearVelocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            audioSources[1].Play();
            
            if (collision.transform.position.x > 0 || collision.transform.position.x < 0)
            {
                direction = new Vector3(-direction.x, 0, direction.z);
            }
            else
            {
                direction = new Vector3(direction.x, 0, -direction.z);
            }
            rb.linearVelocity = direction * speed;
        }
    }
}