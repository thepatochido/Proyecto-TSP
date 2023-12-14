using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CorazonesScript : MonoBehaviour
{
    public int health;
    public int numDeCorazones;
    private bool hasDied = false;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject blackImage;

    private void Update()
    {
        if(health > numDeCorazones)
        {
            health = numDeCorazones;
        }

        for (int i = 0;  i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].rectTransform.localScale = new Vector3(0.6f,0.6f,0.99f);
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].rectTransform.localScale = new Vector3(0.5f,0.5f,1);
            }

            if(i<numDeCorazones)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled=false;
            }
        }
    }

    public void DamagePlayer(int damageToPlayer)
    {
        health -= damageToPlayer;
        if (health <= 0 && !hasDied)
        {
            hasDied = true;
            blackImage.SetActive(true);
            StartCoroutine(DelaySceneChange());
        }
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}
