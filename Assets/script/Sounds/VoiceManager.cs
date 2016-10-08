using UnityEngine;
using System.Collections;

public class VoiceManager : MonoBehaviour {
	private AudioSource Vod0010;
	private AudioSource Guyo0020;
	private AudioSource Vod0030;
	private AudioSource Guyo0040;
	private AudioSource Guyo0050;
	private AudioSource Vod0060;
	private AudioSource Guyo0070;
	private AudioSource Guyo0080;
	private AudioSource Vod0090;
	private AudioSource Vod0100;
	private AudioSource Guyo0110;
	private AudioSource Vod0120;
	private AudioSource Guyo0130;
	private AudioSource Guyo0140;
	private AudioSource Guyo0150;

	void Start() {
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		Vod0010 = audioSources [0];
		Guyo0020 = audioSources [1];
		Vod0030 = audioSources [2];
		Guyo0040 = audioSources [3];
		Guyo0050 = audioSources [4];
		Vod0060 = audioSources [5];
		Guyo0070 = audioSources [6];
		Guyo0080 = audioSources [7];
		Vod0090 = audioSources [8];
		Vod0100 = audioSources [9];
		Guyo0110 = audioSources [10];
		Vod0120 = audioSources [11];
		Guyo0130 = audioSources [12];
		Guyo0140 = audioSources [13];
		Guyo0150 = audioSources [14];
	}
	public void PlayVoice( int count ) {
		for (int i = count; i <= 15; i++) { 
			if (count == 1) {
				Vod0010.PlayOneShot (Vod0010.clip);
			}
			if (count == 2) {
				Vod0010.Stop ();
				Guyo0020.PlayOneShot (Guyo0020.clip);
			}
			if (count == 3) {
				Guyo0020.Stop ();
				Vod0030.PlayOneShot (Vod0030.clip);
			}
			if (count == 4) {
				Vod0030.Stop ();
				Guyo0040.PlayOneShot (Guyo0040.clip);
			}
			if (count == 5) {
				Guyo0040.Stop ();
				Guyo0050.PlayOneShot (Guyo0050.clip);
			}
			if (count == 6) {
				Guyo0050.Stop ();
				Vod0060.PlayOneShot (Vod0060.clip);
			}
			if (count == 7) {
				Vod0060.Stop ();
				Guyo0070.PlayOneShot (Guyo0070.clip);
			}
			if (count == 8) {
				Guyo0070.Stop ();
				Guyo0080.PlayOneShot (Guyo0080.clip);
			}
			if (count == 9) {
				Guyo0080.Stop ();
				Vod0090.PlayOneShot (Vod0090.clip);
			}
			if (count == 10) {
				Vod0090.Stop ();
				Vod0100.PlayOneShot (Vod0100.clip);
			}
			if (count == 11) {
				Vod0100.Stop ();
				Guyo0110.PlayOneShot (Guyo0110.clip);
			}
			if (count == 12) {
				Guyo0110.Stop ();
				Vod0120.PlayOneShot (Vod0120.clip);
			}
			if (count == 13) {
				Vod0120.Stop ();
				Guyo0130.PlayOneShot (Guyo0130.clip);
			}
			if (count == 14) {
				Guyo0130.Stop ();
				Guyo0140.PlayOneShot (Guyo0140.clip);
			}
			if (count == 15) {
				Guyo0140.Stop ();
				Guyo0150.PlayOneShot (Guyo0150.clip);
			}
		}
	}
	public void StopVoice() {
		Vod0010.Stop ();
		Guyo0020.Stop ();
		Vod0030.Stop ();
		Guyo0040.Stop ();
		Guyo0050.Stop ();
		Vod0060.Stop ();
		Guyo0070.Stop ();
		Guyo0080.Stop ();
		Vod0090.Stop ();
		Vod0100.Stop ();
		Guyo0110.Stop ();
		Vod0120.Stop ();
		Guyo0130.Stop ();
		Guyo0140.Stop ();
		Guyo0150.Stop ();
	}

}
