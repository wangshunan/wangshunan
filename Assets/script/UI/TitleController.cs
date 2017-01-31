using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    private enum BUTTON_LIST {
        NEW_GAME,
        OPTION,
        CREDIT,
        EXIT,
        //OnStageIntroducePanel
        GO
    }
    [SerializeField]MoveController movieController;
    public GameObject MenuPanel;
    public GameObject VolumePanel;
    public GameObject StageSelectPanel;
    public GameObject CharatePanel;
    public GameObject[] OptionBackButton;
    public GameObject StageSelectBackButton;
    public GameObject Exit;
    public GameObject[] BackButton;
	public GameObject StageIntrodudePanel;
	public GameObject CharacterIntrodudePanel;
    public GameObject MovieController;
	
    public GameObject[ ] MainButton;
    public GameObject[ ] ButtonEffect;

	public Slider BgmSlider;
	public Slider SeSlider;
	public Slider VoiceSlider;

	private const float startValue = 0.5f;



    //メーニュー切り替え処理

  
    public void OnMainButtonCliked( int ButtonName ) {
		
        EffectOn( ButtonName );
        Invoke( "EffectOff", 1 ); 
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
        //ニューゲームボタン
        if ( ButtonName == ( int )BUTTON_LIST.NEW_GAME ) {
			
            MenuPanel.SetActive( false );
            StageSelectPanel.SetActive(true);

        }

         //オプションメーニュー選択ボタン
		else if( ButtonName == ( int )BUTTON_LIST.OPTION ) {
			
            MenuPanel.SetActive(false);
            VolumePanel.SetActive(true);

        }
        //レコードボタン
		else if( ButtonName == ( int )BUTTON_LIST.CREDIT ) {
			
            MenuPanel.SetActive( false );
			Application.LoadLevel ("Title");
        }
        else if( ButtonName == ( int )BUTTON_LIST.EXIT ) {
			
            //If we are running in a standalone build of the game
	        #if UNITY_STANDALONE
		    //Quit the application
		    Application.Quit();
    	    #endif

		    //If we are running in the editor
	        #if UNITY_EDITOR
		    //Stop playing the scene
		    UnityEditor.EditorApplication.isPlaying = false;
	        #endif
        }

       
    }
    
    void EffectOn( int button ) {
        ButtonEffect[ button ].SetActive( true );
    }
		

    //一個前メーニューに戻るボタン
    public void OnOptionBackButton() {
        MenuPanel.SetActive(true);
        VolumePanel.SetActive(false);
		ButtonEffect[ 1 ].SetActive( false );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.Back);
    }    

    public void OnStageSelectBackButtonCliked() {
        StageSelectPanel.SetActive( false );
        MenuPanel.SetActive(true);
		ButtonEffect[ 0 ].SetActive( false );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.Back);

    }
    public void OnStartButtonCliked() {
		StageSelectPanel.SetActive( false );
		StageIntrodudePanel.SetActive( true );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
    }

	/*public void TalkingScenePanelTouch() {
		soundManager.PlaySeGameStart ();
		Invoke ("LoadSceneBattle", 2);
	}
	public void LoadSceneBattle() {
		Application.LoadLevel ("Battle");
	}*/
	public void OnStageIntroduceGoButtonCliked() {
		
		Invoke ("LoadSceneTalking", 1);
	}
	public void LoadSceneTalking() {
		Application.LoadLevel ("Talking");
	}
	public void OnRecordSyagaButtonCliked() {
		
		//RecordPanel.SetActive( false );
		CharacterIntrodudePanel.SetActive( true );
	}
	public void OnCharacterIntroduceBackButtonCliked() {
		CharacterIntrodudePanel.SetActive (false);
		//RecordPanel.SetActive (true);

	}


    //メインメーニューに戻るボタン
    public void OnBackButtonCliked() {
        //OptionPanel.SetActive( false );
        MenuPanel.SetActive( true );

    }

	public void OnStageIntroduceButtonCliked( int ButtonName) {
        EffectOn(ButtonName);
        //Invoke("EffectOff", 1);

        if (ButtonName == (int)BUTTON_LIST.GO)
        {
            Invoke("LoadSceneTalking", 1);
			SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonGo);

        }

	}

    public void OnNotYetButtonClicked()
    {

        StageIntrodudePanel.SetActive(false);
        StageSelectPanel.SetActive(true);
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
    }



	void Awake( ) {
		SoundManager.Instance.PlayBGM ((int)SoundManager.BGM_LIST.TilteMenu);
		BgmSlider.value = startValue;
		SeSlider.value = startValue;
		VoiceSlider.value = startValue;
		
	}
	void Update( ) {
		SoundManager.Instance.BGMsource.volume = BgmSlider.value;
		SoundManager.Instance.SEsources.volume = SeSlider.value;
		SoundManager.Instance.VoiceSources.volume = VoiceSlider.value;

	}
}
