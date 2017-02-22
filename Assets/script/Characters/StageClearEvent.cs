using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearEvent : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    FadeManager fadeController;


	// Use this for initialization
	void Awake () {
		target = GameObject.Find( "Cguy" );
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D( Collider2D coll) {

        if ( coll.gameObject.tag == ("Player") ) {
            fadeController.fadeOutOver = true;
            fadeController.sceneName = "TalkingZako";
            gameLogic.gameStatus = GameLogic.GAME_STATUS.Clear;
			Destroy( gameObject.GetComponent<BoxCollider2D>() );
        }
    }
}
