using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // This script is largely unutilized, but I'm keeping it around just in case we decide to add wall jumping later.

    [Header("Layers")]
    public LayerMask groundLayer;
    
    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        // Transform the offsets using the player's rotation
        Vector2 transformedBottomOffset = (Vector2)transform.TransformPoint(bottomOffset) - (Vector2)transform.position;
        Vector2 transformedRightOffset = (Vector2)transform.TransformPoint(rightOffset) - (Vector2)transform.position;
        Vector2 transformedLeftOffset = (Vector2)transform.TransformPoint(leftOffset) - (Vector2)transform.position;

        // Check collisions using the transformed offsets
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + transformedBottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + transformedRightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + transformedLeftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + transformedRightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + transformedLeftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Transform the offsets for visualization in the editor
        Vector2 transformedBottomOffset = (Vector2)transform.TransformPoint(bottomOffset) - (Vector2)transform.position;
        Vector2 transformedRightOffset = (Vector2)transform.TransformPoint(rightOffset) - (Vector2)transform.position;
        Vector2 transformedLeftOffset = (Vector2)transform.TransformPoint(leftOffset) - (Vector2)transform.position;

        Gizmos.DrawWireSphere((Vector2)transform.position + transformedBottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + transformedRightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + transformedLeftOffset, collisionRadius);
    }
}

