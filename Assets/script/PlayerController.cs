using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rig2d;
    LayerMask groundMask;

    public float speed;
    public float jumpPower;
    public float gravity;
    public int hp = 4;

	private SpriteRenderer spriteRenderer;
	private float distanceCheck = 0.0f;
	private const float hitDiretion = 1.0f;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;
    private float axis = 0.0f;
    
    [SerializeField] private float characterHeightOffset = 0.2f;

    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int run = Animator.StringToHash ( "Base Layer.Run" );
    static int jump = Animator.StringToHash( "Base Layer.Jump" );


	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator>();
        rig2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

        axis = Input.GetAxisRaw ("Horizontal");
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);

		    //　キャラを移動させる
			if( axis != 0 && currentBaseState.fullPathHash != attack ) {
                rig2d.transform.position += new Vector3( axis * speed * Time.deltaTime, 0 );  
                animator.SetBool( "Run", true );
			} else {
                animator.SetBool( "Run", false );
            }
				
			if( Input.GetKeyDown( KeyCode.Z ) && rig2d.velocity.y == 0 ) {
                //distanceCheck = transform.position.x - target.gameObject.transform.position.x;
                animator.SetTrigger( "Attack" );
                //animator.SetBool( "Run", false );
				/*if( ( distanceCheck < 0 && spriteRenderer.flipX == false ) ||
                    ( distanceCheck > 0 && spriteRenderer.flipX == true ) ) {

					if ((distanceCheck > 0 ? distanceCheck : -distanceCheck) < hitDiretion) {
						/*if (target.gameObject.CompareTag ("Enemy")) {
							Debug.Log ("Hit!!!");
						}
					}
                }*/
            }
	        if ( Input.GetButtonDown ("Jump") ) {
		        rig2d.velocity = new Vector2 ( rig2d.velocity.x, jumpPower );
                animator.SetBool( "Jump", true );
	        }

            if ( rig2d.velocity.y == 0 ) {
                animator.SetBool( "Jump", false );
            }

        var distanceFromGround = Physics2D.Raycast ( transform.position, Vector3.down, 1, groundMask ); 


		if ( axis != 0 && currentBaseState.fullPathHash != attack ) {
			spriteRenderer.flipX = axis < 0;
        }
        Debug.Log( "Y = " + rig2d.velocity.y );

    }
}
