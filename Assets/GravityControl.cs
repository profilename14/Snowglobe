using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GravityControl : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Physics2D-gravity.html
    //enum GravityDirection { Down, Left, Up, Right };
    //GravityDirection m_GravityDirection;

    private Vector2 _gravityVector;
    private Vector2 originalVector;
    private Vector2 rotatedVector;
    private float angleInRadians;
    private Rigidbody2D rigid;

    void Start()
    {
        _gravityVector = Physics2D.gravity;
        originalVector = _gravityVector;
        rotatedVector = originalVector;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            float h = Input.mousePosition.x - Screen.width / 2;
            float v = Input.mousePosition.y - Screen.height / 2;
            Vector2 dir = new Vector2(h, v);
            dir.Normalize();
            dir *= _gravityVector.magnitude;

            originalVector = dir;
            Physics2D.gravity = originalVector;
        }
        

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var angle = 15;

            angleInRadians = angle * Mathf.Deg2Rad;

            rotatedVector = new Vector2 (
             originalVector.x * Mathf.Cos(angleInRadians) - originalVector.y * Mathf.Sin(angleInRadians),
             originalVector.x * Mathf.Sin(angleInRadians) + originalVector.y * Mathf.Cos(angleInRadians)
            );

            originalVector = rotatedVector;

            Physics2D.gravity = originalVector;
        }


        Debug.DrawRay(transform.position, Physics2D.gravity);

    }
}