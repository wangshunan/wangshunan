using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchCro : MonoBehaviour {

	private Vector3 startPos;
	private Vector3 movePos;

	//[SerializeField] Text debugRight;
	[SerializeField] Text debugLeft;
	private string text;
	private float screenWidth;
	private float screenHeight;

	private Touch[] touches;

	private Touch touchRight;
	private Touch touchLeft;

	Touch touch;


	enum TouchCheck {
		none,
		start,
		push,
		move,
		over
	};


	private int touchStatus;
	private int touchPointCheck;

	void Awake() {
		screenWidth = Screen.currentResolution.width;
		screenHeight = Screen.currentResolution.height;
		movePos = Vector3.zero;
	}
		
	
	// Update is called once per frame
	void Update () {
		//MouseCtrUpData ();
		TouchCtrUpData ();
		TouchPointCheck ();
		TouchPointUpDate ();

		if (GetMovePosY () > 0) {
			debugLeft.text = "Move!!!";
		} else {
			debugLeft.text = screenHeight + "!!!";
		}
	}

	void TouchPointCheck() {
		
		touches = Input.touches;

		for (int i = 0; i < touches.Length; i++) {
			if (touches [i].position.x > screenWidth / 2) {
				touchRight = touches [i];
			} else {
				touchLeft = touches [i];
			}
		}

		/*if ( touchRight.tapCount < 2 ) {
			debugRight.text = "Tap!!!";
		}

		if ( touchRight.tapCount >= 2 ) {
			debugRight.text = "doubleTap!!";
		}*/

	}

	void TouchCtrUpData() {
		
		if (Input.touchCount > 0) {			
			touch = Input.GetTouch(0);

			if(touch.phase == TouchPhase.Began) {
				// タッチ開始
				touchStatus = (int)TouchCheck.start;
				text = "Start";
			} else if (touch.phase == TouchPhase.Moved) {
				// タッチ移動
				touchStatus = (int)TouchCheck.move;
				text = "move_y: " + movePos.y;
			} else if (touch.phase == TouchPhase.Ended) {
				// タッチ終了
				touchStatus = (int)TouchCheck.over;
				text = "Over";
			}
		}

	}

	void TouchPointUpDate() {

		switch (touchStatus) {
			case (int)TouchCheck.start:
				startPos = touchRight.position;
				break;
			case (int)TouchCheck.move:
					Vector3 overPos = touchRight.position;
					movePos.y = overPos.y - startPos.y;
					//touchRight.position.y = overPos.y - startPos.y;
					break;
			case (int)TouchCheck.over:
				touchStatus = (int)TouchCheck.none;
				startPos = Vector3.zero;
				movePos = Vector3.zero;
					break;
			default :
				break;
		}

	}

	void MouseCtrUpData() {

		if (Input.GetMouseButtonDown (0) && touchStatus != (int)TouchCheck.push) {
			touchStatus = (int)TouchCheck.start;
			startPos = Input.mousePosition;
			Debug.Log ("Start!!!");
		}

		if (touchStatus == (int)TouchCheck.start || touchStatus == (int)TouchCheck.push) {
			Vector3 overPos = Input.mousePosition;
			movePos.y = overPos.y - startPos.y;
			if (movePos.y != 0) {
				touchStatus = (int)TouchCheck.push;
				Debug.Log (movePos);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			touchStatus = (int)TouchCheck.over;
			movePos.y = 0;
			Debug.Log ("OVER!!!");

		}

		if (touchStatus == (int)TouchCheck.over) {
			touchStatus = (int)TouchCheck.none;
		}

	}

	public int GetMovePosX( ) {
		
		if (movePos.x != 1 / screenWidth) {	
			return movePos.x > 0 ? 1 : -1;
		}

		return 0;
	}

	public int GetMovePosY( ) {

		if ( screenHeight / movePos.y  > 5 ) {	
			return 1;
		} else {
			return 0;
		}
	
	}
}
