using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager: MonoBehaviour {

    [SerializeField]
    PauseSystem pause;

    [SerializeField]
    GameLogic gameLogic;

    [SerializeField]
    GameObject fadeController;

    private Color fadeColor;
    public bool fadeInOver = true;
    public bool fadeOutOver = false;
    public string sceneName;

	// Use this for initialization
	void Awake () {
	    pause = GameObject.Find( "GameLogic" ).GetComponent<PauseSystem>();
        gameLogic = GameObject.Find( "GameLogic" ).GetComponent<GameLogic>();
        fadeController = GameObject.Find( "FadeController" );
        fadeColor = new Color( 0, 0, 0, 255 );
	}
	
	// Update is called once per frame
	void Update () {
	    FadeIn();
        FadeOut();
	}

    void FadeIn() {
        if ( !fadeInOver ) {
            return;
        }
        if ( fadeController.GetComponent<Image>().fillAmount > 0 ) {
            fadeController.GetComponent<Image>().fillAmount -= 0.02f;
        }

        if ( fadeController.GetComponent<Image>().fillAmount == 0 ) {
            fadeController.SetActive( false );
            fadeInOver = false;
            pause.GameResume();
        }
    }

    void FadeOut() {

        if ( fadeOutOver ) {
            fadeController.SetActive( true );
        } else {
            return;
        }

        if ( fadeController.GetComponent<Image>().fillAmount < 1 ) {
            fadeController.GetComponent<Image>().fillAmount += 0.02f;
        }

        if ( fadeController.GetComponent<Image>().fillAmount >= 1 ) {
            if ( sceneName != null ) {
                SceneManager.LoadScene( sceneName );
            }
        }
    }
}
