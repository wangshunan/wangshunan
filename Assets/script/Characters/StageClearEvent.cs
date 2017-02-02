using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearEvent : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameLogic gameLogic;


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
            gameLogic.gameStatus = GameLogic.GAME_STATUS.Clear;
			Destroy( gameObject.GetComponent<BoxCollider2D>() );
        }
    }
}
