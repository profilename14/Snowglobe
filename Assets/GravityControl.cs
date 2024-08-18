using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GravityControl : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Physics2D-gravity.html
    //enum GravityDirection { Down, Left, Up, Right };
    //GravityDirection m_GravityDirection;

    public GameObject snowglobe;

    private Vector2 gravityVector;
    private Vector2 originalVector;
    private Vector2 rotatedVector;
    private float angleInRadians;
    private Rigidbody2D rigid;
    private bool isRotating;
    private float startMousePosition;

    void Start()
    {
        isRotating = false;
        gravityVector = Physics2D.gravity;
        originalVector = gravityVector;
        rotatedVector = originalVector;
    }

    void Update()
    {
        snowglobe.transform.up = -Physics2D.gravity.normalized;


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics2D.gravity = new Vector2(0, 9.8f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isRotating = true;
            startMousePosition = Input.mousePosition.x;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            float currentMousePosition = Input.mousePosition.x;
            float mouseRelative = currentMousePosition - startMousePosition;

            snowglobe.transform.Rotate(0, 0, -(mouseRelative * 0.5f));

            Vector2 newGravityDirection = snowglobe.transform.up * -Physics2D.gravity.magnitude;
            Physics2D.gravity = newGravityDirection;

            startMousePosition = currentMousePosition;

            //float h = Input.mousePosition.x - Screen.width / 2;
            //float v = Input.mousePosition.y - Screen.height / 2;
            //Vector2 dir = new Vector2(h, v);
            //dir.Normalize();
            //dir *= gravityVector.magnitude;

            //originalVector = dir;
            //Physics2D.gravity = originalVector;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var angle = 15;

            angleInRadians = angle * Mathf.Deg2Rad;

            rotatedVector = new Vector2(
             originalVector.x * Mathf.Cos(angleInRadians) - originalVector.y * Mathf.Sin(angleInRadians),
             originalVector.x * Mathf.Sin(angleInRadians) + originalVector.y * Mathf.Cos(angleInRadians)
            );

            originalVector = rotatedVector;

            Physics2D.gravity = originalVector;
        }


        Debug.DrawRay(transform.position, Physics2D.gravity);

    }
}