using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	PlayerController playerController;
	[SerializeField]
	Rigidbody2D EnemyRd;

    Animator animator;
	private SpriteRenderer spriteRenderer;

	public GameObject target;

	private Vector2 EnemyMove = Vector2.one;

	float TargetX;
	float EnemyX;
	float DistanceCheck;
	float EnemySpeed = 1f;
	const float DISTANCE = 10.0f;



	void Start () {
		animator = GetComponent<Animator> ();
		target = GameObject.Find ("Unitychan");
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
			animator.SetTrigger ("HolgerAttack");
		}

	}
}

