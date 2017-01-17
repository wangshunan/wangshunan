using UnityEngine;
using System.Collections;

public class ImageMoving : MonoBehaviour {
    public static ImageMoving Instance {
        set;
        get;
    }
    public Transform cguyTrans;
    public Transform vodkeTrans;
	public Transform targetTrans;
    private float speed = 150f;

	private bool isMoveToLeft = false;

	public TextController textController;
	void Start () {
		


	}
	// Update is called once per frame
	void Update () {

		if ( textController.screenCount >= 11 && textController.screenCount < 15) {
			vodkeTrans.localScale = new Vector3( -1, 1, 1 );
			isMoveToLeft = true;
		}
	

		if ( isMoveToLeft ) {
			
			vodkeTrans.position = Vector3.MoveTowards( vodkeTrans.position, targetTrans.position, speed * Time.deltaTime );
		}

	}

   
}
