using UnityEngine;
using System.Collections;

public class ResultsManager : MonoBehaviour {

	[SerializeField] ResultsController resultsController;
	[SerializeField] GameLogic gamelogic;

    public int test;


	// Use this for initialization
	void Start () {
	    
	}

	// Update is called once per frame
	void Update () {
		switch ( test ) {
		case (int)GameLogic.GAME_STATUS.Start:
			break;
		case (int)GameLogic.GAME_STATUS.Clear:
			resultsController.GameClear ();
			break;
		case (int)GameLogic.GAME_STATUS.Over:
			resultsController.GameOver ();
			break;
		}
	}
}
