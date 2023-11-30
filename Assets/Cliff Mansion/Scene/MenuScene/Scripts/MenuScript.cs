using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioClip pressedClip;
    public AudioClip highlightedClip;
    public GameObject loadingImage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void OnHighlighted()
    {
        GetComponent<AudioSource>().PlayOneShot(highlightedClip);
    }
    public void OnPressedButton(string sceneName)
    {
        loadingImage.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        StartCoroutine(DelaySceneChange(sceneName));
    }
    public void OnPressedQuit()
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        StartCoroutine(DelayQuit());
    }

    private IEnumerator DelaySceneChange(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private IEnumerator DelayQuit()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();

    }


    // Update is called once per frame
    void Update()
    {

    }
}

