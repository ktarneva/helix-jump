using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheckController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.gameManager.AddScore(1);
        Debug.Log(GameManager.gameManager.score);
       
    }
}
