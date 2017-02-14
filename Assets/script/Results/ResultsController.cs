using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour {

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    PauseSystem pause;

    [SerializeField]
    TimeOverEvent timeOverEvent;

    [SerializeField]
    FadeManager fadeController;

	const float MAX_HP = 100;
	const float HALF_HP = 50;
	const float MIN_HP = 0;

	float alpha = 0.0f;
	public GameObject retry;
	public GameObject title;
	public GameObject moveControllerPanel;
    public GameObject timeOver;
    private GameObject continuePanel;
    private GameObject balloonStatus;

	public GameObject bossHp;
	public GameObject playerHp;
	public GameObject stamina;

	public GameObject clearImage;
	public GameObject overImage;
    public GameObject continueImage;
	public GameObject player;

	void Awake( ) {
        continuePanel = GameObject.Find("Continue");
		balloonStatus = GameObject.Find ("TextController");
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        pause = GameObject.Find( "GameLogic" ).GetComponent<PauseSystem>();
        timeOverEvent = GameObject.Find( "TimeOverEvnet" ).GetComponent<TimeOverEvent>();
        fadeController = GameObject.Find( "FadeEvent" ).GetComponent<FadeManager>();
    }

	// Use this for initialization
	void Start () {
         overImage.SetActive( false );
         clearImage.SetActive( false );
	}

	// Update is called once per frame
	void Update () {
	}

	public void GameClear( ) {
		moveControllerPanel.SetActive (false);
		bossHp.SetActive (false);
		playerHp.SetActive (false);
		stamina.SetActive (false);
		if (alpha <= 0.6f) {
			// フェードアウト
			alpha += 0.01f;
            continuePanel.GetComponent<Image>().color = new Color( 255, 255, 255, alpha );
		}

        if ( alpha > 0.6f && clearImage.transform.position.y > Screen.height * 0.5f ) {
            clearImage.SetActive( true );
			clearImage.transform.position -= new Vector3 ( 0, 2, 0 );
		}

		if ( clearImage.transform.position.y <= Screen.height * 0.5f ) {
			if ( SceneManager.GetSceneByName( "BossStage" ).isLoaded ) {
				fadeController.sceneName = "TalkingBoss";
			} else {
				fadeController.sceneName = "TalkingZako";
			}
            fadeController.fadeOutOver = true;
		}
	}

	public void GameOver() {

		if (alpha <= 0.6f) {
			// フェードアウト
			alpha += 0.01f;
            continuePanel.GetComponent<Image>().color = new Color( 255, 255, 255, alpha );
		}

        if ( timeOverEvent.eventOver ) {
            return;
        }

		if ( alpha > 0.6f && overImage.transform.position.y > Screen.height * 0.7f ) {
            overImage.SetActive( true );
			bossHp.SetActive (false);
			playerHp.SetActive (false);
			stamina.SetActive (false);
			overImage.transform.position -= new Vector3 (0, 2, 0 );
		}

		if (overImage.transform.position.y <= Screen.height * 0.7f) {
            moveControllerPanel.SetActive( false );
            continueImage.SetActive( true );
			retry.SetActive (true);
			title.SetActive (true);
		}
	}

	public void OnRetryButtonClicked() {
		fadeController.sceneName = "Battle";
        fadeController.fadeOutOver = true;
	}

	public void OnOverButtonClicked() {
		fadeController.sceneName = "TitleMenu";
        fadeController.fadeOutOver = true;
	}

    public void OnLoseButton( ) {
        gameLogic.gameStatus = GameLogic.GAME_STATUS.Over;
    }


    public void OnClearButton( ) {
        gameLogic.gameStatus = GameLogic.GAME_STATUS.Clear;
    }

}
