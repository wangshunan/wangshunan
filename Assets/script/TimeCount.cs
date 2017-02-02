using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;
    
    Text minute;
    Text second;

    private const int SECOND_MAX = 60;
    private const int SECOND_MIN = 0;
    private const int MINUTE_MAX = 3;
    private const int MINUTE_MIN = 0;

    public bool timeOver;
    private float secondNow;
    private float minuteNow;

    void Awake() {
        timeOver = false;
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
        minute = GameObject.Find( "Minute" ).GetComponent<Text>();
        second = GameObject.Find( "Second" ).GetComponent<Text>();
        minuteNow = 3.0f;
        secondNow = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if( gameLogic.gameStatus != GameLogic.GAME_STATUS.Start ) {
            return;
        }

        if ( !timeOver ) {
            TimeCountUpdate( );
            TimeUpdate( );
        }
	}

    void TimeUpdate() {
        minute.text = "" + (int)minuteNow;
        second.text = ( secondNow >= 10.0f ? "" : "0" ) + (int)secondNow;
    }

    void TimeCountUpdate() {

        if ( (int)secondNow <= SECOND_MIN ) {          
            if ( (int)minuteNow - 1 >= MINUTE_MIN ) {
                secondNow = SECOND_MAX;
                minuteNow -= 1;
            }
        }
        if ( ( int )secondNow == SECOND_MIN && ( int )minuteNow == MINUTE_MIN ) {
            timeOver = true;
        } else {
            secondNow -= Time.deltaTime;
        }
    }

    void TimeOverEvent() {

    }
}
