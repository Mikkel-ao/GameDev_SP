

using UnityEngine;

namespace SoundScripts.SoundManager_main
{
    /// <summary>
    /// Animation behavior that plays a sound when a state is exited.
    /// Add this behavior to an animation state in the Animator Controller.
    /// </summary>
    public class PlaySoundExit : StateMachineBehaviour
    {
        [SerializeField] private SoundType sound;
        [SerializeField, Range(0, 1)] private float volume = 1;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SoundManager.PlaySound(sound, volume);
        }
    }
}