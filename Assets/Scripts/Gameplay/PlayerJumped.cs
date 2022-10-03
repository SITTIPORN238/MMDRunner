using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerJumped"></typeparam>
    public class PlayerJumped : Simulation.Event<PlayerJumped>
    {
        public PlayerController player;

        public override void Execute()
        {
            if (IsPlayerAudioSourceEqualPlayerJumpAudio())
                player.audioSource.PlayOneShot(player.jumpAudio);
        }
        bool IsPlayerAudioSourceEqualPlayerJumpAudio()
        {
            return player.audioSource && player.jumpAudio;
        }
    }
}