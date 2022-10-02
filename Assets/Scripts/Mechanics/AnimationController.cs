using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// AnimationController integrates physics and animation. It is generally used for simple enemy animation.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class AnimationController : KinematicObject
    {
        /// <summary>
        /// Max horizontal speed.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Max jump velocity
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        /// <summary>
        /// Used to indicated desired direction of travel.
        /// </summary>
        public Vector2 move;

        /// <summary>
        /// Set to true to initiate a jump.
        /// </summary>
        public bool IsJump;

        /// <summary>
        /// Set to true to set the current jump velocity to zero.
        /// </summary>
        public bool IsStopJump;

        SpriteRenderer spriteRenderer;
        Animator animator;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void ComputeVelocity()
        {
            setJumpFalse();
            setStopJumpFalse();

            setFlipXFalse();

            setFlipxTrue();
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
        public void setJumpFalse()
        {
            if (IsJumpEqualIsGrounded())
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                IsJump = false;
                return;
            }
        }
        bool IsJumpEqualIsGrounded()
        {
            return IsJump && IsGrounded;
        }
        public void setFlipXFalse()
        {
            if (IsMoveXMorethan001f())
            {
                spriteRenderer.flipX = false;
                return;
            }
        }
        bool IsMoveXMorethan001f()
        {
            return move.x > 0.01f;
        }
        public void setFlipxTrue()
        {
            if (IsMoveXLessthanNegative001f())
                spriteRenderer.flipX = true;
        }
        bool IsMoveXLessthanNegative001f()
        {
            return move.x < -0.01f;
        }
        public void setVelocityY()
        {
            if (IsVelocityYMorethan0())
            {
                velocity.y = velocity.y * model.jumpDeceleration;
            }
        }
        bool IsVelocityYMorethan0()
        {
            return velocity.y > 0;
        }
        public void setStopJumpFalse()
        {
            if (Is_StopJump())
            {
                IsStopJump = false;
                setVelocityY();
            }
        }
        bool Is_StopJump()
        {
            return IsStopJump;
        }
    }
}