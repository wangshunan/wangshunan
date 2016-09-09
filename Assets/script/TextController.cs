using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public SoundManager soundManager;
	public VoiceManager voiceManager;

	public GameObject btn;
	public GameObject vodke;
	public string[] scenarios;
	[SerializeField]Text uiText;

	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private int currentLine = 0;
	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int lastUpdateCharacter = -1;
	private int count = 1;                    //Screenを押すカウント

	public bool IsCompleteDisplayText {
		get{ return Time.time > timeElapsed + timeUntilDisplay;}	
	}

	void Start () {
		
		SetNextLine ();
		voiceManager.PlayVoice(count);
		vodke = GameObject.Find ("Vodke");
	}
	
	// Update is called once per frame
	void Update () {
		if (IsCompleteDisplayText) {
			
			if (currentLine < scenarios.Length && Input.GetMouseButtonDown (0)) {
				count++;
				voiceManager.PlayVoice (count);
				soundManager.PlaySeButton ();
				SetNextLine ();
			}
			if (count == 11) {
				vodke.SetActive (false);
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				timeUntilDisplay = 0;

			} 

			if (currentText == scenarios [14]) {
				btn.SetActive (true);
			}
		} 

		int displayCharacterCount = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

		if (displayCharacterCount != lastUpdateCharacter) {
			uiText.text = currentText.Substring (0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}

	void SetNextLine() {
		currentText = scenarios [currentLine];
		currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;

		lastUpdateCharacter = -1;
	}

}
