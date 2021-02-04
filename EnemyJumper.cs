using UnityEngine;
using System.Collections;

public class EnemyJumper : MonoBehaviour {

	
	public float forceY;			//controlling how high he jumps
	private Rigidbody2D myRigidbody;	//allows us to aply physics to jumper
	private Animator myAnimator;		//allows us to have control over animation states

	
	void Awake(){
	//associates the componets in Unity with the script
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {
		//calling function, which allows the function to be running when others are
		StartCoroutine (Attack ());
	}

	IEnumerator Attack(){
		yield return new WaitForSeconds (Random.Range (2, 4)); //controls the pause between jumps
		forceY = Random.Range (250, 550); //controls the random force it will jump at 
		myRigidbody.AddForce(new Vector2(0, forceY)); //applies the jump to the enemy
		myAnimator.SetBool ("attack", true); //allows the attack animation to play
		yield return new WaitForSeconds (1.5f); //length of time to play the attack animation 
		myAnimator.SetBool ("attack", false);	//back to idle state in the animator 
		StartCoroutine (Attack ());		//creates a loop
	}

	/*
	void OnTriggerEnter2D(Collider2D target){

		if (target.tag == "bullet") {
			Destroy (gameObject);
			Destroy (target.gameObject);
		}
	}
	*/
}