using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	//public float forcetoAdd=100;
	private Rigidbody2D rigid;
	[SerializeField] private float walkSpeed = 1;
	[SerializeField] private float jumpPower = 1;






	void Start () {
		//gives it force
		rigid = GetComponent<Rigidbody2D>();

		rigid.velocity = Vector2.up * 10;

	}


	void Update () {

		float walkDirection = Input.GetAxis("Horizontal");
		float jump = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2(walkDirection, jump);

		Move(movement);
	}

	void Move (Vector2 movement) {
		rigid.velocity = new Vector2(movement.x * walkSpeed, movement.y * jumpPower);
	}
}
