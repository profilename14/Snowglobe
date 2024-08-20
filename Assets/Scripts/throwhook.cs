using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class throwhook : MonoBehaviour {


	public GameObject hook;
	public GameObject Player;
	public GameObject[] allPitons;
	public float pitonRange = 5;



	public bool ropeActive;

	GameObject curHook;


	Vector2 lastSavePosition;

	// Use this for initialization
	void Start () {
		allPitons = GameObject.FindGameObjectsWithTag("Piton");	
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKeyDown ("e") || Input.GetMouseButtonDown(1)) {
			HookThrow();
		} else if (ropeActive == false && allPitons.Length > 0) {
			HookThrow();
		}

		if (Input.GetKeyDown ("i")) {
			Respawn();
		}


	}

	private void HookThrow() {
		GameObject nearestPiton = allPitons[0];
		float distanceToNearest = Vector3.Distance(Player.transform.position, nearestPiton.transform.position);

		for (int i = 1; i < allPitons.Length; i++)
		{
			float distanceToCurrent = Vector3.Distance(Player.transform.position, allPitons[i].transform.position);

			if (distanceToCurrent < distanceToNearest)
			{
				nearestPiton = allPitons[i];
				distanceToNearest = distanceToCurrent;
			}

		}

		Piton nearestPitonScript = nearestPiton.gameObject.GetComponent<Piton>();


		if (nearestPitonScript.isAdjacent || ropeActive == false)
		{
			if (ropeActive == true)
			{
				Destroy(curHook);
			}
			
			Vector2 destiny = nearestPiton.transform.position;

			curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);

			curHook.GetComponent<RopeScript>().destiny = destiny;
			curHook.GetComponent<RopeScript>().minimumDistance = nearestPitonScript.minimumDistance;

			ropeActive = true;

			lastSavePosition = nearestPitonScript.location;
		}
	}

	public void Respawn() {
		// play death animation
	
		Destroy(curHook);
		ropeActive = false;
		Player.transform.position = lastSavePosition;
		Vector3 displacerVector = lastSavePosition;
		Player.transform.position += displacerVector.normalized * 0.1f;

		HookThrow();


	}
}
