using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool StopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        [SerializeField] inputChecker inputchecker;
        bool Jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            setWalk();
            UpdateJumpState();
            base.Update();
        }
        public void setWalk()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                setJumpState();
                return;
            }
            
           
                move.x = 0;
            
        }
        public void setJumpState()
        {
            if (IsJumpStateEqualGroundedAndGetInputButtonDownJump())
               
            {
                jumpState = JumpState.PrepareToJump;
                return;
            }

            setStopJumpTrue();
        }
        bool IsJumpStateEqualGroundedAndGetInputButtonDownJump()
        {
            return jumpState == JumpState.Grounded && Input.GetButtonDown("Jump");
        }
        public void setStopJumpTrue()
        {
            if (inputchecker.input())
            {
                StopJump = true;
                Schedule<PlayerStopJump>().player = this;
            }
        }
        void UpdateJumpState()
        {
            Jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    Jump = true;
                    StopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            setJumpFalse();

            setFlipXFalse();

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
        public void setVelocityY()
        {
            if (VelocityYMorethan0())
            {
                velocity.y = velocity.y * model.jumpDeceleration;
            }
        }
        bool VelocityYMorethan0()
        {
            return velocity.y > 0;
        }
        public void setJumpFalse()
        {
            if (Is_IsJumpEqualIsGrounded())
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                Jump = false;
                return;
            }
            setStopJumpFalse();
        }
        bool Is_IsJumpEqualIsGrounded()
        {
            return Jump && IsGrounded;
        }
        public void setStopJumpFalse()
        {
            if (StopJump)
            {
                StopJump = false;
                setVelocityY();
            }
        }
        public void setFlipXFalse()
        {
            if (IsMoveXMorethan001f())
            {
                spriteRenderer.flipX = false;
                return;
            }

            setFlipXTrue();
        }
        bool IsMoveXMorethan001f()
        {
            return move.x > 0.01f;
        }
        public void setFlipXTrue()
        {
            if (IsMoveXLessthanNegative001f())
                spriteRenderer.flipX = true;
        }
        bool IsMoveXLessthanNegative001f()
        {
            return move.x < -0.01f;
        }
        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}