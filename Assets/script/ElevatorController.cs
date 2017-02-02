using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    [SerializeField]
    GameObject elevator;

    private float startPos;
    private float overPos = -5;
    private float speed = 2f;

    public bool elevatorSwitch;

    void Awake () {
        elevator = GameObject.Find( "Elevator" );
        startPos = elevator.transform.position.y;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if( elevatorSwitch ) {

            if ( elevator.transform.position.y >= overPos ) {
                speed *= -1;
            } else if ( elevator.transform.position.y <= startPos ) {
                speed = Mathf.Abs( speed );
            }

		    elevator.transform.position += new Vector3( 0, speed * Time.deltaTime, 0 );
        }
	}
}
