using UnityEngine;
using System.Collections;

public class TimeOverEvent : MonoBehaviour {

    [SerializeField]
    public GameObject backGround;

    [SerializeField]
    TimeCount timeCount;

    [SerializeField]
    GameLogic gameLogic;

    public bool eventOver;

    private const float scalePos = 0;
    private const float maxScalePos = 1.0f; 
    private const float speed = 0.8f;
    private Vector3 maxScale;
    private Transform targetTrans;
    private bool isScale = true;

    // Use this for initialization
    void Awake( ) {
        timeCount = GameObject.Find( "GameLogic" ).GetComponent<TimeCount>( );
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>( );
        transform.localScale = new Vector3( scalePos, scalePos, maxScalePos );
        targetTrans = GameObject.Find( "TimeCount" ).transform;
        maxScale = new Vector3( maxScalePos, maxScalePos, maxScalePos );
        eventOver = false;
    }

    // Update is called once per frame
    void Update( ) {
        if ( !timeCount.timeOver ) {
            return;
        }

        if ( isScale ) {
            eventOver = true;
            gameLogic.gameStatus = GameLogic.GAME_STATUS.Over;
            transform.localScale = Vector3.MoveTowards( transform.localScale, targetTrans.localScale, speed * Time.deltaTime );
        }

        if ( transform.localScale == maxScale ) {
            eventOver = false;
            isScale = false;
            gameObject.SetActive( false );
        }
    }
}
