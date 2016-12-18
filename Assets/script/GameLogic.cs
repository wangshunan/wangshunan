using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField] PlayerController playerController;
	[SerializeField] BossController bossController;
	[SerializeField] EnemyController enemyComtroller;
	[SerializeField] ResultsController resultsController;

	public enum GAME_STATUS {
		Start,
		Clear,
		Over
	}; 

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
		}

		if ( playerController.isDead ) {
			gameStatus = (int)GAME_STATUS.Over;
		}

	}
		
}
