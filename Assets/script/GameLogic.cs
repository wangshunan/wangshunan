using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
	float alpha = 0.0f;
	private int Color;
    public Slider bossHpSlider;
	public Slider playerHpSlider;
	public GameObject clear;
	public GameObject gameOver;
	public int game;
	public GameObject retry;
	public GameObject title;
	public GameObject asd;

	private GameObject boss;
	private GameObject enemy;
	private GameObject clearImage;
	private GameObject overImage;
	public GameObject player;

	public enum Game {
		Start,
		Clear,
		Over
	};

	void Start() {
		clear = GameObject.Find ("GameClear");
		gameOver = GameObject.Find ("GameOver");
		clearImage = GameObject.Find ("CLEAR!");
		overImage = GameObject.Find ("game_over2");
		game = ( int )Game.Start;
		//asd.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( bossHpSlider.value <= 0 ) {
			GameClear ();
		}
		if ( playerHpSlider.value == 100 || player.transform.position.y <= -2.0f ) {
			GameOver ();
		}
	}

	public void GameClear( ) {
		asd.SetActive (false);
		if (alpha <= 0.6f) {
			// フェードアウト
			alpha += 0.01f;
			clear.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, alpha);
		}

		if ( alpha > 0.6f && clearImage.transform.position.y > 2.0f ) {
			clearImage.transform.position -= new Vector3 (0, 0.05f, 0 );
		}

		if (clearImage.transform.position.y <= 2.0f) {
			retry.SetActive (true);
			title.SetActive (true);
		}
	}

	public void GameOver( ) {
		asd.SetActive (false);
		if (alpha <= 0.6f) {
			alpha += 0.01f;
			gameOver.GetComponent<SpriteRenderer> ().color = new Color ( 0, 0, 0, alpha);
		}

		if ( alpha > 0.6f && overImage.transform.position.y > 2.0f ) {
			overImage.transform.position -= new Vector3 (0, 0.05f, 0 );
		}

		if (overImage.transform.position.y <= 2.0f) {
			retry.SetActive (true);
			title.SetActive (true);
		}

	}

	public void SceneManager( ) {
		
	}
}
