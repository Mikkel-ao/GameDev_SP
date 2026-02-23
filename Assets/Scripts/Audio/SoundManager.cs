using UnityEngine;
using UnityEngine.UI;

// Central audio controller: plays background music and SFX based on SoundType.
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct SoundEntry
    {
        // Sound identifier.
        public SoundType type;
        // One or more clips for random variation.
        public AudioClip[] clips;
        // Per-sound volume multiplier.
        [Range(0f, 1f)] public float volume;
    }

    [Header("Clips")]
    // Library of sounds available to the manager.
    [SerializeField] private SoundEntry[] sounds;

    [Header("Sources")]
    // Music plays from musicSource, SFX from sfxSource.
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("UI")]
    // Optional UI slider to control master volume.
    [SerializeField] private Slider volumeSlider;
    // Global volume applied to all sounds.
    [Range(0f, 1f)] [SerializeField] private float masterVolume = 1f;

    [Header("Background")]
    // Auto-start background music on scene load.
    [SerializeField] private bool autoPlayBackground = true;

    // Singleton instance for static access.
    private static SoundManager instance;

    // Unity: initialize singleton and cache sources.
    private void Awake()
    {
        // Singleton setup and keep across scenes.
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

    // Unity: bind slider when object becomes active.
    private void OnEnable()
    {
        BindSlider(volumeSlider);
    }

    // Unity: finalize UI binding and optionally play background.
    private void Start()
    {
        BindSlider(volumeSlider);

        if (autoPlayBackground)
        {
            PlayBackground();
        }
    }

    // Unity: cleanup slider listener.
    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        }
    }

    // Hook a UI slider to master volume updates.
    public void BindSlider(Slider slider)
    {
        if (slider == null)
        {
            return;
        }

        // Bind UI slider to master volume.
        volumeSlider = slider;
        volumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);
        volumeSlider.SetValueWithoutNotify(masterVolume);
    }

    // Update master volume and keep looping music in sync.
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);

        // Keep background volume in sync with master volume.
        if (musicSource != null && musicSource.loop)
        {
            SoundEntry entry = FindSound(SoundType.BACKGROUND);
            musicSource.volume = entry.volume * masterVolume;
        }
    }

    // Static helper to play SFX by type.
    public static void PlaySound(SoundType type, float volumeMultiplier = 1f)
    {
        if (instance == null)
        {
            Debug.LogWarning("SoundManager: Instance not found!");
            return;
        }

        // Simple static entry point for SFX.
        instance.PlaySfx(type, volumeMultiplier);
    }

    // Play looping background music using BACKGROUND entry.
    public void PlayBackground()
    {
        // Start looping background music.
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

    // Internal SFX playback with volume scaling.
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

    // Lookup a sound entry by type.
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