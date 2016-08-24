using UnityEngine;
using System.Collections;

public class EnemyController1 : MonoBehaviour {

	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	Animator animator;
	[SerializeField]
	Rigidbody2D EnemyRd;
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	public GameObject target;

	private Vector2 EnemyMove = Vector2.one;

	float TargetX;
	float EnemyX;
	float DistanceCheck;
	float EnemySpeed = 1f;
	const float DISTANCE = 1.0f;



	void Start () {
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Cguy");
		EnemyRd = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		EnemyMove = transform.position;
		TargetX = target.transform.position.x;
		EnemyX = transform.position.x;
		DistanceCheck = TargetX - EnemyX;

		if (TargetX > EnemyX) {
			spriteRenderer.flipX = false;
		} else {
			spriteRenderer.flipX = true;

		}

		if (DistanceCheck > DISTANCE || DistanceCheck < -DISTANCE) {
			EnemyRd.MovePosition (Vector2.Lerp (EnemyMove, target.transform.position, Time.deltaTime));
			animator.SetBool ("Run", true);
		} else {
			animator.SetBool( "Run", false );
			animator.SetTrigger ("Attack");
		}

	}
}

