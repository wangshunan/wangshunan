using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator animator;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();	
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {           
	}

    public void BlockDestroyAni() {
        animator.SetTrigger( "Destroy" );
    }
	void BlokDestroy() {
		Destroy (gameObject);
	}

	void BlockPhysics() {
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
