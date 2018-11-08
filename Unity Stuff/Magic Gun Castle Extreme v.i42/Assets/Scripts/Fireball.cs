using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public float speed;
	public int power;

	void Update() {
		float move = speed * Time.deltaTime;
		// it only shoots up at this time. 
		transform.Translate (Vector3.up * move);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		// if bullet hits enemy object;
		if (other.tag == "Enemy") {
			Destroy(other.gameObject);
			Destroy (gameObject);
		}
		// if bullet hits a wall
		if (other.tag == "Wall") {
			//Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

}