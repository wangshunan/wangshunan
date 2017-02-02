using UnityEngine;
using System.Collections;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> origin/master

public class TitleController : MonoBehaviour {
    private enum BUTTON_LIST {
        NEW_GAME,
<<<<<<< HEAD
        OPTION,
        CREDIT,
=======
        //LOAD_GAME,
        OPTION,
        RECORD,
>>>>>>> origin/master
        EXIT,
        //OnStageIntroducePanel
        GO
    }
<<<<<<< HEAD
    [SerializeField]MoveController movieController;
    public GameObject MenuPanel;
    public GameObject VolumePanel;
    public GameObject StageSelectPanel;
=======
    public GameObject MenuPanel;
    public GameObject VolumePanel;
    public GameObject OptionPanel;
    public GameObject StageSelectPanel;
    public GameObject LevelSelectPanel;
    public GameObject LaunchPanel;
>>>>>>> origin/master
    public GameObject CharatePanel;
    public GameObject[] OptionBackButton;
    public GameObject StageSelectBackButton;
    public GameObject Exit;
    public GameObject[] BackButton;
	public GameObject StageIntrodudePanel;
<<<<<<< HEAD
	public GameObject CharacterIntrodudePanel;
    public GameObject MovieController;
	
    public GameObject[ ] MainButton;
    public GameObject[ ] ButtonEffect;

	public Slider BgmSlider;
	public Slider SeSlider;
	public Slider VoiceSlider;

	private const float startValue = 0.5f;



=======
	public GameObject RecordPanel;
	public GameObject CharacterIntrodudePanel;
	
    public GameObject[ ] MainButton;
    public GameObject[ ] ButtonEffect;
>>>>>>> origin/master
    //メーニュー切り替え処理

  
    public void OnMainButtonCliked( int ButtonName ) {
<<<<<<< HEAD
		
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
			
=======
        EffectOn( ButtonName );
        Invoke( "EffectOff", 1 ); 
        //ニューゲームボタン
        if ( ButtonName == ( int )BUTTON_LIST.NEW_GAME ) {
            MenuPanel.SetActive( false );
            StageSelectPanel.SetActive(true);
           
        }

        /*if( ButtonName == ( int )BUTTON_LIST.LOAD_GAME ) {
        }
        */
         //オプションメーニュー選択ボタン
        if( ButtonName == ( int )BUTTON_LIST.OPTION ) {
            MenuPanel.SetActive(false);
            VolumePanel.SetActive(true);
        }
		
        //レコードボタン
        if( ButtonName == ( int )BUTTON_LIST.RECORD ) {
            MenuPanel.SetActive( false );
		    RecordPanel.SetActive( true );

        }
        if( ButtonName == ( int )BUTTON_LIST.EXIT ) {
>>>>>>> origin/master
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
<<<<<<< HEAD

=======
>>>>>>> origin/master
       
    }
    
    void EffectOn( int button ) {
        ButtonEffect[ button ].SetActive( true );
    }
<<<<<<< HEAD
		
=======

    void EffectOff( ) {
        for( int i = 0; i < 5; i++ ) {
            ButtonEffect[ i ].SetActive( false );
        }
    
    }
    //レベル選択ボタン
    public void OnLevelSelectButtonCliked() {
        LevelSelectPanel.SetActive( false );
        StageSelectPanel.SetActive( true );

    }



    //言語選択メーニューボタン
    public void OnLaunchButtonClicked() {
        OptionPanel.SetActive( false );
        LaunchPanel.SetActive( true );

    }

    //文字サイズメーニュー選択ボタン
    public void OnCharateButtonCliked() {
        OptionPanel.SetActive( false );
        CharatePanel.SetActive( true );

    }

    //音量選択メーニューボタン
    public void OnVolumeButtonCliked() {
        OptionPanel.SetActive( false );
        VolumePanel.SetActive( true );

    }
>>>>>>> origin/master

    //一個前メーニューに戻るボタン
    public void OnOptionBackButton() {
        MenuPanel.SetActive(true);
        VolumePanel.SetActive(false);
<<<<<<< HEAD
		ButtonEffect[ 1 ].SetActive( false );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.Back);
=======
>>>>>>> origin/master
    }    

    public void OnStageSelectBackButtonCliked() {
        StageSelectPanel.SetActive( false );
        MenuPanel.SetActive(true);
<<<<<<< HEAD
		ButtonEffect[ 0 ].SetActive( false );
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.Back);
=======
>>>>>>> origin/master

    }
    public void OnStartButtonCliked() {
		StageSelectPanel.SetActive( false );
		StageIntrodudePanel.SetActive( true );
<<<<<<< HEAD
		SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonDecide);
=======

>>>>>>> origin/master
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
		
<<<<<<< HEAD
		//RecordPanel.SetActive( false );
=======
		RecordPanel.SetActive( false );
>>>>>>> origin/master
		CharacterIntrodudePanel.SetActive( true );
	}
	public void OnCharacterIntroduceBackButtonCliked() {
		CharacterIntrodudePanel.SetActive (false);
<<<<<<< HEAD
		//RecordPanel.SetActive (true);
=======
		RecordPanel.SetActive (true);
>>>>>>> origin/master

	}


    //メインメーニューに戻るボタン
    public void OnBackButtonCliked() {
<<<<<<< HEAD
        //OptionPanel.SetActive( false );
=======
        LevelSelectPanel.SetActive( false );
        //OptionPanel.SetActive( false );
		RecordPanel.SetActive( false );
>>>>>>> origin/master
        MenuPanel.SetActive( true );

    }

	public void OnStageIntroduceButtonCliked( int ButtonName) {
        EffectOn(ButtonName);
<<<<<<< HEAD
        //Invoke("EffectOff", 1);

        if (ButtonName == (int)BUTTON_LIST.GO)
        {
            Invoke("LoadSceneTalking", 1);
			SoundManager.Instance.PlaySE ((int)SoundManager.SE_LIST.ButtonGo);

=======
        Invoke("EffectOff", 1);
        //
        if (ButtonName == (int)BUTTON_LIST.GO)
        {
            Invoke("LoadSceneTalking", 1);
>>>>>>> origin/master
        }

	}

    public void OnNotYetButtonClicked()
    {

        StageIntrodudePanel.SetActive(false);
        StageSelectPanel.SetActive(true);
<<<<<<< HEAD
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
=======

    }
>>>>>>> origin/master
}
