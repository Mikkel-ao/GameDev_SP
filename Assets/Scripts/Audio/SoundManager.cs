//Author: Small Hedge Games
//Updated: 13/06/2024

using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundScripts.SoundManager_main
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundsSO SO;
        [SerializeField] private bool autoPlayBackground = true;
        private static SoundManager instance = null;
        private AudioSource audioSource;

        private void Awake()
        {
            if(!instance)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();

                Debug.Log($"SoundManager Awake: SO assigned = {SO != null}, audioSource found = {audioSource != null}");

                // Auto-play background if configured
                if (SO != null && autoPlayBackground)
                {
                    // Check if background entry exists and has clips before trying
                    if (SO.sounds != null && SO.sounds.Length > (int)SoundType.BACKGROUND && SO.sounds[(int)SoundType.BACKGROUND].sounds.Length > 0)
                    {
                        Debug.Log("SoundManager: Auto-playing background music.");
                        PlaySound(SoundType.BACKGROUND);
                    }
                    else
                    {
                        Debug.Log("SoundManager: Background entry missing or has no clips.");
                    }
                }

                // Keep across scenes so background music and manager persist
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // Prevent duplicate managers
                Destroy(gameObject);
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager: No instance available to play sound.");
                return;
            }

            if (instance.SO == null)
            {
                Debug.LogWarning("SoundManager: No Sounds SO assigned on the SoundManager instance.");
                return;
            }

            // Validate sounds array exists and contains the requested index
            if (instance.SO.sounds == null || instance.SO.sounds.Length <= (int)sound)
            {
                Debug.LogWarning($"SoundManager: Sounds SO does not contain an entry for '{sound}'.");
                return;
            }

            SoundList soundList = instance.SO.sounds[(int)sound];
            AudioClip[] clips = soundList.sounds;

            if (clips == null || clips.Length == 0)
            {
                Debug.LogWarning($"SoundManager: No clips assigned for sound '{sound}'.");
                return;
            }

            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            // Ensure manager audio source exists
            if (instance.audioSource == null)
                instance.audioSource = instance.GetComponent<AudioSource>();

            if (source != null)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                source.loop = sound == SoundType.BACKGROUND;
                source.Play();
            }
            else
            {
                instance.audioSource.outputAudioMixerGroup = soundList.mixer;

                if (sound == SoundType.BACKGROUND)
                {
                    // Use the manager's audio source for background music so it can loop
                    instance.audioSource.clip = randomClip;
                    instance.audioSource.volume = volume * soundList.volume;
                    instance.audioSource.loop = true;
                    instance.audioSource.Play();
                }
                else
                {
                    instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
                }
            }
        }
    }

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
    }
}