using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TrhowBoneScript : MonoBehaviour
{
    public GameObject bonePrefab;
    public GameObject shootPoint;
    public GameObject audioManager;
    public AudioClip throwClip;

    public float speed;
    public float timeToShoot;

    private GameObject player;
    private AudioSource audioSource;

    private float counter;
    private int playersBones;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = audioManager.GetComponent<AudioSource>();
        
    }

    void Update()
    {
        playersBones = player.GetComponent<BoneCountScript>().boneQuantity;

        counter += Time.deltaTime;

        if (counter >= timeToShoot && Input.GetButtonDown("Fire2") && playersBones>0)
        {
            counter = 0;

            GameObject launchedBone = Instantiate(bonePrefab, shootPoint.transform.position, transform.rotation);
            launchedBone.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * speed);
            player.GetComponent<BoneCountScript>().removeBone();
            audioSource.PlayOneShot(throwClip);
        }
    }
}
