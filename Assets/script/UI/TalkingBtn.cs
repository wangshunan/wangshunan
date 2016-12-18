using UnityEngine;
using System.Collections;

public class TalkingBtn : MonoBehaviour {
	public GameObject startBtn;


	public void LoadSceneBattle() {
		Application.LoadLevel("Battle");
	}

	public void OnStartBtnClicked(){
		Invoke ("LoadSceneBattle", 1);

	}

}