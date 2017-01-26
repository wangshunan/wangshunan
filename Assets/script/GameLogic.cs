using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	[SerializeField] PlayerController playerController;
	[SerializeField] BossController bossController;


	public enum GAME_STATUS {
		Start,
        Pause,
		Clear,
		Over
	}; 

	public GAME_STATUS gameStatus;

	void Awave() {
		gameStatus = (int)GAME_STATUS.Start;
	}

	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		//GameStatusUpData ();
	}

	void GameStatusUpData( ) {
		
		if ( bossController.isDead ) {
			gameStatus = GAME_STATUS.Clear;
		}

		if ( playerController.isDead ) {
			gameStatus = GAME_STATUS.Over;
		}

	}
		
}
