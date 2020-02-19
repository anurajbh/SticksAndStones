using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform target;
    Camera cam;

    public float resolutionScale = 0.75f;
    public float camLerpSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = (Screen.height / 100f) / resolutionScale;

    }

    void LateUpdate()
    {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, camLerpSpeed) + new Vector3(0, 0, -10f);
        }
    }
}
