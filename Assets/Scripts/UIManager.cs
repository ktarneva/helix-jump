using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private Text textBestScore;

    void Update()
    {   
        textBestScore.text = "Best:" + GameManager.gameManager.best;
        textScore.text = "Score:" + GameManager.gameManager.score;
    }
}
