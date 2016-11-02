using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField] PlayerController playerController;
	[SerializeField] BossController bossController;
	[SerializeField] EnemyController enemyComtroller;
	[SerializeField] ResultsController resultsController;
	[SerializeField] BalloonController balloonController;

	public enum GAME_STATUS {
		Start,
		Clear,
		Over
	};

	const int MAX_HP = 100;
	const int MIN_HP = 0;

	public int gameStatus;

	void Awave() {
		gameStatus = (int)GAME_STATUS.Start;
	}

	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		GameStatusUpData ();
	}

	void GameStatusUpData( ) {
		
		if ( bossController.isDead ) {
			gameStatus = (int)GAME_STATUS.Clear;
			balloonController.Clear();
		}

		if ( playerController.isDead ) {
			gameStatus = (int)GAME_STATUS.Over;
			balloonController.Over ();
		}

	}
		
}
