using UnityEngine;

public class RotateItemScript : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Time.deltaTime * 100f, 0, 0);
    }
}
