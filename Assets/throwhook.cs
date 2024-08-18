using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;
	public GameObject Player;
	public GameObject[] allPitons;
	public float pitonRange = 5;



	public bool ropeActive;

	GameObject curHook;

	// Use this for initialization
	void Start () {
	allPitons = GameObject.FindGameObjectsWithTag("Piton");	
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKeyDown ("e") || Input.GetMouseButtonDown(1)) {
			GameObject nearestPiton = allPitons[0];
			float distanceToNearest = Vector3.Distance(Player.transform.position, nearestPiton.transform.position);

			for (int i = 1; i < allPitons.Length; i++) {
			float distanceToCurrent = Vector3.Distance(Player.transform.position, allPitons[i].transform.position);
			
			if (distanceToCurrent < distanceToNearest) {
				nearestPiton = allPitons[i];
				distanceToNearest = distanceToCurrent;
			}
		
		}


			if (ropeActive == false && distanceToNearest < pitonRange) {
				Vector2 destiny = nearestPiton.transform.position;


				curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);

				curHook.GetComponent<RopeScript> ().destiny = destiny;


				ropeActive = true;
			} else {

				//delete rope

				Destroy (curHook);


				ropeActive = false;

			}
		}


	}
}
