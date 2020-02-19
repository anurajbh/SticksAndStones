using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform target;
    Camera cam;

    public float resolutionScale = 0.75f;
    public float camLerpSpeed = 0.04f;
    public float camTargetRadius = 2f;

    public float camLerpSpeedOnTarget = 0.04f;
    public float camLerpSpeedOnTransition = 0.02f;
    private bool isCamOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isCamOnPlayer = true;
        camLerpSpeed = camLerpSpeedOnTransition;
    }

    // Update is called once per frame
    void Update()
    {
        // Allows the camera to scale with screen size
        cam.orthographicSize = (Screen.height / 100f) / resolutionScale;

        // Change the target of the camera with SPACE
        if (Input.GetKeyDown("space")) {
            if (isCamOnPlayer) {
                if (GameObject.FindGameObjectWithTag("Target") != null) {
                    target = GameObject.FindGameObjectWithTag("Target").transform;
                    isCamOnPlayer = false;
                }
            } else {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                isCamOnPlayer = true;
            }
        }

        // Change the camera speed if moving between targets
        // TODO: Need to make change in camera speed smoother
        Vector2 camXY = new Vector2(cam.transform.position.x, cam.transform.position.y);
        Vector2 targetXY = new Vector2(target.position.x, target.position.y);
        if ((targetXY - camXY).magnitude > camTargetRadius) {
            camLerpSpeed = camLerpSpeedOnTransition;
        } else {
            camLerpSpeed = camLerpSpeedOnTarget;
        }
        print(camLerpSpeed);

    }

    void LateUpdate()
    {
        // Move the camera towards the target using a LERP
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, camLerpSpeed) + new Vector3(0, 0, -10f);
        }
    }
}
