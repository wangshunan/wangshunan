using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

    [SerializeField]
    PauseSystem pause;

    private GameObject startPos;
	private GameObject overPos;
    private GameObject underGroundPos;

    private GameObject m_target;

    private float offSetPoint;
    private float startPoint;
    private float fallPoint;

    bool fallEvent = false;
    bool upEvent = false;
    bool isUnderGround = false;

    void Awake() {
        pause = GameObject.Find( "GameLogic" ).GetComponent<PauseSystem>();
        startPos = GameObject.Find( "StartPos" );
		overPos = GameObject.Find ("OverPos");
        underGroundPos = GameObject.Find( "UnderGroundPos" );
        m_target = GameObject.Find( "Cguy" );
        transform.position = startPos.transform.position;
		offSetPoint = transform.position.x - m_target.transform.position.x;
        fallPoint = ( startPos.transform.position.y - underGroundPos.transform.position.y ) / 2;
    }

    void Update() {
        startPoint = m_target.transform.position.x;
    }
    
    void LateUpdate() {
        HorizontalController();
        VerticalController();
        FallEvent();
        UpEvnet();
    }

    void HorizontalController() {
		if ( m_target.transform.position.x > transform.position.x ) {
			float overPoint = m_target.transform.position.x;
			float moveDistance = overPoint - startPoint;
			if( transform.position.x - moveDistance >= overPos.transform.position.x ) {
				return;
			}
				
            transform.position = new Vector3( m_target.transform.position.x, transform.position.y, transform.position.z );
        }

        if ( m_target.transform.position.x < transform.position.x ) {
			float overPoint = m_target.transform.position.x;
            float moveDistance = Mathf.Abs( overPoint - startPoint );
           
            if ( transform.position.x - moveDistance <= startPos.transform.position.x ) {
                return;
            }

            transform.position = new Vector3( m_target.transform.position.x, transform.position.y, transform.position.z );
        }
    }

    void VerticalController() {
        if ( m_target.transform.position.y > startPos.transform.position.y ) {
            transform.position = new Vector3( transform.position.x, m_target.transform.position.y, transform.position.z );
        }

        if ( m_target.transform.position.y < startPos.transform.position.y - fallPoint && isUnderGround == false ) {
            fallEvent = true;
        }

        if ( m_target.transform.position.y > underGroundPos.transform.position.y + fallPoint && isUnderGround == true ) {
            upEvent = true;
        }
    }
    
    void FallEvent() {
        if( fallEvent ) {
            pause.GamePause();
            transform.position -= new Vector3( 0, 0.5f, 0 );

            if ( transform.position.y < underGroundPos.transform.position.y ) {
                transform.position = new Vector3( transform.position.x, underGroundPos.transform.position.y, transform.position.z );
                pause.GameResume();
                fallEvent = false;
                isUnderGround = true;
            }
        }
    }

    void UpEvnet() {
        if( upEvent ) {
            pause.GamePause();
            transform.position += new Vector3( 0, 0.5f, 0 );

            if ( transform.position.y > startPos.transform.position.y ) {
                transform.position = new Vector3( transform.position.x, startPos.transform.position.y, transform.position.z );
                pause.GameResume();
                upEvent = false;
                isUnderGround = false;
            }
        }
    }
}
