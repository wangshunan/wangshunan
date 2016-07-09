using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	PlayerController playerController;

    public Transform player;    //プレイヤーを代入
    public float speed;
    Animator animator;
	const float DIRECTION_Z = 5.0f;
	const float ATTACK_DIRECTION_Z = 1.5f;

	// Use this for initialization
	void Start () {
	    player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        Vector3 playerPos = player.position;    //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向
        direction = direction.normalized;   //単位化（距離要素を取り除く）

		if (Mathf.Abs (transform.position.z - player.position.z) < DIRECTION_Z &&
		    Mathf.Abs (transform.position.z - player.position.z) > ATTACK_DIRECTION_Z) {
			transform.position += new Vector3 (0.0f, 0.0f, direction.z * speed * Time.deltaTime);
			transform.LookAt ( new Vector3( 0.0f, 0.0f, player.position.z ) );
			animator.SetBool ("walk", true);
		} else {
			animator.SetBool ("walk", false);
		}			
			
	}

	public void Damage() {
		animator.SetTrigger ("damage");

	}
				
}
