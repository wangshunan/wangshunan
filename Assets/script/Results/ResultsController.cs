using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour {

	const float MAX_HP = 100;
	const float HALF_HP = 50;
	const float MIN_HP = 0;

	[SerializeField] GameLogic gameLogic;

	float alpha = 0.0f;
	private int Color;
	private GameObject clear;
	private GameObject gameOver;
	public GameObject retry;
	public GameObject title;
	public GameObject moveControllerPanel;
    private GameObject balloonStatus;

	public GameObject bossHp;
	public GameObject playerHp;
	public GameObject stamina;

	private GameObject boss;
	private GameObject enemy;
	private GameObject clearImage;
	private GameObject overImage;
	public GameObject player;

	private float count;

	void Awake( ) {
		clear = GameObject.Find ("GameClear");
		gameOver = GameObject.Find ("GameOver");
		clearImage = GameObject.Find ("CLEAR!");
		overImage = GameObject.Find ("game_over2");
		balloonStatus = GameObject.Find ("TextController");
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public void GameClear( ) {
		moveControllerPanel.SetActive (false);
		bossHp.SetActive (false);
		playerHp.SetActive (false);
		stamina.SetActive (false);
		if (alpha <= 0.6f) {
			// フェードアウト
			alpha += 0.01f;
			clear.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, alpha);
		}

		if ( alpha > 0.6f && clearImage.transform.position.y > 2.0f ) {
			balloonStatus.GetComponent<BalloonController> ().BalloonDestroy ();
			clearImage.transform.position -= new Vector3 (0, 0.05f, 0 );
		}

		if (clearImage.transform.position.y <= 2.0f) {
			retry.SetActive (true);
			title.SetActive (true);
		}
	}

	public void GameOver( ) {
		moveControllerPanel.SetActive (false);
		if (alpha <= 0.6f) {
			alpha += 0.01f;
			gameOver.GetComponent<SpriteRenderer> ().color = new Color ( 0, 0, 0, alpha);
		}

		if ( alpha > 0.6f && overImage.transform.position.y > 2.0f ) {
			bossHp.SetActive (false);
			playerHp.SetActive (false);
			stamina.SetActive (false);
			overImage.transform.position -= new Vector3 (0, 0.05f, 0 );
		}

		if (overImage.transform.position.y <= 2.0f) {
			retry.SetActive (true);
			title.SetActive (true);
		}
	}

	public void OnRetryButtonClicked() {
		SceneManager.LoadScene ("Battle");
	}

	public void OnOverButtonClicked() {
		SceneManager.LoadScene ("Title");
	}
}
