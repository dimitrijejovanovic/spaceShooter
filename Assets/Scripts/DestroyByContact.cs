using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	private PlayerController playerController;


	void Start (){

		GameObject pc = GameObject.FindGameObjectWithTag("Player");
		GameObject gc = GameObject.FindGameObjectWithTag("GameController");
		if (gc != null) {
			gameController = gc.GetComponent<GameController> ();

		}
		if (pc != null) {
			playerController = pc.GetComponent<PlayerController> ();

		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Boundary" && other.tag != "Asteroid" ) {
			
			Destroy (other.gameObject);
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			gameController.addScore (10);
			if (playerController != null && other.tag != "Player") {
				playerController.ammoGain ();
			}
			if (other.tag == "Player") {
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				gameController.GameOver ();
			}
		}
	}

}
