using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    EnemyController enemyController;

    [SerializeField]
    BossController bossController;

    [SerializeField]
    GameLogic gameLogic;

	// Use this for initialization
	void Awake () {
	    enemyController = GameObject.Find( "Zako" ).GetComponent<EnemyController>();
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        if ( gameLogic.gameStatus != GameLogic.GAME_STATUS.Start ) {
            return;
        }

        enemyController.Controller();       
	    //bossController.Controller();
	}
}
