using UnityEngine;
using System.Collections;

public class TalkingBtn : MonoBehaviour {
	public GameObject startBtn;
	public SoundManager soundManager;

	public void LoadSceneBattle() {
		Application.LoadLevel("Battle");
	}

	public void OnStartBtnClicked(){
		Invoke ("LoadSceneBattle", 1);
		soundManager.PlaySeGameStart ();
	}

}