using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using System.Collections;


public class PowerUp : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 rotation = new Vector3(0, 0, 1);
    public Vector2[] spownerZone_Left = new Vector2[2];
    public Vector2[] spownerZone_Right = new Vector2[2];
    [FormerlySerializedAs("ball")] public GameObject ballToThrow;
    
    public GameObject originalBall ;
    public GameObject GO_colide;
    public Vector3 oldSize;
    public int oldSide = 0;
    
    
    private bool isWaiting = false; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation, Time.deltaTime*speed);
    }
    void OnCollisionEnter(Collision collisionn)
    {
        if(collisionn.gameObject.CompareTag("Paddle") && !isWaiting)
        {
            GO_colide = collisionn.gameObject;
            StartCoroutine(ResetPowerUp());
            
          
            
            

        }
    }
    IEnumerator ResetPowerUp()
    {
        isWaiting = true;
        (Vector3, int) random = randomSpownerPosition();
        Vector3 oldPosition = transform.position;
        
        Vector3 position = random.Item1;
        transform.position = new Vector3(100, 100, 100);
        int side = random.Item2; // 1 for right, 0 for left
        int powertype = Random.Range(0, 2);
        
        if(powertype == 0)
        {
            oldSize = GO_colide.transform.localScale;
            
            GO_colide.transform.localScale += new Vector3(0, 0, 2);
        }
        
        if(powertype == 1)
        {
            Vector3 positionBall = oldPosition;
            if (oldSide == 0)
            {
                positionBall.x += 2;
            }
            if(oldSide == 1)
            {
                positionBall.x -= 2;
            }
            Instantiate(ballToThrow, positionBall, Quaternion.identity);
            ballToThrow.gameObject.GetComponent<movementBallToThrow>().side = oldSide;
                
        }
        
        
        
        yield return new WaitForSeconds(2);
        
        
        
        if(powertype == 0)
        {
            GO_colide.transform.localScale = oldSize;


        }
        
        
        yield return new WaitForSeconds(4);
        transform.position = position;
        isWaiting = false;
        oldSide = side;
        
    }

    private (Vector3, int) randomSpownerPosition()
    {
        //choose a camp
        int randomSpawn = Random.Range(0, 2);
        print("THE SIDE IS : " + randomSpawn  );
        if(Random.Range(0, 2) < 1)
        {
            //left
            
            return (new Vector3(spownerZone_Left[randomSpawn].x, 1.26f, spownerZone_Left[randomSpawn].y), 0);
            
        }
        else
        {
            //right
            return (new Vector3(spownerZone_Right[randomSpawn].x, 1.26f, spownerZone_Right[randomSpawn].y), 1);
        }
    }
}
