using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	private AudioSource BgmMenuOne;
	private AudioSource BgmMenuTwo;
	private AudioSource BgmZako;
	private AudioSource SeButton;
	private AudioSource SeBack;
	private AudioSource SeGameStart;
	private AudioSource SePunch;
	private AudioSource SeJump;
	private AudioSource SeBlock;

	void Start() {
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		BgmMenuOne = audioSources [0];
		SeButton = audioSources [1];
		SeBack = audioSources [2];
		SeGameStart = audioSources [3];
		BgmMenuTwo = audioSources [4];
		BgmZako = audioSources [5];
		SePunch = audioSources [6];
		SeJump = audioSources [7];
		SeBlock = audioSources [8];
	}

	public void PlayBgmMenuOne() {
		if (BgmMenuOne != null) {
			BgmMenuOne.PlayOneShot (BgmMenuOne.clip);
		}
	}
	public void PlayBgmMenutwo() {
		BgmMenuTwo.PlayOneShot (BgmMenuTwo.clip);
	}
	public void PlayBgmMenuZako() {
		BgmZako.PlayOneShot (BgmZako.clip);
	}
	public void PlaySeButton() {
		SeButton.PlayOneShot (SeButton.clip);
	}
	public void StopSeButton() {
		SeButton.Stop ();
	}
	public void PlaySeBack() {
		SeBack.PlayOneShot (SeBack.clip);
	}
	public void PlaySeGameStart() {
		SeGameStart.PlayOneShot (SeGameStart.clip);
	}
	public void PlaySePunch() {
		SePunch.PlayOneShot (SePunch.clip);
	}
	public void PlaySeJump() {
		SeJump.PlayOneShot (SeJump.clip);
	}
	public void PlaySeBlock() {
		SeBlock.PlayOneShot (SeBlock.clip);
	}
}
