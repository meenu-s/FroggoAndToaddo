using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_SwimDownForce = 400f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_waterLevel = 0.5f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_waterDrag = 3.0f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWater;							// A mask determining what is water to the character
	[SerializeField] private LayerMask m_WhatIsWall;							// A mask determining what is wall to the character
	[SerializeField] private LayerMask m_WhatIsDeathTrigger;					// A mask determining what will cause death to character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Transform m_LeftCheck;								// A position marking where to check left walls
	[SerializeField] private Transform m_RightCheck;							// A position marking where to check right walls
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private bool m_inWater;
	private bool m_touchedDeathTrigger = false;
	private bool m_justDied = false;
	const float k_DeathTriggerRadius = 0.2f; // Radius of the overlap circle to determine if touched death trigger
	const float k_ClimbingRadius = 0.2f; // Radius of the overlap circle to determine if grounded
	private bool m_onWall;
	const float k_SwimmingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

    // Start is called before the first frame update
    void Awake()
    {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
        
    }


    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		if (m_LeftCheck!=null && m_RightCheck!=null)
		{
			m_onWall = false;
			Collider2D[] leftColliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_ClimbingRadius, m_WhatIsWall);
			for (int i = 0; i < leftColliders.Length; i++)
			{
				if (leftColliders[i].gameObject != gameObject)
				{
					m_onWall = true;
					print("On wall Left");
				}
			}
			Collider2D[] rightColliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_ClimbingRadius, m_WhatIsWall);
			for (int i = 0; i < rightColliders.Length; i++)
			{
				if (rightColliders[i].gameObject != gameObject)
				{
					m_onWall = true;
					print("On wall Right");
				}
			}
		}

		// Count water as grounded as well
		if (Physics2D.OverlapCircle(m_GroundCheck.position+new Vector3(0, m_waterLevel), k_SwimmingRadius, m_WhatIsWater))
		{
			if (!wasGrounded)
				OnLandEvent.Invoke();
			if (!m_inWater) 
			{
				// if m was not in water and now is in water 
				m_inWater = true; 
				gameObject.GetComponent<Rigidbody2D>().gravityScale = -.5f;
				gameObject.GetComponent<Rigidbody2D>().drag = m_waterDrag;
			}
		} else if (m_inWater) // if m was in water and now is not in water 
		{
			m_inWater = false; 
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;	
				gameObject.GetComponent<Rigidbody2D>().drag = 0f;
		}

		// Check if you are touching an obstacle that causes death
		// if (Physics2D.OverlapCircle(m_GroundCheck.position+new Vector3(0, m_waterLevel), k_DeathTriggerRadius, m_WhatIsDeathTrigger))

		if (!m_touchedDeathTrigger) 
		{
			if (m_LeftCheck!=null && m_RightCheck!=null)
			{
				Collider2D[] leftColliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_DeathTriggerRadius, m_WhatIsDeathTrigger);
				for (int i = 0; i < leftColliders.Length; i++)
				{
					if (leftColliders[i].gameObject != gameObject)
					{
						m_touchedDeathTrigger = true;
						print("Died");
					}
				}
				Collider2D[] rightColliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_DeathTriggerRadius, m_WhatIsDeathTrigger);
				for (int i = 0; i < rightColliders.Length; i++)
				{
					if (rightColliders[i].gameObject != gameObject)
					{
						m_touchedDeathTrigger = true;
						print("Died");
					}
				}
			}
			Collider2D[] topColliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_DeathTriggerRadius, m_WhatIsDeathTrigger);
			for (int i = 0; i < topColliders.Length; i++)
			{
				if (topColliders[i].gameObject != gameObject)
				{
					m_touchedDeathTrigger = true;
					print("Died");
				}
			}
			Collider2D[] bottomColliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_DeathTriggerRadius, m_WhatIsDeathTrigger);
			for (int i = 0; i < bottomColliders.Length; i++)
			{
				if (bottomColliders[i].gameObject != gameObject)
				{
					m_touchedDeathTrigger = true;
					print("Died");
				}
			}
			if (m_touchedDeathTrigger) 
			{
				m_justDied = true;
			}
		}
	}

	public bool isInWater() {
		return m_inWater;
	}

	public bool justDied() {
		if (m_justDied) {
			m_justDied = false;
			return true;
		}
		return false;
	}

	// public void Move(float move, bool crouch, bool jump, bool swimDown = false, bool climb = false)
	public void Move(float move, bool crouch, bool jump, bool swimDown = false)
	{
		// If character is trying to swim down, check to make sure character is in water and 
		if (swimDown) 
		{
			// Check if the character is in water
			if (m_inWater)
			{
				// Check the character is not grounded on ground
				// if (!Physics2D.OverlapCircle(m_GroundCheck.position, k_SwimmingRadius, m_WhatIsGround))
				// {
					Debug.Log("Successfully swam down in water");
					m_Rigidbody2D.AddForce(new Vector2(0f, -m_SwimDownForce));
				// }
				// animation 
			}
		}
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// Should we let the Player to jump
		if (jump)
		{
			// if (m_LeftCheck!=null && m_RightCheck!=null)
			// {
			// 	m_onWall = false;
			// 	Collider2D[] leftColliders = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_ClimbingRadius, m_WhatIsWall);
			// 	Collider2D[] rightColliders = Physics2D.OverlapCircleAll(m_RightCheck.position, k_ClimbingRadius, m_WhatIsWall);
			// 	for (int i = 0; i < leftColliders.Length; i++)
			// 	{
			// 		if (leftColliders[i].gameObject != gameObject || rightColliders[i].gameObject != gameObject)
			// 		{
			// 			m_onWall = true;
			// 			print("On wall");
			// 		}
			// 	}
			// }
			Debug.Log("Tried to jump");
			if (m_Grounded) 
			{
				Debug.Log("Jumped on ground");
				// Yes if on ground
				// Add jump force to the player.
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			} 
			else if (m_inWater) 
			{
				Debug.Log("Jumped in water");
				// Yes if in water
				// Add 1/2 jump force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}	
			else if (m_onWall) 
			{
				Debug.Log("Jumped on wall");
				// Yes if on wall
				// Add 1/3 jump force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce*2.0f));
			}
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		// Vector3 theScale = transform.localScale;
		// theScale.x *= -1;
		// transform.localScale = theScale;

		// or better 
		transform.Rotate(0f, 180f, 0f);
	}

}
