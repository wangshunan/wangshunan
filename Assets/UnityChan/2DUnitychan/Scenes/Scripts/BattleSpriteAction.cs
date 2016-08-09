using UnityEngine;
using System.Collections;

public class BattleSpriteAction : MonoBehaviour
{
	static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashIsCrouch = Animator.StringToHash ("IsCrouch");
	static int hashAttack1 = Animator.StringToHash ("Attack1");
	static int hashAttack2 = Animator.StringToHash ("Attack2");
	static int hashAttack3 = Animator.StringToHash ("Attack3");


	static int hashDamage = Animator.StringToHash ("Damage");
	static int hashIsDead = Animator.StringToHash ("IsDead");

    private bool grounded = false;
    private float distanceCheck = 0.0f;

    public float jumpPower = 3.0f;

    AnimatorStateInfo stateInfo;

	[SerializeField]
    private float characterHeightOffset = 0.2f;

	[SerializeField]
    LayerMask groundMask;

    [SerializeField]
    EnemyController enemyController;

	[SerializeField, HideInInspector] Animator animator;
    [SerializeField, HideInInspector] Animation animation;
	[SerializeField, HideInInspector]SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector]Rigidbody2D rig2d;

	public int hp = 4;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
        animation = GetComponent<Animation>();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rig2d = GetComponent<Rigidbody2D> ();
        GetComponent<BoxCollider2D>().enabled = false;
	}

	void Update ()
	{

        stateInfo = animator.GetCurrentAnimatorStateInfo( 0 );
		float axis = Input.GetAxisRaw ("Horizontal");
		bool isDown = Input.GetAxisRaw ("Vertical") < 0;

		var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 1, groundMask);

		// update animator parameters
		animator.SetBool (hashIsCrouch, isDown);
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (axis));

		if( Input.GetKeyDown(KeyCode.Z)  ) {
            distanceCheck = transform.position.x - enemyController.gameObject.transform.position.x;
            animator.Play( "Attack1" );
            if( ( distanceCheck > -0.4f &&  distanceCheck < 0 && spriteRenderer.flipX == false ) ||
                ( distanceCheck < 0.4f && distanceCheck > 0 && spriteRenderer.flipX == true ) ) {
                if( enemyController.gameObject.CompareTag( "Enemy" ) ) {
                    enemyController.Damage();
                }
            }
        }

		if ( Input.GetButtonDown ("Jump")  ) {
			rig2d.velocity = new Vector2 (rig2d.velocity.x, jumpPower);
	    }

        if( axis > 0  ) {
            rig2d.velocity = new Vector2 ( 1, rig2d.velocity.y );
        }
        if( axis < 0 ) {
            rig2d.velocity = new Vector2 ( -1, rig2d.velocity.y );
        }

		// flip sprite
		if (axis != 0) {
			spriteRenderer.flipX = axis < 0;
        }
        
	}
}
