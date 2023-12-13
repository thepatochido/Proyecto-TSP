using System.Collections;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    private GameObject doggy;
    private HowlAndAttackScript doggyScript;
    public bool eated=false;

    private void Start()
    {
        doggy = GameObject.FindGameObjectWithTag("Doggy");
        doggyScript = doggy.GetComponent<HowlAndAttackScript>();
    }

    private void Update()
    {
        float distanceToBone = Vector3.Distance(transform.position, doggy.transform.position);

        if (distanceToBone <= doggyScript.eatRange)
        {
           StartCoroutine("EatDelay");
        }
    }

    IEnumerator EatDelay()
    {
        yield return new WaitForSeconds(doggyScript.eatingTime);
        doggyScript.chaseRange += doggyScript.chaseRangeAmount;
        doggyScript.boneAviable = false;
        Destroy(gameObject);
    }
}
