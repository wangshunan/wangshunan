using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public SoundManager soundManager;
	public VoiceManager voiceManager;

	public GameObject StartButton;
	public GameObject vodke;
	public GameObject SkipButton;
	public string[] scenarios;
	[SerializeField]Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private int currentLine = 0;
	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int lastUpdateCharacter = -1;
	private int count = 1;//Screenを押すカウント

	bool IsStartButtonActive = false;


	public bool IsCompleteDisplayText {
		get{ return Time.time > timeElapsed + timeUntilDisplay;}	
	}

	void Start () {
		
		SetNextLine (0);
		voiceManager.PlayVoice(count);
		vodke = GameObject.Find ("Vodke");
		SkipButton = GameObject.Find ("SkipButton");

	}
	
	// Update is called once per frame
	void Update () {
		if (IsCompleteDisplayText) {
			
			if (currentLine < scenarios.Length && Input.GetMouseButtonDown (0) && count < 15 && IsStartButtonActive == false) {
				count++;
				voiceManager.PlayVoice (count);
				soundManager.PlaySeButton ();
				SetNextLine (0);
			}
			if (count == 11) {
				vodke.SetActive (false);
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				timeUntilDisplay = 0;

			} 

			if (currentText == scenarios [14]) {
				Invoke ("StartButtonAlive", 2);
			}
		} 

		int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

		if (displayCharacterCount != lastUpdateCharacter) {
			uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

	public void SetNextLine(int LineCount) {
		LineCount = currentLine;
		currentText = scenarios [currentLine];
		currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;

		lastUpdateCharacter = -1;
	}

	public void OnSkipButtonCliked() {
		
		currentText = scenarios [15];
		soundManager.PlaySeButton ();
		voiceManager.StopVoice ();
		IsStartButtonActive = true;
		StartButton.SetActive (true);
		SkipButton.SetActive (false);

	}
	public void LoadSceneBattle() {
		Application.LoadLevel("tutorial");
	}

	public void OnStartButtonClicked(){
		Invoke ("LoadSceneBattle", 1);
		soundManager.PlaySeGameStart ();
		soundManager.StopSeButton ();
		currentText = scenarios [15];
		voiceManager.StopVoice ();
	}
	public void StartButtonAlive() {
		StartButton.SetActive (true);
	}


}
