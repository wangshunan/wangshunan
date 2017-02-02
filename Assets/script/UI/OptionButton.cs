using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionButton : MonoBehaviour {
    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    PauseSystem pause;

    [SerializeField]
    FadeManager fadeController;

    public GameObject pausePlane;
    public GameObject backGround;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject pauseText;
    public GameObject moveButton;
    private GameObject pauseButton;

    private const float scalePos = 0.01f;
    private const float maxScalePos = 1.0f;
    private const float speed = 0.1f;
    private Vector3 maxScale;
    private bool isScale = false;

	// Use this for initialization
    void Awake() {  
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
        fadeController = GameObject.Find( "FadeEvent" ).GetComponent<FadeManager>();
        pause = GameObject.Find( "GameLogic" ).GetComponent<PauseSystem>();
        pauseButton = GameObject.Find( "OptionButton" );
        backGround.transform.localScale = new Vector3( scalePos, scalePos, maxScalePos );
        maxScale = new Vector3( maxScalePos, maxScalePos, maxScalePos );
    }
	
	// Update is called once per frame
	void Update () {

        if ( gameLogic.gameStatus == GameLogic.GAME_STATUS.Start ) {
            pauseButton.SetActive( true );
        } else {
            pauseButton.SetActive( false );
        }

        if ( isScale ) {
            backGround.transform.localScale = Vector3.MoveTowards( backGround.transform.localScale, pausePlane.transform.localScale, speed );
        }

        if ( backGround.transform.localScale == maxScale ) {
            isScale = false;
            pauseText.SetActive( true );
            yesButton.SetActive( true );
            noButton.SetActive( true );
        } else { 
            pauseText.SetActive( false );
            yesButton.SetActive( false );
            noButton.SetActive( false );
        }		

	}

    public void OnYesButtonClicked() {
        fadeController.sceneName = "TitleMenu";
        fadeController.fadeOutOver = true;
    }

    public void OnNoButtonClicked() {
        backGround.transform.localScale = new Vector3( scalePos, scalePos, maxScalePos );
        pausePlane.SetActive( false );
        moveButton.SetActive( true );
        pause.GameResume();
    }

    public void OnPauseButtonClicke() {
        moveButton.SetActive( false );
        pausePlane.SetActive( true );
        isScale = true;
        pause.GamePause();
    }
}
