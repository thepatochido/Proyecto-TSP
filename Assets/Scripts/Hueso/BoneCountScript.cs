using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCountScript : MonoBehaviour
{
    public int boneQuantity;

    public void addBone()
    {
        boneQuantity++;
    }

    public void removeBone()
    {
        boneQuantity--;
    }
}
