using UnityEngine;
using System;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public enum BGM_LIST{
		MENU,
		ZAKO
	}
	public enum SE_LIST {
		BACK,
		BUTTON,
		GAME_START,
		PUNCH,
		BLOCK
	}
	public enum VOICE_LIST {
		NONE,
		DIALOG_1,
		DIALOG_2,
		DIALOG_3,
		DIALOG_4,
		DIALOG_5,
		DIALOG_6,
		DIALOG_7,
		DIALOG_8,
		DIALOG_9,
		DIALOG_10,
		DIALOG_11,
		DIALOG_12,
		DIALOG_13,
		DIALOG_14,
		DIALOG_15,
		END
	}

	protected static SoundManager instance;

	public static SoundManager Instance {
		get {
			if(instance == null) {
				
				instance = (SoundManager) FindObjectOfType(typeof(SoundManager));

				if (instance == null) {
					Debug.LogError("SoundManager Instance Error");
				}
			}
			return instance;
		}
	}

	public const int maxSeSources = 5;
	public const int maxVoiceSources = 16;
	// === AudioSource ===
	// BGM
	private AudioSource BGMsource;
	// SE
	private AudioSource SEsources;
	// 音声
	private AudioSource VoiceSources;
	// === AudioClip ===
	// BGM
	public AudioClip[] BGM;
	// SE
	public AudioClip[] SE;
	// 音声
	public AudioClip[] Voice;



	void Awake (){
		DontDestroyOnLoad (transform.gameObject);
		GameObject[] obj = GameObject.FindGameObjectsWithTag("SoundManager");
		if( obj.Length > 1 ){
			// 既に存在しているなら削除
			Destroy(gameObject);
		}else{
			// 音管理はシーン遷移では破棄させない
			DontDestroyOnLoad(gameObject);
		}

		// 全てのAudioSourceコンポーネントを追加する

		// BGM AudioSource
		BGMsource = gameObject.AddComponent<AudioSource>();
		// BGMはループを有効にする
		BGMsource.loop = true;

		// SE AudioSource
		SEsources = gameObject.AddComponent<AudioSource>();

		// 音声 AudioSource
		VoiceSources = gameObject.AddComponent<AudioSource> ();
	}
		
	// ***** BGM再生 *****
	// BGM再生
	public void PlayBGM(int index){
		if( 0 > index || BGM.Length <= index ){
			return;
		}
		// 同じBGMの場合は何もしない
		if( BGMsource.clip == BGM[index] ){
			return;
		}
		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.Play();
	}

	// BGM停止
	public void StopBGM(){
		BGMsource.Stop();
		BGMsource.clip = null;
	}

	// ***** SE再生 *****
	// SE再生
	public void PlaySE( int index ) {
		if( 0 > index || SE.Length <= index ){
			return;
		}
		// 同じBGMの場合は何もしない
		if( SEsources.clip == SE[index] ){
			return;
		}
		SEsources.clip = SE[index];
		SEsources.Play();
	}

	// SE停止
	public void StopSE(){
		SEsources.Stop ();
		SEsources.clip = null;
	}

	// ***** Voice再生 *****
	// Vocie再生
	public void PlayVoice( int index ) {
		if( 0 > index || Voice.Length <= index ){
			return;
		}
		// 同じBGMの場合は何もしない
		if( VoiceSources.clip == Voice[index] ){
			return;
		}
		VoiceSources.clip = Voice[index];
		VoiceSources.Play();
	}



}

