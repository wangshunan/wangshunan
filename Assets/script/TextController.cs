using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour {

    [SerializeField] GameObject moveController;

	public GameObject StartButton;
	public GameObject vodke;
	public GameObject SkipButton;

	public string[] scenarios;
    public string[] scenariosOfZako;
    public string[] scenariosOfBoss;
  
	[SerializeField]Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private int currentLine = 0;
	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int lastUpdateCharacter = -1;
	public int screenCount = 1;                    //Screenを押すカウント

	bool IsStartButtonActive = false;
	Vector3 pos = new Vector3( 0, 0, 0 );

	public bool IsCompleteDisplayText {
		get{ return Time.time > timeElapsed + timeUntilDisplay;}	
	}
	void Awake( ) { 
		
	}
	void Start( ) {
        vodke = GameObject.Find( "Vodke" );
		SkipButton = GameObject.Find ( "SkipButton" );
        
		if ( SceneManager.GetSceneByName( "Talking" ).isLoaded ) {
			
            SoundManager.Instance.PlayVoice ( (int)SoundManager.VOICE_LIST.DIALOG_1 );
			SoundManager.Instance.PlayBGM ((int)SoundManager.BGM_LIST.Senario_1);
	        SetNextLine( 0 );
        } else if ( SceneManager.GetSceneByName( "TalkingZako" ).isLoaded ) {
			
            SoundManager.Instance.PlayVoice ( (int)SoundManager.VOICE_LIST.DIALOG_16 );
			SoundManager.Instance.PlayBGM ((int)SoundManager.BGM_LIST.Senario_2);
            screenCount = 16;
	        SetNextLine( 0 );
        } else if ( SceneManager.GetSceneByName( "TalkingBoss" ).isLoaded ) {
			
            SoundManager.Instance.PlayVoice ( (int)SoundManager.VOICE_LIST.DIALOG_22 );
			SoundManager.Instance.PlayBGM ((int)SoundManager.BGM_LIST.Senario_3);
            screenCount = 22;
	        SetNextLine( 0 );
        }

     
	}
	
	// Update is called once per frame
	void Update( ) {
		if (IsCompleteDisplayText) {
			if (/*currentLine < scenarios.Length && */Input.GetMouseButtonDown (0) && screenCount < 25 && IsStartButtonActive == false ) {
				
				SetNextLine( 0 );
                screenCount++;
				SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
				SoundManager.Instance.PlayVoice(screenCount);
			}
			/*if (screenCount >= 11) {
				//vodkeTrans.position += new Vector3( 10, 0, 0 );
				vodkeTrans.position = Vector3.MoveTowards( pos, targetTrans.position, 10 * Time.deltaTime );
			}*/
		} else {
			if (Input.GetMouseButtonDown (0)) {
				timeUntilDisplay = 0;
			} 

			if ( currentText == scenarios [14] || currentText == scenariosOfZako[ 5 ] || currentText == scenariosOfBoss[ 3 ] ) {
				Invoke ("StartButtonAlive", 1);
			}
		} 

		int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

		if ( displayCharacterCount != lastUpdateCharacter ) {
			uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

	public void SetNextLine(int LineCount) {
		LineCount = currentLine;
        if ( screenCount > 14 && screenCount <= 21) {
            currentText = scenariosOfZako [currentLine];
        } else if( screenCount > 21  ) {
            currentText = scenariosOfBoss [currentLine];
        } else {
		currentText = scenarios [currentLine];
        }
		currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;

		lastUpdateCharacter = -1;
	}

	public void OnSkipButtonCliked() {	
		currentText = scenarios [15];
        IsStartButtonActive = true;
		StartButton.SetActive (true);
		SkipButton.SetActive (false);
        SoundManager.Instance.StopVoice( );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
	}
	public void LoadSceneBattle() {
		Application.LoadLevel("tutorial");
	}

	public void OnStartButtonClicked(){
		Invoke ("LoadSceneBattle", 1);
		currentText = scenarios [15];
        SoundManager.Instance.StopVoice( );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
	}
	public void StartButtonAlive( ) {
		StartButton.SetActive (true);
	}
    
    public void BossStageButtonClicked() {
        SceneManager.LoadScene( "BossStage" );
        SoundManager.Instance.StopVoice( );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
    }

    public void tsetButtonClicked() {
        SoundManager.Instance.StopVoice( );
	    SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
		SceneManager.LoadScene( "Title" );
    }
}
