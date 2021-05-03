using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TMP_Text tmp;

    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TMP_Text>();
        tmp.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onEvent(GameManager.Event value) {
        switch(value) {
            case GameManager.Event.PASSED_OBSTACLE:
                currentScore++;
                tmp.text = currentScore.ToString();
                break;
            case GameManager.Event.RESTART:
                currentScore = 0;
                tmp.text = currentScore.ToString();
                break;
        }
        if (value == GameManager.Event.PASSED_OBSTACLE) {
            
        }
    }
}
