using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using DG.Tweening;
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
<<<<<<< HEAD
            checkJumpAndIsGrounded();
=======
            setJumpFalse();
            setStopJumpFalse();
>>>>>>> origin/master

            setFlipXFalse();

            setFlipxTrue();
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
<<<<<<< HEAD
        public void setVelocityY()
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * model.jumpDeceleration;
            }
        }
        public void checkJumpAndIsGrounded()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                
            }

            StopJump();
        }
        public void StopJump()
        {
            if (stopJump)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                return;
            }
            setStopJumpFalse();
        }
        public void setStopJumpFalse()
        {
            if (stopJump)
            {
                stopJump = false;

                setVelocityY();
            }
        }
        public void setFlipXFalse()
        {
            if (move.x > 0.01f)
=======
        public void setJumpFalse()
        {
            if (IsJumpEqualIsGrounded())
            {
                transform.DOJump(new Vector3(5, 0, 0), 1, 1, 1, false);
                //velocity.y = jumpTakeOffSpeed * model.jumpModifier;
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
>>>>>>> origin/master
            {
                spriteRenderer.flipX = false;
                return;
            }
<<<<<<< HEAD

            setFlipXTrue();
                
        }
        public void setFlipXTrue()
        {
            if (move.x < -0.01f)
            {
                spriteRenderer.flipX = true;
            }
        }
=======
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
>>>>>>> origin/master
    }
}