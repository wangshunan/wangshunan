using UnityEngine;
using System.Collections;

public class LogoController : MonoBehaviour {

    [SerializeField]
    public GameObject title;
    [SerializeField]
    public GameObject backGround;

    private const float scalePos = 0.01f;
    private const float maxScalePos = 1.0f; 
    private const float speed = 0.8f;
    private Vector3 maxScale;
    private Transform targetTrans;
    private bool isScale = true;

	// Use this for initialization
	void Awake( ) {
	    transform.localScale = new Vector3( scalePos, scalePos, maxScalePos );
        targetTrans = GameObject.Find( "BackGround" ).transform;
        maxScale = new Vector3( maxScalePos, maxScalePos, maxScalePos );
	}
	
	// Update is called once per frame
	void Update( ) {
        
	    if ( isScale ) {
            transform.localScale = Vector3.MoveTowards( transform.localScale, targetTrans.localScale, speed * Time.deltaTime );
        } else {
            isScale = false;
        }
        if ( transform.localScale == maxScale ) {
            
            title.SetActive( true );
            
        }
	}
}
