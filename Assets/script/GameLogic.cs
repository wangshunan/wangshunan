using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	/*[SerializeField] PlayerController playerController;
	[SerializeField] BossController bossController;
	[SerializeField] EnemyController enemyComtroller;
	[SerializeField] ResultsController resultsController;*/

	private enum GAME_STATUS {
		Start,
		Clear,
		Over
	};

	const int MAX_HP = 100;
	const int MIN_HP = 0;

	public Slider bossHp;
	public Slider playerHp;
	int gameStatus;

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
		
		if (bossHp.value <= MIN_HP) {
			gameStatus = (int)GAME_STATUS.Clear;
		}

		if (playerHp.value >= MAX_HP) {
			gameStatus = (int)GAME_STATUS.Over;
		}

	}
		
}
