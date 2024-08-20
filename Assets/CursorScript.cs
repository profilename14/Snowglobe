using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
	Cursor.visible = false;
        Vector2 cursorpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorpos.x, cursorpos.y);
    }
}
