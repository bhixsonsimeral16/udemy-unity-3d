using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", delay);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
