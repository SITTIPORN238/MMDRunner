using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            jumpB(willHurtEnemy);
        }
        public void playerBounce()
        {
            if (!enemyHealth.IsAlive)
            {
                Schedule<EnemyDeath>().enemy = enemy;
                player.Bounce(2);
            }
            else
            {
                player.Bounce(7);
            }

        }
        public void kill()
        {
            if (enemyHealth != null)
            {
                enemyHealth.Decrement();
                playerBounce();
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemy;
                player.Bounce(2);
            }
        }
        public void jumpB(bool willHurtEnemy)
        {

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                kill();

            }
            else
            {
                Schedule<PlayerDeath>();
            }
        }

    }
}