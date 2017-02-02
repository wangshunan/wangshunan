using UnityEngine;
using System.Collections;

public class PauseSystem : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;

    private bool pause = false;
    private float tmpTimeScale = 0;

	// Use this for initialization
	void Awake () {
	    gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
        GamePause();
	}
	

    public void GamePause() {

        if ( pause ) {
            return;
        }

        tmpTimeScale = Time.timeScale;
        Time.timeScale = 0;

        gameLogic.gameStatus = GameLogic.GAME_STATUS.Pause;
        pause = true;

    }

    public void GameResume() {

        pause = false;

        Time.timeScale = tmpTimeScale;
        gameLogic.gameStatus = GameLogic.GAME_STATUS.Start;

    }
}
