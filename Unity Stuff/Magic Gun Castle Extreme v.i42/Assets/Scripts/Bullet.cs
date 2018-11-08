using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;

	void Update() {
		float move = speed * Time.deltaTime;
		// it only shoots up at this time. 
		transform.Translate (Vector3.up * move);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		// if bullet hits enemy object;
		if (other.tag == "Enemy") {
			print ("enemy hit");
			Destroy(other.gameObject);
			Destroy (gameObject);
		}
		// if bullet hits a wall
		if (other.tag == "Wall") {
			print ("wall hit");
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

}