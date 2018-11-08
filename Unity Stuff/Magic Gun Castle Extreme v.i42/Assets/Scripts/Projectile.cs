using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed;
	public ParticleSystem explosionPrefab;
	public Rigidbody2D body;
	private ParticleSystem ep;
	public int damage = 0;

	void Update() {
		// it only shoots up at this time. 
		float move = speed * Time.deltaTime;
		//Vector3 pos = transform.position;
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.Normalize ();
		transform.Translate (pos * move);
	}

   

    private void OnTriggerEnter2D(Collider2D other) {
        //If projectile hits enemy
        if (other.tag == "Enemy") {
            ep = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as ParticleSystem;
            ep.Play();
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            //enemy.health -= damage;
            Destroy(gameObject);
            Destroy(ep.gameObject, ep.startLifetime);
        }
		//Projectile hits wall
        if (other.tag == "Wall") {
            ep = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity) as ParticleSystem;
            ep.Play();
            Destroy(gameObject);
            Destroy(ep.gameObject, ep.startLifetime);
        }
    }

}