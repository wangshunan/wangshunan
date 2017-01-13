using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
    private enum BUTTON_LIST {
        NEW_GAME,
        LOAD_GAME,
        OPTION,
        RECORD,
        EXIT,
        //OnStageIntroducePanel
        GO
    }
    public GameObject MenuPanel;
    public GameObject VolumePanel;
    public GameObject OptionPanel;
    public GameObject StageSelectPanel;
    public GameObject LevelSelectPanel;
    public GameObject LaunchPanel;
    public GameObject CharatePanel;
    public GameObject[] OptionBackButton;
    public GameObject StageSelectBackButton;
    public GameObject Exit;
    public GameObject[] BackButton;
	public GameObject StageIntrodudePanel;
	public GameObject RecordPanel;
	public GameObject CharacterIntrodudePanel;
	
    public GameObject[ ] MainButton;
    public GameObject[ ] ButtonEffect;
    //メーニュー切り替え処理

  
    public void OnMainButtonCliked( int ButtonName ) {
        EffectOn( ButtonName );
        Invoke( "EffectOff", 1 ); 
        //ニューゲームボタン
        if ( ButtonName == ( int )BUTTON_LIST.NEW_GAME ) {
            MenuPanel.SetActive( false );
            StageSelectPanel.SetActive(true);
           
        }
        if( ButtonName == ( int )BUTTON_LIST.LOAD_GAME ) {
        }
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

    //一個前メーニューに戻るボタン
    public void OnOptionBackButton() {
        MenuPanel.SetActive(true);
        VolumePanel.SetActive(false);
    }    

    public void OnStageSelectBackButtonCliked() {
        StageSelectPanel.SetActive( false );
        MenuPanel.SetActive(true);

    }
    public void OnStartButtonCliked() {
		StageSelectPanel.SetActive( false );
		StageIntrodudePanel.SetActive( true );

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
		
		RecordPanel.SetActive( false );
		CharacterIntrodudePanel.SetActive( true );
	}
	public void OnCharacterIntroduceBackButtonCliked() {
		CharacterIntrodudePanel.SetActive (false);
		RecordPanel.SetActive (true);

	}


    //メインメーニューに戻るボタン
    public void OnBackButtonCliked() {
        LevelSelectPanel.SetActive( false );
        //OptionPanel.SetActive( false );
		RecordPanel.SetActive( false );
        MenuPanel.SetActive( true );

    }

	public void OnStageIntroduceButtonCliked( int ButtonName) {
        EffectOn(ButtonName);
        Invoke("EffectOff", 1);
        //
        if (ButtonName == (int)BUTTON_LIST.GO)
        {
            Invoke("LoadSceneTalking", 1);
        }

	}

    public void OnNotYetButtonClicked()
    {

        StageIntrodudePanel.SetActive(false);
        StageSelectPanel.SetActive(true);

    }
}
