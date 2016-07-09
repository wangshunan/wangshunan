using UnityEngine;
using System.Collections;

public class BattleUiController : MonoBehaviour {
	public GameObject BattlePanel;
	public GameObject PauseMenuPanel;
	public GameObject RestartSurePanel;
	public GameObject OverSurePanel;
	public GameObject OptionPanel;
	public GameObject LaunchPanel;
	public GameObject CharatePanel;
	public GameObject VolumePanel;
	public GameObject CharacterPanel;
	public GameObject PlayerDetails;
	public GameObject ItemPanel;

	//メーニューボタン
	public void OnBattleMenuCliked() {
		PauseMenuPanel.SetActive( true );
	}

	//パスメーニューの切り替え処理

	//再開ボタン
	public void OnGameBackButtonCliked() {
		PauseMenuPanel.SetActive( false );
		BattlePanel.SetActive( true );
	}
	//リスタートボタン
	public void OnRestartButtonCliked() {
		PauseMenuPanel.SetActive( false );
		RestartSurePanel.SetActive( true );
	}
	//終了ボタン
	public void OnOverButtonCliked() {
		PauseMenuPanel.SetActive( false );
		OverSurePanel.SetActive( true );
	}
	//オプションボタン
	public void OnOptionButtonCliked() {
		OptionPanel.SetActive( true );
		PauseMenuPanel.SetActive( false );
	}
	//リスタートのYESボタン
	public void OnRestartSurePanelYesButtonCliked() {
		Application.LoadLevel( "Battle" );
	}
	//リスタートのNOボタン
	public void OnRestartSurePanleNoButtonClicked() {
		RestartSurePanel.SetActive( false );
		BattlePanel.SetActive( true );
	}
	//終了のYESボタン
	public void OnOverSurePanelYesButtonCliked() {
		Application.LoadLevel( "Title" );
	}
	//終了のNOボタン
	public void OnOverSurePanelNoButtonCliked() {
		OverSurePanel.SetActive( false );
		PauseMenuPanel.SetActive( true );
	}

	public void OnOverStatePanelButtonClicked() {
		CharacterPanel.SetActive (false);
		PlayerDetails.SetActive (true);
	}

	public void OnOverQPanelButtonClicked() {
		CharacterPanel.SetActive (false);
		ItemPanel.SetActive (true);
	}

	public void OnCharacterButtonClicked() {
		CharacterPanel.SetActive (true);
		BattlePanel.SetActive (false);
	}

	public void OnBackClicked(){
		CharacterPanel.SetActive (false);
		BattlePanel.SetActive (true);
	}

	public void OnBackButtonClicked(){
		ItemPanel.SetActive (false);
		CharacterPanel.SetActive (true);
	}

	public void OnCharacterBackButtonClicked(){
		PlayerDetails.SetActive (false);
		CharacterPanel.SetActive (true);
	}

	//オプションメーニュー

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
	//オプションに戻るのBACKボタン
	public void OnOptionBackButtonCliked() {
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

	//オプション画面のBACKボタン
	public void OnOptionPanelBackButtonCliked() {
		OptionPanel.SetActive( false );
		BattlePanel.SetActive( true) ;
	}
}
