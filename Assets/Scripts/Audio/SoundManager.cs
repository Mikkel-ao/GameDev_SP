using UnityEngine;
using UnityEngine.UI;

namespace SoundScripts.SoundManager_main
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [System.Serializable]
        public struct SoundEntry
        {
            public SoundType type;
            public AudioClip[] clips;
            [Range(0f, 1f)] public float volume;
        }

        [Header("Clips")]
        [SerializeField] private SoundEntry[] sounds;

        [Header("Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("UI")]
        [SerializeField] private Slider volumeSlider;
        [Range(0f, 1f)] [SerializeField] private float masterVolume = 1f;

        [Header("Background")]
        [SerializeField] private bool autoPlayBackground = true;

        private static SoundManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                if (musicSource == null)
                {
                    musicSource = GetComponent<AudioSource>();
                }
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            BindSlider(volumeSlider);
        }

        private void Start()
        {
            BindSlider(volumeSlider);

            if (autoPlayBackground)
            {
                PlayBackground();
            }
        }

        private void OnDestroy()
        {
            if (volumeSlider != null)
            {
                volumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            }
        }

        public void BindSlider(Slider slider)
        {
            if (slider == null)
            {
                return;
            }

            volumeSlider = slider;
            volumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            volumeSlider.onValueChanged.AddListener(SetMasterVolume);
            volumeSlider.SetValueWithoutNotify(masterVolume);
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);

            if (musicSource != null && musicSource.loop)
            {
                SoundEntry entry = FindSound(SoundType.BACKGROUND);
                musicSource.volume = entry.volume * masterVolume;
            }
        }

        public static void PlaySound(SoundType type, float volumeMultiplier = 1f)
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager: Instance not found!");
                return;
            }

            instance.PlaySfx(type, volumeMultiplier);
        }

        public void PlayBackground()
        {
            SoundEntry entry = FindSound(SoundType.BACKGROUND);
            if (entry.clips == null || entry.clips.Length == 0)
            {
                Debug.LogWarning("SoundManager: No BACKGROUND clips assigned.");
                return;
            }

            if (musicSource == null)
            {
                Debug.LogWarning("SoundManager: No musicSource assigned.");
                return;
            }

            AudioClip clip = entry.clips[Random.Range(0, entry.clips.Length)];
            musicSource.clip = clip;
            musicSource.volume = entry.volume * masterVolume;
            musicSource.loop = true;
            musicSource.Play();
        }

        private void PlaySfx(SoundType type, float volumeMultiplier)
        {
            SoundEntry entry = FindSound(type);
            if (entry.clips == null || entry.clips.Length == 0)
            {
                Debug.LogWarning($"SoundManager: No clips assigned for '{type}'.");
                return;
            }

            if (sfxSource == null)
            {
                sfxSource = GetComponent<AudioSource>();
            }

            AudioClip clip = entry.clips[Random.Range(0, entry.clips.Length)];
            sfxSource.PlayOneShot(clip, entry.volume * volumeMultiplier * masterVolume);
        }

        private SoundEntry FindSound(SoundType type)
        {
            if (sounds != null)
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i].type == type)
                    {
                        return sounds[i];
                    }
                }
            }

            return new SoundEntry { type = type, clips = new AudioClip[0], volume = 1f };
        }
    }
}