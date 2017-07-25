using UnityEngine;

public class CompletePlayerMovement : MonoBehaviour {
	// adding SerializeField attr to a field makes it available on Unity's inspector without making it public
	[SerializeField] float jumpForce = 240f; // we define a default jumpForce for our player
	[SerializeField] float movementSpeed = 5f; // we define a default movementSpeed for our player
	[SerializeField] LayerMask[] whatIsGround; // we define a which layers are part of the ground
	// more vars to reference local components
	Rigidbody myRigidbody;
	Animator myAnimator;
	GameManager gameManager;
	// on Awake() is where we initializate our local data
	void Awake () {
		myRigidbody = GetComponent<Rigidbody> ();
		myAnimator = GetComponent<Animator> ();
	}
	// on Start() is where the interaction with other objects takes place
	void Start () {
		GameObject gameManagerGameObject = GameObject.FindGameObjectWithTag ("GameManager");
		if (gameManagerGameObject != null) {
			gameManager = gameManagerGameObject.GetComponent<GameManager> ();
		}
	}
	// on Update() is where we place our business logic, this runs every frame
	void Update () {
		if (gameManager == null) {
			return;
		}
		// returns true during the frame the user pressed down the virtual button identified by the name "Jump"
		bool isJumping = Input.GetButtonDown ("Jump");

		if (!gameManager.HasGameStarted) {
			if (isJumping) {
				gameManager.StartGame ();
			}
			return;
		}

		float xForce = movementSpeed;
		float yForce = 0f;

		if (isJumping && IsGrounded ()) {
			// we want to stop falling before jumping to achieve a predictable behavior
			myRigidbody.velocity = Vector3.zero;
			yForce = jumpForce;
		}
		if (myAnimator.GetBool ("IsJumping") == IsGrounded ()) {
			myAnimator.SetBool ("IsJumping", !IsGrounded ());
		}
		// we set vector forces as new velocity to our object, we ignore Z component at all
		Vector3 newForce = new Vector3 (xForce, myRigidbody.velocity.y + yForce, 0f);
		myRigidbody.velocity = newForce;
		if (!myAnimator.GetBool ("IsRunning")) {
			myAnimator.SetBool ("IsRunning", true);
		}
	}
	// IsGrounded() verifies if our game object is colliding with any layer identified as "Ground"
	bool IsGrounded () {
		// uncomment this to see the ray on unity editor
		// Debug.DrawRay (transform.position, Vector3.down, Color.green, 0.4f);

		foreach (LayerMask layer in whatIsGround) {
			if (Physics.Raycast (transform.position, Vector3.down, 0.4f, layer.value)) {
				return true;
			}
		}

		return false;
	}
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.layer == 31) { // layer 31 = "Bounds"
			Restart();
			gameManager.Restart ();
		}
	}

	public void Restart() {
		myRigidbody.velocity = Vector3.zero;
		myAnimator.SetBool ("IsRunning", false);
		myAnimator.SetBool ("IsJumping", false);
	}
}
