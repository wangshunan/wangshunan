using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	EnemyController enemyController;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    Animator animator;
    CharacterController controller;

    public float speed;
    public float jumpPower;
    public float gravity;
    public GameObject target;

    private float distanceCheck = 0.0f;
    private Vector3 moveDiretion = Vector3.zero;
    // base layerで使われる、アニメーターの現在の状態の参照
    private AnimatorStateInfo currentBaseState;


    // アニメーター各ステートへの参照
    static int attack = Animator.StringToHash ( "Base Layer.Attack" );
    static int run = Animator.StringToHash ( "Base Layer.Run" );
    static int jumpLanding = Animator.StringToHash( "Base Layer.Jump_Fall" );


	// Use this for initialization
	void Start () {

        controller = GetComponent<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        float axis = Input.GetAxisRaw ("Horizontal");
        float velY = moveDiretion.y;
        currentBaseState = animator.GetCurrentAnimatorStateInfo (0);


		//　キャラを移動させる
		if( currentBaseState.fullPathHash != attack ) {

            if( axis != 0.0f ) {
                moveDiretion.x = axis * speed;
                animator.SetBool( "Run", true );
            } else {
                moveDiretion.x = 0;
                animator.SetBool( "Run", false );
            }

		}

        // 接地状態でしかできない動き
        if( controller.isGrounded ) {
			if( Input.GetKeyDown(KeyCode.Z)  ) {
                distanceCheck = transform.position.x - enemyController.gameObject.transform.position.x;
                animator.SetTrigger( "Attack" );

                if( ( distanceCheck < 0 && spriteRenderer.flipX == false ) ||
                    ( distanceCheck > 0 && spriteRenderer.flipX == true ) ) {
                    if( enemyController.gameObject.CompareTag( "Enemy" ) ) {
                        enemyController.Damage();
                    }
                }
            }
		    if ( Input.GetButtonDown ("Jump")  ) {
			    moveDiretion.y = jumpPower;
                animator.SetTrigger( "Jump");      
	        }
            
        }
        animator.SetFloat("velY",velY);
		moveDiretion.y -= gravity* Time.deltaTime;
        Vector3 globaDirection = transform.TransformDirection( moveDiretion );
        controller.Move( globaDirection * Time.deltaTime );


		if( controller.isGrounded ) {
			moveDiretion.y = 0;
		}

 		if (axis != 0) {
			spriteRenderer.flipX = axis < 0;
        }

	}


}
