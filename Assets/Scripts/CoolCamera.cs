using UnityEngine;

public class CoolCamera : MonoBehaviour {
	[SerializeField] Vector3 offset;

	[SerializeField] float time = 0.1f;

	[SerializeField] bool isHorizontalLocked;

	[SerializeField] bool isVerticalLocked;

	[SerializeField] Transform target;

	void Start () {
		if (target == null) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			target = player.transform;
		}
	}

	void Update () {
		if (target != null) {
			Vector3 goalPosition = target.position;
			goalPosition.z = transform.position.z;

			if (isHorizontalLocked) {
				goalPosition.x = transform.position.x;
			}

			if (isVerticalLocked) {
				goalPosition.y = transform.position.y;
			}

			transform.position = Vector3.Lerp (transform.position, goalPosition + offset, time);
		}
	}
}
