using UnityEngine;

public class PlayerShooting : MonoBehaviour {
	public int damagePerShot = 20;                  // The damage of the bullet
	public int damagePerMagic = 10;					// The damage of the magic
	public float timeBetweenBullets = 0.15f;        // Time between each shot
	public float range = 100f;                      // The distance in which the weapon can be shot at

	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	ParticleSystem gunParticles;                    // Reference to the particle system.
	LineRenderer gunLine;                           // Reference to the line renderer.
	//AudioSource gunAudio;                         // Reference to the audio source.
	Light gunLight;                                 // Reference to the light component.
	public Light faceLight;						
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

	void Awake () {
		// Create a layer mask for the Shootable layer.
		shootableMask = LayerMask.GetMask ("Shootable");

		// Set up the references.
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		//gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		//faceLight = GetComponentInChildren<Light> ();
	}


	void Update () {
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the Fire1 button (left-click) is being press and it's time to fire...
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			if (PlayerController.isMagic) {
				ShootMagic ();
			} else if (!PlayerController.isMagic) {
				ShootWeapon ();
			} 
		} 

		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}


	public void DisableEffects () {
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		faceLight.enabled = false;
		gunLight.enabled = false;
	}


	void ShootWeapon (){
		if (PlayerController.isWeapon > 0) {
			PlayerController.isWeapon--;

			print (PlayerController.isWeapon);

			// Reset the timer.
			timer = 0f;

			// Play the gun shot audioclip.
			//gunAudio.Play ();

			// Enable the lights.
			gunLight.enabled = true;
			faceLight.enabled = true;

			// Stop the particles from playing if they were, then start the particles.
			gunParticles.Stop ();
			gunParticles.Play ();

			// Enable the line renderer and set it's first position to be the end of the gun.
			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
	
			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			if (Physics.Raycast (shootRay, out shootHit, 100, shootableMask)) {
				EnemyHealth enemyHealth = shootHit.transform.GetComponent<EnemyHealth> ();

				// If the EnemyHealth component exist...
				if (enemyHealth != null) {
					// ... the enemy should take damage.
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);

				}
				// Set the second position of the line renderer to the point the raycast hit.
				gunLine.SetPosition (1, shootHit.point);
			}
			// If the raycast didn't hit anything on the shootable layer...
			else {
				// ... set the second position of the line renderer to the fullest extent of the gun's range.
				gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			}
		} else {
			OutOfAmmo ();
		}
	}

	void OutOfAmmo() {
		
		// give player back magic
		PlayerController.isMagic = true;

		Transform parent = transform.parent;

		foreach (Transform child in parent) {
			// disable the weapon
			if (child.name == "GunBarrelEnd") {
				child.gameObject.SetActive (false);
			} 
			// re-enable the magic
			else if (child.name == "MagicBarrelEnd") {
				child.gameObject.SetActive (true);
			}
		}

	}

	void ShootMagic() {

		// Reset the timer.
		timer = 0f;

		// Play the gun shot audioclip.
		//gunAudio.Play ();

		// Enable the lights.
		gunLight.enabled = true;
		faceLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		gunParticles.Stop ();
		gunParticles.Play ();

		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, 100, shootableMask)) {
			EnemyHealth enemyHealth = shootHit.transform.GetComponent<EnemyHealth>();
			
		// If the EnemyHealth component exist...
			if(enemyHealth != null) {
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damagePerMagic, shootHit.point);

			}
			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else {
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

}