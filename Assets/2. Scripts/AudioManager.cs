using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    // Start is called before the first frame update
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;
        [HideInInspector]
        public AudioSource source;
    }
    public List<Sound> MusicSounds, SfxSounds;
    public AudioSource MusicSource, SfxSource;
    private Sound currentBGM;
    private const float fadeTime = 1f;
    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    public AudioMixer audioMixer;

    public Slider BgmSlider;
    public Slider SfxSlider;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
            SceneManager.sceneLoaded += OnSceneLoaded; //씬 로드 이벤트에 메서드 구독
        }
        else
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetInt("levelkey", 1);
        PlayerPrefs.SetInt("PlayerExpKey", 0);
    }
    void Start()
    {
        BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1);
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1);
        BgmSlider.value = 1;
        SfxSlider.value = 1;
        PlayMusic("BGM");
        currentBGM = MusicSounds[0];
    }

    public void SetBgmVolum(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        foreach(Sound s in MusicSounds)
        {
            s.source.volume = volume;
        }
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }
    public void SetSfxVolum(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        foreach (Sound s in SfxSounds)
        {
            s.source.volume = volume;
        }
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 구독 해제
    }

    private void InitializeSounds()
    {
        foreach (Sound s in MusicSounds)
        {
            //s.source = MusicSource;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in SfxSounds)
        {
            //s.source = SfxSource;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void PlayMusic(string name)
    {
        if(currentBGM != null && currentBGM.source.isPlaying)
        {
            currentBGM.source.Stop();
        }

        Sound s = MusicSounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        currentBGM = s;
        s.source.Play();
    }
    public void PlaySFX(string name)
    {
        Sound s = SfxSounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Play();
    }
    public void PlayOneShot(string clip)
    {
        PlayMusic(clip);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            currentBGM.source.Stop();
            currentBGM = MusicSounds[1];
            PlayMusic("GamePlay");
        }
        if(scene.name == "Success")
        {
            currentBGM.source.Stop();
            PlayMusic("Success");
        }
        if(scene.name == "Intro")
        {
           // currentBGM.source.Stop();
            PlayMusic("BGM");
        }

    }
}
