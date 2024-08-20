using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityRespectingIndicator : MonoBehaviour
{
    public float distanceFromOrigin = 0; // 3
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = startPosition - (Physics2D.gravity.normalized * distanceFromOrigin);
        //transform.up = -Physics2D.gravity.normalized;
    }
}
