using UnityEngine;

public class GrabBoneScript : MonoBehaviour
{
    public AudioClip[] grabClips;
    public GameObject audioManager;

    private GameObject player;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = audioManager.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int clipIndex = Random.Range(0, grabClips.Length);
            audioSource.PlayOneShot(grabClips[clipIndex]);
            gameObject.SetActive(false);
            player.GetComponent<BoneCountScript>().addBone();
        }
    }
}
