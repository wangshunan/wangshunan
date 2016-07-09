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
    public void NewGameButtonClicked() {
        MenuPanel.SetActive( false );
        LevelSelectPanel.SetActive( true );
    }

    //レベル選択ボタン
    public void LevelSelectButtonCliked() {
        LevelSelectPanel.SetActive( false );
        StageSelectPanel.SetActive( true );
    }

    //オプションメーニュー選択ボタン
    public void OptionButtonClicked() {
        MenuPanel.SetActive( false );
        OptionPanel.SetActive( true );
    }

    //言語選択メーニューボタン
    public void LaunchButtonClicked() {
        OptionPanel.SetActive( false );
        LaunchPanel.SetActive( true );
    }

    //文字サイズメーニュー選択ボタン
    public void CharateButtonCliked() {
        OptionPanel.SetActive( false );
        CharatePanel.SetActive( true );
    }

    //音量選択メーニューボタン
    public void VolumeButtonCliked() {
        OptionPanel.SetActive( false );
        VolumePanel.SetActive( true );
    }
	//レコードボタン
	public void RecordButtonCliked() {
		MenuPanel.SetActive( false );
		RecordPanel.SetActive( true );
	}
    //一個前メーニューに戻るボタン
    public void OptionBackButtonCliked() {
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

    public void StageSelectBackButtonCliked() {
        StageSelectPanel.SetActive( false );
        LevelSelectPanel.SetActive( true );
    }
    public void StartButtonCliked() {
		StageSelectPanel.SetActive( false );
		StageIntrodudePanel.SetActive( true );
    }

	public void TalkingScenePanelTouch() {
		Application.LoadLevel ("Battle");
	}
	public void StageIntroduceGoButtonCliked() {
		Application.LoadLevel ("Talking");
	}
	public void RecordSyagaButtonCliked() {
		RecordPanel.SetActive( false );
		CharacterIntrodudePanel.SetActive( true );
	}
	public void CharacterIntroduceBackButtonCliked() {
		CharacterIntrodudePanel.SetActive (false);
		RecordPanel.SetActive (true);
	}
	public void NotYetButtonClicked(){
		StageIntrodudePanel.SetActive (false);
		StageSelectPanel.SetActive (true);
	}

    //メインメーニューに戻るボタン
    public void BackButtonCliked() {
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
