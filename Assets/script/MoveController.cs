using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class MoveController : MonoBehaviour {

    public MovieTexture movieTexture;
    private AudioSource audio;

    void Awake() {
        gameObject.GetComponent<RawImage>().texture = movieTexture as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movieTexture.audioClip;
    }

   
    public void movePlay() {
        audio.clip = movieTexture.audioClip; 
        movieTexture.Play();
        audio.Play();
    }

    void Update() {

        if ( Input.GetMouseButtonDown(0) ) {
            movieTexture.Stop();
            audio.Stop();
        }

        if ( !movieTexture.isPlaying ) {
            SceneManager.LoadScene( "TitleMenu" );
        }

    }
}
