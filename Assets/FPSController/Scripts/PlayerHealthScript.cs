using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour
{
    public int playerHealth = 100;
    private bool hasDied = false;
    public Image lifeBarImage;
    public GameObject blackImage;
    public void DamagePlayer(int damageToPlayer)
    {
        playerHealth -= damageToPlayer;
        UpdateLifeBar();
        if (playerHealth <= 0 && !hasDied)
        {
            hasDied = true;
            blackImage.SetActive(true);
            StartCoroutine(DelaySceneChange());
        }
    }

    public void HealPlayer(int healAmount)
    {
        playerHealth += healAmount;
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        lifeBarImage.fillAmount = (float)playerHealth / 100f;
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}