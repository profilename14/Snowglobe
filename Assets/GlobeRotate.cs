using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRotate : MonoBehaviour
{
public GameObject snowglobe;
    public AnimationCurve SmoothingCurve;
    public float MaxCorrectionSpeed = 20f;
    public float gravCorrectionDist = 10f;
    public float gravMoveMultiplier = 1.0f;

    private bool isRotating;
    private float startMousePosition;
    private Quaternion targetRotation;
    private bool snapping;

    void Start()
    {
        isRotating = false;
        snapping = false;
    }

    void Update()
    {
        snowglobe.transform.up = -Physics2D.gravity.normalized;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isRotating = true;
            snapping = false;
            startMousePosition = Input.mousePosition.x;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isRotating = false;
            snapping = true;
            StartSnapping();
        }

        if (isRotating)
        {
            float currentMousePosition = Input.mousePosition.x;
            float mouseRelative = currentMousePosition - startMousePosition;

            snowglobe.transform.Rotate(0, 0, -(mouseRelative * gravMoveMultiplier));

            Vector2 newGravityDirection = snowglobe.transform.up * -Physics2D.gravity.magnitude;
            Physics2D.gravity = newGravityDirection;

            startMousePosition = currentMousePosition;
        }

        if (snapping)
        {
            CorrectRotation();
        }

        Debug.DrawRay(transform.position, Physics2D.gravity);
    }

    private void StartArrowRotation(Vector2 newGravityDirection)
    {
        // Set the target rotation based on the new gravity direction
        targetRotation = Quaternion.LookRotation(Vector3.forward, -newGravityDirection);
        snapping = true;
    }

    private void StartSnapping()
    {
        float currentRotationZ = snowglobe.transform.eulerAngles.z;
        float targetRotationZ = Mathf.Round(currentRotationZ / 90f) * 90f;
        targetRotation = Quaternion.Euler(0, 0, targetRotationZ);
    }

    private void CorrectRotation()
    {

        // Get the new rotation towards the target angle.
        Quaternion desiredRot = targetRotation;

        // Calculate the smoothing strength using the AnimationCurve and MaxCorrectionSpeed
        float strength = Time.unscaledDeltaTime * (MaxCorrectionSpeed * SmoothingCurve.Evaluate(Mathf.Clamp(1f, 0f, gravCorrectionDist) / gravCorrectionDist));

        // Apply the smoothed rotation using Slerp (shluuurp)
        snowglobe.transform.rotation = Quaternion.Slerp(snowglobe.transform.rotation, desiredRot, strength);

        Vector2 newGravityDirection = snowglobe.transform.up * -Physics2D.gravity.magnitude;
        Physics2D.gravity = newGravityDirection;

        // Stop snapping once the rotation is close enough to the target
        if (Quaternion.Angle(snowglobe.transform.rotation, targetRotation) < 0.1f)
        {
            snapping = false;
        }
    }
}
