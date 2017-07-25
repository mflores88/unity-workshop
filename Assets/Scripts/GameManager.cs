using UnityEngine;

public class GameManager : MonoBehaviour {
	[SerializeField] int scoreToAdd = 1;
	[SerializeField] float scoreRepeatRate = 1f;

	bool hasGameStarted;
	GameObject player;
	Vector3 playerSpawnPoint;
	int score = 0;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player) {
			playerSpawnPoint = player.transform.position;
		}
	}

	void AddScorePoint () {
		score += scoreToAdd;
	}

	public bool HasGameStarted {
		get {
			return hasGameStarted;
		}
	}

	public int Score {
		get {
			return score;
		}
	}

	public void StartGame () {
		hasGameStarted = true;
		InvokeRepeating ("AddScorePoint", scoreRepeatRate, scoreRepeatRate);
	}

	public void Restart () {
		if (!player) {
			return;
		}
		CancelInvoke ("AddScorePoint");
		score = 0;
		hasGameStarted = false;
		player.transform.position = playerSpawnPoint;
	}
}
