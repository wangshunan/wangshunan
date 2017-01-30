using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    
    [SerializeField]
    public FadeManager fadeController;
    
    [SerializeField]
    public PauseSystem pause;

	public enum GAME_STATUS {
		Start,
        Pause,
		Clear,
		Over
	}; 

	public GAME_STATUS gameStatus;

	void Awave() {
		gameStatus = GAME_STATUS.Pause;
        fadeController = GameObject.Find("FadeEvent").GetComponent<FadeManager>();
        pause = GameObject.Find("GameLogic").GetComponent<PauseSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		GameStatusUpData ();
	}

	void GameStatusUpData( ) {

	}
		
}
