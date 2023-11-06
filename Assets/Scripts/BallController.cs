using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float inpulseForce = 3f;
    private Vector3 startPos;
 
    void Awake()
    {
        startPos = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
            return;
        }
        
            //Reset lvl when death part is hit
            DeathTrigger deathTrigger = collision.transform.GetComponent<DeathTrigger>();
        if (deathTrigger)
        {
            deathTrigger.HitDeathPart();

        }
        
      
        Debug.Log("The ball touched the platform");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * inpulseForce, ForceMode.Impulse);
        ignoreNextCollision = true;

        Invoke("AllowCollision", .2f);

    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }
   public void ReserBall()
    {
        transform.position = startPos;
    }
}
