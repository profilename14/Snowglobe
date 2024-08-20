using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            //Debug.Log("AAAAAAA");
            //SceneManager.LoadScene("SampleScene");
            GameObject playerVisual = collision.gameObject.GetComponent<PlayerMovement>().playerFX.gameObject;
            playerVisual.GetComponent<Animator>().SetBool("isDead", true);;
        }
    }
}
