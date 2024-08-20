using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityRespectingIndicator : MonoBehaviour
{
    public float distanceFromOrigin = 3; // 3
    Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        transform.up = Physics2D.gravity.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (startPosition.x, startPosition.y, 0) - ( transform.up * distanceFromOrigin);
    }
}
