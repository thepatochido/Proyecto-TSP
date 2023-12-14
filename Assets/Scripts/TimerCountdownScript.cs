<<<<<<< Updated upstream
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

using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    private bool timerStarted = false;
=======
    private BarreraInvisibleEntradaScript barreraScript;
    public GameObject blackImage;
>>>>>>> Stashed changes

    void Start()
    {
        remainingTime = initialTime;
    }

    void Update()
    {
        if (timerStarted && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else if (remainingTime <= 2)
        {
            remainingTime = 0;
            blackImage.SetActive(true);
            StartCoroutine(DelaySceneChange());
            timerText.color = Color.red;
            UpdateTimerUI();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change the tag to match your player's tag
        {
            timerStarted = true;
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}
