using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BalloonController : MonoBehaviour {

    private float count;
    bool countOpen;
    private GameObject balloon;
    private GameObject balloonText;
    public Slider playerBooldPressureSlider;
	public Slider bossHp;
    private Vector3 position;
    private bool boold = true;

    public string[] scenarios;
	private GameObject player;
    private string currentText = string.Empty;
	[SerializeField]Text uiText;
	enum GAMESTATUS {
		start,
		clear,
		over
	};

    private int rand;
	private int gameStatus;
	private bool gameOpen;
	private bool balloonOpen;

	private AnimatorStateInfo currentBaseState;

	static int jump = Animator.StringToHash("Base Layer.Jump");

	void Awake() {
		gameStatus = 0;
		balloonOpen = true;
		gameOpen = true;
		balloon = GameObject.Find( "Balloon" );
		balloonText = GameObject.Find( "BalloonText" );
		position = balloon.transform.position;
		rand = Random.Range( 0 , 7 );
		currentText = scenarios [rand];
		player = GameObject.Find ("Cguy");
	}

	// Use this for initialization
	void Start () {
		currentBaseState = player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
	}
	
	// Update is called once per frame
	void Update () {
		if ( gameOpen == false ) {
			if ( balloonOpen == true ) {
				if (balloonOpen == true && playerBooldPressureSlider.value == 100 ) {
					currentText = scenarios [8];
					uiText.text = currentText.Substring (0, currentText.Length);
					balloon.transform.position = position;
				}

				if (balloonOpen == true && bossHp.value == 0 ) {
					currentText = scenarios [9];
					uiText.text = currentText.Substring (0, currentText.Length);
					balloon.transform.position = position;
				}
			}
		} else {
			Ballon ();
		}
	}

	void Ballon() {

		count += Time.deltaTime;
		if (( count >= 3.0f && count < 6.0f ) || currentBaseState.fullPathHash == jump ) {
			//balloon.transform.position = new Vector3 (0, -100);
			balloon.SetActive( false );
		} else {				
			if (playerBooldPressureSlider.value < 50) {
				boold = true;
			}
			if ( player.transform.position.y <= -1.15297f ) {
				if (playerBooldPressureSlider.value >= 50 && boold == true) {
					currentText = scenarios [7];
					count = 0.0f;
					//balloon.transform.position = position;
					balloon.SetActive( true );
					boold = false;
				} else if (count >= 6.0f) {
					count = 0.0f;
					rand = Random.Range (0, 7);
					currentText = scenarios [rand];
					//balloon.transform.position = position;
					balloon.SetActive( true );
				}
				if (playerBooldPressureSlider.value == 100) {
					currentText = scenarios [8];
					//balloon.transform.position = position;
					balloon.SetActive( true );
				}
			}
			uiText.text = currentText.Substring (0, currentText.Length);
		}
	}

	public void BalloonDestroy(){
		balloon.transform.position = new Vector3 (0, -100);
		balloon.SetActive( false );
		balloonOpen = false;
	}
		

	public void Clear( ){
		gameOpen = false;
		gameStatus = (int)GAMESTATUS.clear;
	}
	public void Over( ) {
		gameOpen = false;
		gameStatus = (int)GAMESTATUS.over;
	}
}
