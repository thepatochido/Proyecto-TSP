using UnityEngine;

public class GrabBoneScript : MonoBehaviour
{
    public AudioClip[] grabClips;
    public GameObject audioManager;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = audioManager.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int clipIndex = Random.Range(0, grabClips.Length);
            audioSource.PlayOneShot(grabClips[clipIndex]);
            gameObject.SetActive(false);
        }
    }
}
