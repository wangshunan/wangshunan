using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyApperOfBoss : MonoBehaviour {

	[SerializeField]
	public GameObject enemy;

	[SerializeField]
	public GameObject capsules;

	[SerializeField]
	public GameObject hamburger;

	public GameObject apperPoint;
	public GameObject apperPoint2;

	public Slider bossHp;
	public Slider playerBlood;
	bool capsEnemy = true;
	bool hamEnemy = true;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( playerBlood.value >= 50 && capsEnemy == true ) {
			Instantiate( enemy, apperPoint.transform.position, Quaternion.identity );
			Instantiate( capsules, apperPoint.transform.position, Quaternion.identity );
			capsEnemy = false;
		}

		if ( bossHp.value <= 50 && hamEnemy == true ) {
			Instantiate( enemy, apperPoint2.transform.position, Quaternion.identity );
			Instantiate( hamburger, apperPoint2.transform.position, Quaternion.identity );
			hamEnemy = false;
		}

	}
}
