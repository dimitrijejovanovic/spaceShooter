using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public int ammo;

	public Boundary bound;
	public float speed, tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

    private GameController gameController;

    void Awake(){
		ammo = 20;
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc != null)
        {
            gameController = gc.GetComponent<GameController>();
        }
    }

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire && ammo > 0) {
			nextFire = fireRate + Time.time;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			ammo--;
            gameController.updateAmmo();
		}
	}

	void FixedUpdate (){
        	float moveHor = Input.GetAxis ("Horizontal");
            float moveVer = Input.GetAxis ("Vertical");
    
      
        Vector3 movement = new Vector3 (moveHor, 0.0f, moveVer);
		GetComponent<Rigidbody>().velocity = movement*speed;
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, bound.xMin, bound.xMax),
			0.0f,
			Mathf.Clamp (GetComponent<Rigidbody> ().position.z, bound.zMin, bound.zMax)

		);
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f ,0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);
	}

	public void ammoGain(){
		ammo += 3;
        gameController.updateAmmo();
    }


}
