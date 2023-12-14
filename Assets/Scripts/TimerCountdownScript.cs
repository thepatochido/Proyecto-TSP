/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountdownScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remianingTime;
    void Update()
    {
        if (remianingTime > 0)
        {
            remianingTime -= Time.deltaTime;
        }
        else if(remianingTime < 0)
        {
            remianingTime = 0;
            //GameOver();
            timerText.color = Color.red;
        }
        int minutes = Mathf.FloorToInt(remianingTime / 60);
        int seconds = Mathf.FloorToInt(remianingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerCountdownScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float initialTime;
    private float remainingTime;
    private BarreraInvisibleEntradaScript barreraScript;

    void Start()
    {
        remainingTime = initialTime;
        barreraScript = FindObjectOfType<BarreraInvisibleEntradaScript>();
    }

    void Update()
    {
        if (barreraScript != null && barreraScript.tiempoRestante > 0)
        {
            UpdateTimerUI(barreraScript.tiempoRestante);
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            // GameOver();
            timerText.color = Color.red;
            UpdateTimerUI(barreraScript.tiempoRestante);
        }
    }
        
    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
