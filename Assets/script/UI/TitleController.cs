using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

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
	public GameObject Chicken;



    //メーニュー切り替え処理


    //ニューゲームボタン
    public void OnNewGameButtonClicked() {
        MenuPanel.SetActive( false );
        LevelSelectPanel.SetActive( true );

    }

    //レベル選択ボタン
    public void OnLevelSelectButtonCliked() {
        LevelSelectPanel.SetActive( false );
        StageSelectPanel.SetActive( true );

    }

    //オプションメーニュー選択ボタン
    public void OnOptionButtonClicked() {
        MenuPanel.SetActive( false );
        OptionPanel.SetActive( true );

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
	//レコードボタン
	public void OnRecordButtonCliked() {
		MenuPanel.SetActive( false );
		RecordPanel.SetActive( true );

	}
    //一個前メーニューに戻るボタン
    public void OnOptionBackButton() {
        if ( LaunchPanel == true ) {
            LaunchPanel.SetActive( false );
        }
        if ( VolumePanel == true ) {
            VolumePanel.SetActive( false );
        }
        if ( CharatePanel == true ) {
            CharatePanel.SetActive( false );
        }
        OptionPanel.SetActive( true );


    }    

    public void OnStageSelectBackButtonCliked() {
        StageSelectPanel.SetActive( false );
        LevelSelectPanel.SetActive( true );

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
	public void OnNotYetButtonClicked(){
		StageIntrodudePanel.SetActive (false);
		StageSelectPanel.SetActive (true);

	}

    //メインメーニューに戻るボタン
    public void OnBackButtonCliked() {
        LevelSelectPanel.SetActive( false );
        OptionPanel.SetActive( false );
		RecordPanel.SetActive( false );
        MenuPanel.SetActive( true );

    }

    public void ExitButtonCliked()
	{
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

	public void OnStageIntroduceButtonCliked() {
		
	}
}
