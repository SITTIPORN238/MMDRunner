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

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
<<<<<<< HEAD
                        player.Bounce(3);
                    }
                    else
                    {
                        player.Bounce(10);
                    }
=======
                        player.Bounce(2);
                        return;
                    }
                    
                    
                        player.Bounce(7);
                    return;  
>>>>>>> origin/master
                }
                
              
                    Schedule<EnemyDeath>().enemy = enemy;
<<<<<<< HEAD
                    player.Bounce(3);
                }
=======
                    player.Bounce(2);
                return;  
>>>>>>> origin/master
            }
           
           
                Schedule<PlayerDeath>();
           
        }
    }
}