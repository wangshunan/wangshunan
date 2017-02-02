using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterColorChange : MonoBehaviour {
    [SerializeField]
    private Image guy;

    [SerializeField]
    private Image vodke;

    [SerializeField]
    private Color changeColor;

    [SerializeField]
    private Color originalColor;

    public TextController textController;
    void Start( ) {
        changeColor = new Color( 0.5f, 0.5f, 0.5f, 1 );
        originalColor = new Color( 1, 1, 1, 1 );

        guy = GameObject.Find( "Guy" ).GetComponent<Image>( );
        vodke = GameObject.Find( "Vodke" ).GetComponent<Image>( );

        
    }
    void Update( ) {
        if( textController.screenCount < 5 ) {
            if( textController.screenCount % 2 == 1 ) {
                guy.color = changeColor;
                vodke.color = originalColor;
            } else {
               vodke.color = changeColor;
                guy.color = originalColor;
            }
        }
        if( textController.screenCount > 5 ) {
               if( textController.screenCount % 2 == 0 ) {
                guy.color = changeColor;
                vodke.color = originalColor;
            } else {
               vodke.color = changeColor;
                guy.color = originalColor;
            }
        }
        if( textController.screenCount == 8 ) {
             vodke.color = changeColor;
             guy.color = originalColor;
        }
        if( textController.screenCount == 9 || textController.screenCount == 10 ) {
             guy.color = changeColor;
             vodke.color = originalColor;
        }
        if( textController.screenCount > 10  ) {
            guy.color = originalColor;
            vodke.color = changeColor;
        }
        if( textController.screenCount == 17 || textController.screenCount == 18 ||
            textController.screenCount == 21 || textController.screenCount == 22 ||
            textController.screenCount == 24 ) {
             guy.color = changeColor;
             vodke.color = originalColor;
        }
         if( textController.screenCount == 19 || textController.screenCount == 20 ||
             textController.screenCount == 25 ) {
            guy.color = originalColor;
            vodke.color = changeColor;
        }
        Debug.Log( textController.screenCount );
    }
}
