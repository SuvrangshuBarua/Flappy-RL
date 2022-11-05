using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Bird agentRef;
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }
    private void Update()
    {
        scoreText.text = Mathf.FloorToInt(agentRef.counter / 2f).ToString();
    }
}
