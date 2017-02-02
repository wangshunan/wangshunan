using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour {

    GameObject[] allObject;
    GameObject target;

    void Awake() {
        target = GameObject.Find( "Cguy" );
		allObject = GameObject.FindGameObjectsWithTag( "Map" );
    }

	// Use this for initialization
	void Start () {
        for ( int i = 0; i < allObject.Length; i++ ) {
            if ( allObject[ i ].transform.position.x - target.transform.position.x >= 25 ) {
                 allObject[ i ].SetActive( false );
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		 for ( int i = 0; i < allObject.Length; i++ ) {
            if ( allObject[ i ].transform.position.x - target.transform.position.x <= 25 ) {
                 allObject[ i ].SetActive( true );
            }

            if ( target.transform.position.x - allObject[ i ].transform.position.x >= 25 ) {
                allObject[ i ].SetActive( false );
            }
        }

	}
}
