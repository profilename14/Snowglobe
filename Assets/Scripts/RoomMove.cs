using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Transform cameraTargetPosition; // Where the camera should move to
    public float transitionDuration = 1f; // Duration of the transition
    public AnimationCurve transitionCurve; // Smoothing curve for the transition

    private Camera mainCamera;
    private bool isTransitioning = false;
    private float elapsedTime = 0f;
    private Vector3 initialCameraPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartTransition();
        }
    }

    void StartTransition()
    {
        isTransitioning = true;
        elapsedTime = 0f;
        initialCameraPosition = mainCamera.transform.position;

    }

    void Update()
    {
        if (isTransitioning)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);
            t = transitionCurve.Evaluate(t);

            // Move the camera and player smoothly to the new positions
            mainCamera.transform.position = Vector3.Lerp(initialCameraPosition, cameraTargetPosition.position, t);

            if (t >= 1f)
            {
                isTransitioning = false;
            }
        }
    }
}

