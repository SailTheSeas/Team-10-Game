using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleAdjust : MonoBehaviour
{
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindFirstObjectByType<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(cam);
    }
}
