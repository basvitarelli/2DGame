using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
	//gives my Player the ability to interact with other game elements using physics
	private Rigidbody2D myRigidbody;

	private Animator myAnimator; 
	//this is set to public so I can interact with it in the inspector.  It will give the Player variable speed.
	public float movementSpeed; 
	//can be set to true or false to change the Players facing direction
	private bool facingRight; 

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;


	private bool isGrounded;
	private bool jump;


	[SerializeField]
	private float jumpForce;

	public bool imAlive; 

	public GameObject reset; 

	private Slider healthBar;

    public float health = 10f;

    private float healthBurn = 2f;

	public GameObject healthpickup ;


// Use this for initialization
	void Start () 
	{	//initial value to set the Player facing right
		facingRight = true; 
		//associates the Rigidbody component of the Player with a variable we can use later on
		myRigidbody = GetComponent<Rigidbody2D> ();
		//associates the variable myAnimator to the Animator controller atached to the player
		myAnimator = GetComponent<Animator>();

		imAlive = true;

		reset.gameObject.SetActive (false);

		healthBar = GameObject.Find ("health slider").GetComponent<Slider>(); 

        healthBar.minValue = 0f;

        healthBar.maxValue = health;

        healthBar.value = healthBar.maxValue;

		healthpickup.GetComponent<Renderer>().enabled = false;
	}
	

	void Update()
	{
	
	HandleInput();

	}
	
	/* Update is called once per frame.  
Fixed Update locks in speed and performance regardless of console performance and quality*/
	void FixedUpdate () 
	{
		//access the keyboard controls and move left and right
		float horizontal = Input.GetAxis ("Horizontal");   
		//just to see what is being reported by the keyboard on the console
		//Debug.Log (horizontal);
		isGrounded = IsGrounded();
//calling the function in the game 
//controls Player on the x and y axis
if(imAlive){
HandleMovement (horizontal); 
//controls player facing direction
Flip(horizontal);
		}
	}

	private void HandleMovement(float horizontal)
	{
		if(isGrounded && jump){
			isGrounded = false;
			jump = false;
			myRigidbody.AddForce (new Vector2 (0, jumpForce));
		}


			//moves the Player on x axis and y axis
myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);		

myAnimator.SetFloat("speed",Mathf.Abs(horizontal));
	}
	private void Flip(float horizontal)
	{
		//logical test to make sure that we are changing his facing direction
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
			facingRight = !facingRight; //this sets the value of facingRight to its opposite
			Vector3 theScale = transform.localScale; //this accesses the local player scale component
			theScale.x *= -1;  //multiplying the x value of scale by -1 allows us to get either 1 or -1 as the result
			transform.localScale = theScale; //this reports the new value to the player's scale component
		}
	}

	
	private void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.Space))//if the user presses space bar...
		{
			jump = true;
			
			//set jumping bool in the Animator to TRUE
		myAnimator.SetBool("jumping",true);
		}
		
	}

private bool IsGrounded()
	{
		if (myRigidbody.velocity.y <= 0) { 
//if player is not moving vertically test each of Player’s groundpoints for contact with the Ground
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if(colliders[i].gameObject != gameObject)
//if any of the groundpoints is in contact(collides) with anything other than the Player, return true
						{
							return true;
						}
					}
				}
			}
		return false;
		}

 //this function will be called when player collides with an enemy
    public void  UpdateHealth(){
        if (health > 1) {
            //if the health bar has life left..
            health -= healthBurn; //current value of health minus 2f
            healthBar.value = health;  //update the interface slider
        } else {
            ImDead(); //if no life left, run this function which kills
        }

    }


		public void ImDead(){
		imAlive = false; 
		myAnimator.SetBool("dead",true);
		reset.gameObject.SetActive (true);
	}

	public void HealthPickup(){
		if (health > 1){
		health += healthBurn;
		healthBar.value = health;
		}
	}
		


	private void OnCollisionEnter2D(Collision2D target)
	{
		if(target.gameObject.tag == "Ground")
		{
	//set jumping bool in the Animator to FALSE
		myAnimator.SetBool("jumping",false);
		}
	
		if(target.gameObject.tag == "deadly"){
		ImDead();
		}

		if(target.gameObject.tag == "damage"){
		UpdateHealth();}

		if(target.gameObject.tag == "damage"){
		healthpickup.GetComponent<Renderer>().enabled = true;
		}

		if(target.gameObject.tag == "plushealth"){
		HealthPickup();
		}
	}

	


	}