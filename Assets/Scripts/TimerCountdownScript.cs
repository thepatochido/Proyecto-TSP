using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerCountdownScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float initialTime;
    private float remainingTime;
    private bool timerStarted = false;
    private BarreraInvisibleEntradaScript barreraScript;
    public GameObject blackImage;

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
        else if (barreraScript.tiempoRestante <= 0)
        {
            timerText.color = Color.red;
            UpdateTimerUI(0);   
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            timerStarted = true;
        }
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}
