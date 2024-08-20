using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piton : MonoBehaviour
{
    [HideInInspector] public bool isAdjacent = false;
    [HideInInspector] public bool isActive = false;

    public float minimumDistance = 7.5f;
    public Vector2 location = Vector2.zero;
    public SpriteRenderer signalKey;
    // Start is called before the first frame update
    void Start()
    {
        signalKey.enabled = false;
        if (location == Vector2.zero) {
            location = new Vector2 (transform.position.x, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            isAdjacent = true;
            signalKey.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            isAdjacent = false;
            signalKey.enabled = false;
        }
    }

    public void Grapple() {
        if (isAdjacent) {
            signalKey.enabled = false;
        }
    }
}
