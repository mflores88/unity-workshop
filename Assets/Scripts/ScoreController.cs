using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	Text myText;
	GameManager gameManager;

	void Start () {
		myText = GetComponentInChildren<Text> ();
		GameObject gameManagerGameObject = GameObject.FindGameObjectWithTag ("GameManager");
		if (gameManagerGameObject) {
			gameManager = gameManagerGameObject.GetComponent<GameManager> ();
		}
	}

	void FixedUpdate () {
		if (!gameManager) {
			return;
		}
		string myScore = gameManager.Score.ToString ();
		myText.text = myScore;
		if (myText.text.Equals (myScore)) {
			myText.text = myScore;
		}
	}
}
