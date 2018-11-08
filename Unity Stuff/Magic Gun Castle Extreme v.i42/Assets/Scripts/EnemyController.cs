using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	Transform player;               // Reference to the player's position.

	public int enemyHealth = 10;
	public int playerHealth = 10;// Reference to this enemy's health.
	//NavMeshAgent nav; 
	public float speed = 10f;
	public float rotationSpeed = 3f; //speed of turning

	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//nav = GetComponent <NavMeshAgent> ();
	}


	void Update ()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(player.position - transform.position), rotationSpeed*Time.deltaTime);
		// If the enemy and the player have health left...
		transform.position += transform.forward * speed * Time.deltaTime;

//		if(enemyHealth > 0 && playerHealth > 0)
//		{
//			// ... set the destination of the nav mesh agent to the player.
//			nav.SetDestination (player.position);
//		}
//		// Otherwise...
//		else
//		{
//			// ... disable the nav mesh agent.
//			nav.enabled = false;
//		}
	} 
}