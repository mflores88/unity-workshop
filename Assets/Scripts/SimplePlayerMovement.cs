using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed = 5f;
	[SerializeField] float turnSpeed = 1000f;

	Rigidbody myRigidbody;
	Animator myAnimator;

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
		myAnimator = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");

		Vector3 playerInput = new Vector3 (horizontalMovement, 0f, verticalMovement);

		if (playerInput == Vector3.zero) {
			myAnimator.SetBool ("IsRunning", false);
			return;
		}

		myAnimator.SetBool ("IsRunning", true);

		Vector3 newPosition = transform.position + playerInput.normalized * movementSpeed * Time.deltaTime;
		myRigidbody.MovePosition (newPosition);

		Quaternion newRotation = Quaternion.LookRotation (playerInput);
		if (myRigidbody.rotation != newRotation) {
			myRigidbody.rotation = Quaternion.RotateTowards (myRigidbody.rotation, newRotation, turnSpeed * Time.deltaTime);
		}
	}
}
