using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyForTime : MonoBehaviour
{
    public float time = 0.4f;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
