using System;
using UnityEngine;


namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private bool m_SustainedJump = false;                      // Whether or not a player can hold jump to add more force while jumping.
        [SerializeField] private bool m_LimitSustainedJumpSpeed = false;            // Whether or not to limit the amount of sustained jump velocity.
        [SerializeField] private bool m_AltMidAirSustainedJumpForce = false;        // Whether or not an alternative sustained jump force is applied to mid air jump.
        [SerializeField] private bool m_AltMidAirSustainedJumpDuration = false;     // Whether or not an alternative sustained jump duration is used while mid air jumping.
        [SerializeField] private bool m_MidAirJump = false;                         // Whether or not a player can jump while in the air.
        [SerializeField] private int m_MidAirJumpCount = 1;                         // Amount of mid air jumps the player can perform.
        [SerializeField] private bool m_AltMidAirJumpForce = false;                 // Whether or not an alternative mid air jump force is used while mid air jumping.
        private enum ApplyMoveForce {Instantaneous = 0, Additive = 1};
        [SerializeField, HideInInspector] private ApplyMoveForce m_ApplyMoveForce = ApplyMoveForce.Instantaneous;
        [SerializeField] private float m_MaxSpeed = 10f;                            // The fastest the player can travel in the x axis.
        [SerializeField] private float m_AirMaxSpeed = 10f;                         // The fastest the player can travel in the x axis while in the air.
        [SerializeField] private float m_MaxSustainedJumpSpeed = 10f;               // The fastest the player can travel in the y axis.
        [SerializeField] private float m_Accel = 30f;                               // Amount of force added to the player every fixed frame in the x axis.
        [SerializeField] private float m_AirAccel = 20f;                            // Amount of force added to the player while in the air every fixed frame in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
        [SerializeField] private float[] m_JumpForces = new float[1];               // Array of forces added sequentially when the player jumps while in the air.
        [SerializeField] private float m_JumpSustainedForce = 10f;                  // Amount of force added to the player every fixed frame in the y axis while a player holds jump.
        [SerializeField] private float[] m_JumpSustainedForces = new float[1];      // Array of forces added sequentially to the player every fixed frame in the y axis while the player is in the air and holds jump.
        [SerializeField] private float m_JumpSustainedDuration = 2f;                // Amount of time in seconds (deltaTime) the sustained jump force is added to the player every fixed frame.
        [SerializeField] private float[] m_JumpSustainedDurations = new float[1];   // Array of times in seconds used sequentially the sustained jump forces are added to the player every fixed frame.
        [Range(0, 3)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping.
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character.


        private Transform m_GroundCheck;                // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f;             // Radius of the overlap circle to determine if grounded.
        private bool m_Grounded;                        // Whether or not the player is grounded.
        private bool m_Sustained;                       // Whether or not the current jump is being sustained by the player.
        private float m_SustainedJumpTimer = 0f;        // For determining how long the player has been holding jump.
        private Transform m_CeilingCheck;               // A position marking where to check for ceilings.
        const float k_CeilingRadius = .01f;             // Radius of the overlap circle to determine if the player can stand up.
        private Animator m_Anim;                        // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;              // Reference to the player's rigidbody 2D component.
        private bool m_FacingRight = true;              // For determining which way the player is currently facing.
        private int m_JumpCount = 0;                    // For counting the number of jumps the player has performed before the player is grounded.
        private float m_defaultMass = 0f;               // Default Rigidbody 2D mass - which get changed when in the Slimed state.
        private float m_defaultDrag = 0f;               // Default Rigidbody 2D drag - which get changed when in the Slimed state.
        private float m_defaultAngularDrag = 0f;        // Default Rigidbody 2D angular drag - which get changed when in the Slimed state.
        private SpriteRenderer m_SpriteRenderer;        // Reference to the player's sprite renderer component.
        private AudioSource m_JumpAudioSource;          // Reference to the player's jump audio source.
        private AudioSource m_MidAirJumpAudioSource;    // Reference to the player's mid air jump audio source.
        private float m_HurtTimer = 0f;
        private float m_HurtTime = 0.5f;
        private bool m_Hurt = false;
        private bool m_Dead = false;
        private UnityStandardAssets.Cameras.AutoCam m_AutoCam;

        #region SlimeSetup
        [SerializeField] private bool m_SlimeTemporary = false;                   // Sets the slime effect to last for a set duration at full intensity
        [SerializeField] private float m_SlimeTempDuration = 5f;                   // Sets the duration of the slimed effect.
        [SerializeField] private Color m_SlimeTint = new Color(0f, 1f, 0f, 1f);    // Sets the colour of the slimed effect on the player.
        [Range(0, 3)] [SerializeField] private float m_SlimeSpeedScale = 0.5f;                 // Scaling factor on max speed
        [Range(0, 2)] [SerializeField] private float m_SlimeAccellScale = 0.9f;                // Scaling factor on acceleration
        [Range(0, 2)] [SerializeField] private float m_SlimeJumpScale = 0.7f;                  // Scaling factor on jump Y Force
        [Range(0, 3)] [SerializeField] private float m_SlimeJumpSusForceScale = 0.7f;          // Scaling factor on jump sustain force
        [Range(0, 5)] [SerializeField] private float m_SlimeJumpSusDurationScale = 0.7f;       // Scaling factor on jump sustain force duration
        [Range(0, 2)] [SerializeField] private float m_SlimeJump2Scale = 0.7f;                 // Scaling factor on jump Y Force
        [Range(0, 3)] [SerializeField] private float m_SlimeJump2SusForceScale = 0.7f;         // Scaling factor on jump sustain force
        [Range(0, 5)] [SerializeField] private float m_SlimeJump2SusDurationScale = 0.7f;      // Scaling factor on jump sustain force duration
        [Range(0, 3)] [SerializeField] private float m_SlimeMassScale = 1f;                    // Scaling factor on character mass
        [Range(0, 3)] [SerializeField] private float m_SlimeDragScale = 1f;                    // Scaling factor on character's linear drag
        [Range(0, 3)] [SerializeField] private float m_SlimeAngularDragScale = 1f;             // Scaling factor on character's angular drag
        // local Slime scale variables - these need to be set to default of 1 when not slimed, and set to the original values when slimed.
        // The reason for adding this second set of variables is so that there aren't heaps of conditional statements in the move code, 
        // just a permenent scale value in the code which is either 1 (default) or the modified (Slimed state) version.
        private float m_s_SScale = 1f;
        private float m_s_AScale = 1f;
        private float m_s_JScale = 1f;
        private float m_s_JSusFScale = 1f;
        private float m_s_JSDurScale = 1f;
        private float m_s_J2Scale = 1f;
        private float m_s_J2SusFScale = 1f;
        private float m_s_J2SDurScale = 1f;
        private bool m_Slimed;                          // For determining if the player is in a slimed state
        private float m_SlimeTempTimer = 0f;
        private bool m_SlimeOut = false;                // For determining if the player has left the slimepit
        #endregion

        #region DashSetup
        // Dash code setup
        [Range(0, 1)] [SerializeField] private float m_dashDuration = 0.3f;             // m_dashDuration is the time that the extra dash boost is in effect.
        [Range(0, 2)] [SerializeField] private float m_dashCoolDown = 1.0f;             // Dash cooldown timer
        [Range(1, 10)] [SerializeField] private float m_dashMaxSpeedMultiplier = 5f;    // This is the max dash multiplier value.
        [Range(0, 1)] [SerializeField] private float m_dashFade = 0.25f;                // Amount to fade the dash speed multiplier by each frame.
        [SerializeField] private bool m_canDash;                                        // setting this up to be able to turn dash on or off.

        // Internal logic variables - not to be made public
        private bool m_dashing = false;                 // Booleans to manage the dash timers.
        private bool m_dashed = false;
        private float m_curDashTime = 0f;               // m_curDashTime is a temp variable - a counter
        private float m_curDashSpeedMultiplier = 1f;    // When dash is in effect, the move speed in air or ground will be multiplied by this value. This is a temp variable because we may want to fade the value over time for a gradual deceleration.
        private AudioSource m_DashAudioSource;    // Reference to the player's mid air jump audio source.
        #endregion

        public void OnValidate()
        {
            if (m_MidAirJumpCount < 1)
            {
                Debug.LogWarning("Mid Air Jump Count must be greater than 0.");
                m_MidAirJumpCount = 1;

            }

            System.Array.Resize(ref m_JumpForces, m_MidAirJumpCount);
            System.Array.Resize(ref m_JumpSustainedForces, m_MidAirJumpCount);
            System.Array.Resize(ref m_JumpSustainedDurations, m_MidAirJumpCount);
        }


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_JumpAudioSource = transform.Find("JumpAudioSource").GetComponent<AudioSource>();
            m_MidAirJumpAudioSource = transform.Find("MidAirJumpAudioSource").GetComponent<AudioSource>();
            m_DashAudioSource = transform.Find("DashAudioSource").GetComponent<AudioSource>();
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_AutoCam = GameObject.Find("MultipurposeCameraRig").GetComponent<UnityStandardAssets.Cameras.AutoCam>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            DefaultRigidBodySettings();     // Setting the defaults for Rigidbody mass etc.
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            if (m_Sustained)
            {
                m_SustainedJumpTimer -= Time.deltaTime;

                if (m_SustainedJumpTimer < 0)
                {
                    m_SustainedJumpTimer = 0;
                }
            }

            if (m_Hurt)
            {
                m_HurtTimer -= Time.deltaTime;

                if (m_HurtTimer < 0)
                {
                    m_Hurt = false;
                }
            }

            // Slimed scripts
            if (m_Slimed && m_SlimeTemporary && m_SlimeOut) 
            {
                SlimeTemp();
            }

            //Debug.Log(m_SustainedJumpTimer);

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    m_Sustained = false;
                    m_JumpCount = 0;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            
        }
        private void DashReset()
        {
            // Reset the dash cooldown.
            m_dashed = false;
            //Debug.Log("DASH RESET");
        }
        public void Move(float move, bool crouch, bool jump, bool sustainJump, bool dash) // Note:this function was updated from the original Move function to add a fifth argument to recognise the DASH. This is called from PlatformerCharacter2DUserControl (Physics based example project).
        {
            // If player is in the hurt state, do not process input
            if (m_Hurt)
                return;

            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            #region Dash
            //**// DASH //**//
            //If the player has just started a dash, and we are not in the middle of a dash, and have not recently dashed, then we can dash again.
            if (m_canDash && dash && !m_dashing && !m_dashed)
                if (m_canDash && dash && !m_dashing && !m_dashed)
                {
                    // Debug.Log("STARTED A DASH");
                    m_dashing = true;
                    // @@@@@@ To add - hooks for dash animation and sound
                    m_Anim.SetBool("Dash", m_dashing);
                    PlayDashSound();
                    // Initialise the temp variables
                    m_curDashTime = 0f;
                    m_curDashSpeedMultiplier = m_dashMaxSpeedMultiplier;
                    // Start the dash cool down timer
                    Invoke("DashReset", m_dashCoolDown);
                }

            // If the player is dashing ... 
            if (m_dashing)
            {
                // Debug.Log("DASHING");
                // update the dash speed multiplier & count the time spent dashing
                m_curDashSpeedMultiplier = m_curDashSpeedMultiplier -= m_dashFade;
                //Debug.Log("curDASH_Mult" + m_curDashSpeedMultiplier);
                m_curDashTime += Time.deltaTime;

                if (m_curDashTime > m_dashDuration) // If we have dashed for the allowed time, stop dashing, and say we have dashed.
                {
                    // Debug.Log("DASH FORCE ENDED");
                    m_dashed = true;
                    m_dashing = false;
                    m_Anim.SetBool("Dash", m_dashing);

                    // Reset the speed multiplier so that it does not scale normal movement.
                    m_curDashSpeedMultiplier = 1f;
                }
            }

            //**// END DASH //*//
            #endregion

            // only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier, reduce speed further if slimed.
                move = (crouch ? move * m_CrouchSpeed * m_s_SScale : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                switch(m_ApplyMoveForce)
                {
                    case ApplyMoveForce.Instantaneous:
                    {

                        if (!m_Grounded)
                        {
                                // Move the character by Max Speed ... UPDATED to add dash multiplier
                                m_Rigidbody2D.velocity = new Vector2(move * m_AirMaxSpeed * m_s_SScale * m_curDashSpeedMultiplier, m_Rigidbody2D.velocity.y);
                            }
                        else
                        {
                                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * m_s_SScale * m_curDashSpeedMultiplier, m_Rigidbody2D.velocity.y);
                            }
                        break;
                    }
                    case ApplyMoveForce.Additive:
                    {
                        float accel = m_Accel * m_s_AScale;     // Reduce the acceleration if slimed.

                        // Use a different parameter for air acceleration.
                        if (!m_Grounded)
                        {
                            accel = m_AirAccel * m_s_AScale;        // Reduce the air accel if slimed.

                            // Move the character using addforce to produce an acceleration curve, but only if max speed has not been reached, reduce this if slimed.
                            if (m_Rigidbody2D.velocity.magnitude < (m_AirMaxSpeed * m_s_SScale))
                            {
                                m_Rigidbody2D.AddForce(new Vector2(move * accel, 0f));
                            }
                        }
                        else
                        {
                            // Move the character using addforce to produce an acceleration curve, but only if max speed has not been reached, reduce this if slimed.
                            if (m_Rigidbody2D.velocity.magnitude < (m_MaxSpeed * m_s_SScale))
                            {
                                m_Rigidbody2D.AddForce(new Vector2(move * accel, 0f));
                            }
                        }
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
                

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


            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * m_s_JScale));
                PlayJumpSound();

                m_Sustained = false;

                return;
            }
            else if (!m_Grounded && m_MidAirJump && jump)
            {
                if (m_JumpCount < m_MidAirJumpCount)
                {
                    // reduce double jump count (count up and allow this to read from an array...)
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f); // Reset velocity on jump...
                    // If the player does not have an alt mid air jump force...
                    if (!m_AltMidAirJumpForce)
                    {
                        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * m_s_J2Scale)); // ... add a vertical force to the player.
                        PlayMidAirJumpSound();
                    }
                    else
                    {
                        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForces[m_JumpCount] * m_s_J2Scale));
                        PlayMidAirJumpSound();
                    }

                    m_JumpCount++;

                    m_Sustained = false;
                }
                return;
            }

            if (!m_Grounded && m_SustainedJump && sustainJump)
            {
                
                if (!m_Sustained)
                {
                    if (!m_AltMidAirSustainedJumpDuration)
                    {
                        m_SustainedJumpTimer = m_JumpSustainedDuration * m_s_JSDurScale;
                    }
                    else if (m_AltMidAirSustainedJumpDuration && m_JumpCount == 0)
                    {
                        m_SustainedJumpTimer = m_JumpSustainedDuration * m_s_JSDurScale;
                    }
                    else
                    {
                        m_SustainedJumpTimer = m_JumpSustainedDurations[m_JumpCount - 1] * m_s_J2SDurScale;
                    }
                }

                m_Sustained = true;

                float jumpYForce = 0f;

                if (!m_AltMidAirSustainedJumpForce)
                {
                    jumpYForce = m_JumpSustainedForce * m_s_JSusFScale;
                }
                else if (m_AltMidAirSustainedJumpForce && m_JumpCount == 0)
                {
                    jumpYForce = m_JumpSustainedForce * m_s_JSusFScale;
                }
                else
                {
                    jumpYForce = m_JumpSustainedForces[m_JumpCount - 1] * m_s_J2SusFScale;
                }

                if (m_SustainedJumpTimer > 0)
                {
                    if (m_LimitSustainedJumpSpeed)
                    {
                        if (m_Rigidbody2D.velocity.y < (m_MaxSustainedJumpSpeed * m_s_SScale))
                        {
                            m_Rigidbody2D.AddForce(new Vector2(0f, jumpYForce));
                        }
                    }
                    else
                    {
                        m_Rigidbody2D.AddForce(new Vector2(0f, jumpYForce));
                    }
                }

                m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

                return;
            }
        }


        private void DefaultRigidBodySettings()
        {
            m_defaultMass = m_Rigidbody2D.mass;
            m_defaultDrag = m_Rigidbody2D.drag;
            m_defaultAngularDrag = m_Rigidbody2D.angularDrag;
        }


        public void Hurt ()
		{
			m_HurtTimer = m_HurtTime;
			m_Hurt = true;
		}


        public void Death(bool death)
		{
			if (!m_Dead && death)
			{
				m_Dead = true;
                m_Anim.SetBool("Ground", true);
				m_Anim.SetBool("Death", death);

				GetComponent<Platformer2DUserControl>().enabled = false;

				m_AutoCam.enabled = false;

				this.enabled = false;

				GameObject go = GameObject.Find("LivesText");
				if (go != null) go.SendMessage("LoseLife");
			}
		}


        private void Respawn()
		{

            m_Rigidbody2D.velocity = new Vector2(0f, 0f);

			transform.position = GameObject.Find("LevelManager").GetComponent<LevelManager>().GetSpawnPoint();

			m_Dead = false;
			m_Anim.SetBool("Ground", false);
			m_Anim.SetBool("Death", false);

			GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = true;

			m_AutoCam.enabled = true;

			this.enabled = true;

		}


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        public void Slimed()
        {
            // This function runs when a message is sent from colliding with the SlimePit game object.
            // Here we set the player to a 'slimed' state, which changes the player move / platforming settings with some scaled values.
            if (!m_Slimed)
            {
                m_Slimed = true;
                //print("SLIMED");
                // Changing the values used for slime scale

                m_s_SScale = m_SlimeSpeedScale;
                m_s_AScale = m_SlimeAccellScale;
                m_s_JScale = m_SlimeJumpScale;
                m_s_JSusFScale = m_SlimeJumpSusForceScale;
                m_s_JSDurScale = m_SlimeJumpSusDurationScale;
                m_s_J2Scale = m_SlimeJump2Scale;
                m_s_J2SusFScale = m_SlimeJump2SusForceScale;
                m_s_J2SDurScale = m_SlimeJump2SusDurationScale;

                // Changing the RigidBody 2D settings according to the Slimed state values.
                m_Rigidbody2D.mass *= m_SlimeMassScale;
                m_Rigidbody2D.drag *= m_SlimeDragScale;
                m_Rigidbody2D.angularDrag *= m_SlimeAngularDragScale;

                m_SpriteRenderer.color = m_SlimeTint;
            }


            // if we have already been slimed and are now going back into slime, reset the slime exit flag - we are now entering more slime and we want to know when we exit slime!
            if (m_SlimeOut)
            {
                //print ("GOING BACK INTO SLIME");
                m_SlimeOut = false;
            }  
        }


        public void SlimeExit() // called by the SlimePit prefab exit trigger
        {
            m_SlimeOut = true;
            // Now that the character has left the slime, we can start the timer - set the temp slime timer to it's default value
            m_SlimeTempTimer = m_SlimeTempDuration;
        }


        public void SlimeTemp() // called every frame on update - if the player is slimed and the settings have a temp slime effect.
        {
            // count down the timer, and check for: 
            // timer end - stop slime effect
            // delay time - start fading the parameters.
            // if we are out of the slime pit, use the timer
            // if we go back in the slime, stop and reset the timer

            m_SlimeTempTimer -= Time.deltaTime;

            if (m_SlimeOut && m_SlimeTempTimer < 0)
            {
                //print("SLIME TEMP TIMER DONE - CHANGING BACK TO NORMAL");
                m_SlimeTempTimer = 0;
                WashSlime();
            }

            if (!m_SlimeOut && m_SlimeTempTimer > 0)
            {
                // if the slime temp timer is still going, but we have gone back into slime, then reset the timer
                m_SlimeTempTimer = m_SlimeTempDuration;
                //print("STOP SLIME TIMER - BACK IN SLIME");
            }
        }


        public void WashSlime()
        {
            // This function runs when a message is sent from colliding with the WaterPit game object.
            // Here we set the player to a 'slimed' is false state, which changes the player move / platforming settings back to default values.
            if (m_Slimed) m_Slimed = false;

            m_Rigidbody2D.mass = m_defaultMass;
            m_Rigidbody2D.drag = m_defaultDrag;
            m_Rigidbody2D.angularDrag = m_defaultAngularDrag;

            m_s_SScale = 1f;
            m_s_AScale = 1f;
            m_s_JScale = 1f;
            m_s_JSusFScale = 1f;
            m_s_JSDurScale = 1f;
            m_s_J2Scale = 1f;
            m_s_J2SusFScale = 1f;
            m_s_J2SDurScale = 1f;

            //print("UN__SLIMED");

            m_SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }


        public void EnemyJump()
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f); // Reset velocity on jump...
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * m_s_JScale));
            PlayJumpSound();
        }


        private void PlayJumpSound()
        {
            if (m_JumpAudioSource)
            {
                m_JumpAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.10f);
                m_JumpAudioSource.Play();
            }
        }


        private void PlayMidAirJumpSound()
        {
            if (m_MidAirJumpAudioSource)
            {
                m_MidAirJumpAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.10f);
                m_MidAirJumpAudioSource.Play();
            }
        }

        private void PlayDashSound()
        {
            if (m_DashAudioSource)
            {
                m_DashAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.10f);
                m_DashAudioSource.Play();
            }
        }
    }
}
