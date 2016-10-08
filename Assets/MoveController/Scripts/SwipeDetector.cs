using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SwipeDetector : MonoBehaviour
{

    public float minSwipeDistY;

    public float minSwipeDistX;

	public float tapDis;

    private Vector2 startPos;

    public Text text;

    void Start()
    {
        
    }
    void Update()
	{
		if (Input.touchCount > 0) 
			
		{
			
			Touch touch = Input.touches[0];
			
			switch (touch.phase) 
				
			{
				
			case TouchPhase.Began:
                    
                startPos = touch.position;

				break;

			case TouchPhase.Ended:

				float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;

				if (swipeDistVertical > minSwipeDistY && (touch.position.x > (Screen.width / 2))) {
						
					float swipeValue = Mathf.Sign (touch.position.y - startPos.y);

					if (swipeValue > 0) {//up swipe
						//Jump ();
						text.text = "Up Swipe";
					} else if (swipeValue < 0) {//down swipe
						//Shrink ();
						text.text = "Down Swipe";
					}
				}
					
				float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;

				if (swipeDistHorizontal > minSwipeDistX && (touch.position.x > (Screen.width / 2))) {
						
					float swipeValue = Mathf.Sign (touch.position.x - startPos.x);

					if (swipeValue > 0) {//right swipe
						//MoveRight ();
						text.text = "Right Swipe";
					} else if (swipeValue < 0) {//left swipe
						//MoveLeft ();
						text.text = "Left Swipe";
					}
				}

				if (swipeDistVertical <= tapDis && swipeDistHorizontal <= tapDis && (touch.position.x > (Screen.width / 2))) 
				{
					text.text = "tap";
				}
				break;
			}
		}
	}

    
}