using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
