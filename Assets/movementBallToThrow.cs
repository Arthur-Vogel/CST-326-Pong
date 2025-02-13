using UnityEngine;
using TMPro;

public class movementBallToThrow : MonoBehaviour
{
    public int side;
    private GameObject originalBall;
    
    private GameObject Thrower;
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalBall = GameObject.Find("Ball");
        Thrower = GameObject.Find("PowerUp");
        side = Thrower.GetComponent<PowerUp>().oldSide;
        speed =  originalBall.GetComponent<BallController3D>().speed;
        print ("the side is " + side);
        print("the speed is " + speed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (side == 0)
        {
            
            Vector3 direction = new Vector3(1, 0, 0).normalized;
            GetComponent<Rigidbody>().linearVelocity = direction * speed;
        }
        if(side ==1)
        {
            Vector3 direction = new Vector3(-1, 0, 0).normalized;
            GetComponent<Rigidbody>().linearVelocity = direction * speed;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (side == 0)
        {
            if (transform.position.x > 13)
            {
                originalBall.GetComponent<BallController3D>().score.y++;
                originalBall.GetComponent<BallController3D>().audioSources[0].Play();
                Destroy(gameObject);
            }
        }
        if(side == 1)
        {
            if (transform.position.x < -13)
            {
                originalBall.GetComponent<BallController3D>().score.x++;
                originalBall.GetComponent<BallController3D>().audioSources[0].Play();
                Destroy(gameObject);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Destroy(gameObject);
        }
    }
}
