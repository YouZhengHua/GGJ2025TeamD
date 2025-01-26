using UnityEngine;
using UnityEngine.Audio;

public enum BGMType
{
    START,
    GAMEON,
    OVER,
    CRIDIT
}
public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static BGMManager instance;
    public AudioSource audioSource;
    public bool isPlaying;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
    }

    public void PlaySound(BGMType sound)
    {
        instance.audioSource.Stop();
        instance.audioSource.clip = soundList[(int)sound];
        instance.audioSource.Play();
    }
    
}
