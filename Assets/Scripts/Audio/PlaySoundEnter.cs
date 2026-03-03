using UnityEngine;

/// <summary>
/// Animation behavior that plays a sound when a state is entered.
/// Add this behavior to an animation state in the Animator Controller.
/// </summary>
public class PlaySoundEnter : StateMachineBehaviour
{
    [SerializeField] private SoundType sound;
    [SerializeField, Range(0, 1)] private float volume = 1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"PlaySoundEnter: Animation state entered '{stateInfo.shortNameHash}', playing sound '{sound}' with volume {volume}");
        SoundManager.PlaySound(sound, volume);
    }
}