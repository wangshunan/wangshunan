using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour {

    [SerializeField]
    ResultsManager resultsManager;

    [SerializeField]
    PauseSystem pause;

    [SerializeField]
    TimeOverEvent timeOverEvent;

	const float MAX_HP = 100;
	const float HALF_HP = 50;
	const float MIN_HP = 0;

	float alpha = 0.0f;
	private int Color;
	public GameObject retry;
	public GameObject title;
	public GameObject moveControllerPanel;
    public GameObject timeOver;
    private GameObject continuePanel;
    private GameObject balloonStatus;

	public GameObject bossHp;
	public GameObject playerHp;
	public GameObject stamina;

	private GameObject boss;
	private GameObject enemy;
	public GameObject clearImage;
	public GameObject overImage;
    public GameObject continueImage;
	public GameObject player;

	void Awake( ) {
        continuePanel = GameObject.Find("Continue");
		balloonStatus = GameObject.Find ("TextController");
        resultsManager.GetComponent<ResultsManager>();
        pause = GameObject.Find( "GameLogic" ).GetComponent<PauseSystem>();
        timeOverEvent = GameObject.Find( "Image" ).GetComponent<TimeOverEvent>( );
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

        if ( alpha > 0.6f && clearImage.transform.position.y > Screen.height * 0.7f && timeOverEvent.eventOver ) {
            clearImage.SetActive( true );
			//balloonStatus.GetComponent<BalloonController> ().BalloonDestroy ();
			clearImage.transform.position -= new Vector3 ( 0, 2, 0 );
		}

		if ( clearImage.transform.position.y <= Screen.height * 0.7f ) {
            SceneManager.LoadScene( "TalkingZako" );
		}
	}

	public void GameOver() {

		if (alpha <= 0.6f) {
			// フェードアウト
			alpha += 0.01f;
            continuePanel.GetComponent<Image>().color = new Color( 255, 255, 255, alpha );
		}

		if ( alpha > 0.6f && overImage.transform.position.y > Screen.height * 0.7f ) {
            timeOver.SetActive( false );
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
		SceneManager.LoadScene ("Battle");
	}

	public void OnOverButtonClicked() {
		SceneManager.LoadScene ("TitleMenu");
	}

    public void OnLoseButton( ) {
        resultsManager.test = (int)GameLogic.GAME_STATUS.Over;
    }


    public void OnClearButton( ) {
        resultsManager.test = (int)GameLogic.GAME_STATUS.Clear;
    }

}
