using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;            // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	//Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	public static int playerHealth = 100;
	Animator anim;

    public static bool isMagic = true;  // true if player can use magic; otherwise false
    public static int isWeapon = 0;     // is set to the number of ammo player has

    void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator>();
		// Set up references.
		//anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}



	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		//Rotate with mouse point
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		anim.SetFloat ("RotX", Quaternion.LookRotation (Vector3.forward, mousePos - transform.position).x);
//		anim.SetFloat ("RotY", Quaternion.LookRotation (Vector3.forward, mousePos - transform.position).y);
//		Debug.Log (Quaternion.LookRotation (Vector3.forward, mousePos - transform.position).x);
//		Debug.Log (Quaternion.LookRotation (Vector3.forward, mousePos - transform.position).y);
////		Debug.Log (transform.position.x - mousePos.x);
////		Debug.Log (transform.position.y -mousePos.y);
		// Move the player around the scene.
		anim.SetFloat("Valx", h);
		anim.SetFloat ("Valy", v);
		Debug.Log (h);
		Debug.Log (v);
		Move (h, v);
		if (h != 0 || v != 0) {
//			anim.SetBool ("isWalking", true);
			anim.SetBool ("isRunning", true);





		}
		else 
		{
			//anim.SetBool ("isWalking", false);
			anim.SetBool ("isRunning", false);

		}



		// Turn the player to face the mouse cursor.
		Turning ();

		// Animate the player.

	}

	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			playerHealth -= 10;
		};
		Debug.Log (playerHealth);
			
	}

	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			anim.SetFloat("RotY", newRotation.y);
			anim.SetFloat ("RotW", newRotation.w);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	int GetPlayerHealth()
	{
		return playerHealth;
	}


    void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            if (other.gameObject.CompareTag("Magic")) {
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Med")) {
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Weapon")) {
                isMagic = false;
                isWeapon = 10;
                PrepareWeapon();
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Magic")) {
                Destroy(other.gameObject);
            }
        }
    }
    void PrepareWeapon() {
        foreach (Transform child in transform) {
            if (child.name == "WeaponGroup") {
                foreach (Transform gchild in child) {
                    if (gchild.name == "MagicBarrelEnd") {
                        gchild.gameObject.SetActive(false);
                    } else if (gchild.name == "GunBarrelEnd") {
                        gchild.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
