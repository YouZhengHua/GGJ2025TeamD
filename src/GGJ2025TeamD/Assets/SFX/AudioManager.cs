using System;
using UnityEngine;

public enum SoundType
{
    LAYER_ON,
    LAYER_OFF,
    BEER_WATER,
    BEER,
    BUBBLE
}
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject[] soundList;
    private static AudioManager instance;
    private AudioSource audioSource;
    public bool isPlaying;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log(soundList.Length);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        if (instance.soundList[(int)sound].TryGetComponent<AudioSource>(out AudioSource AS))
        {
            AS.volume = volume;
            AS.Play();
        }
    }

    public static void StopSound(SoundType sound)
    {
        if (instance.soundList[(int)sound].TryGetComponent<AudioSource>(out AudioSource AS))
        {
            AS.Stop();
        }
    }
}
