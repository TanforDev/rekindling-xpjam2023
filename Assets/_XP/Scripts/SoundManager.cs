using System.Collections;
using System.Collections.Generic;
using TanforGames.Core.Singletons;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class SoundMananger : Singleton<SoundMananger>
{
    private ObjectPool<AudioSource> audioSourcePool;
    [SerializeField] private GameObject soundPrefab;


    private void Awake()
    {
        SetInitialReferences();
    }
    private void SetInitialReferences()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        audioSourcePool = new ObjectPool<AudioSource>(() =>
        {
            AudioSource audioSource = Instantiate(soundPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<AudioSource>();
            return audioSource;
        },
        audioSource =>
        {
            audioSource.gameObject.SetActive(true);
        },
        audioSource =>
        {
            audioSource.gameObject.SetActive(false);
        },
        audioSource =>
        {
            Destroy(audioSource.gameObject);
        },
        false, 32, 32);
    }
    public AudioSource PlayClip(AudioClip clip, Vector3 position, float volume = 1, float pitch = 1, float spartialBlend = 0)
    {
        AudioSource source = audioSourcePool.Get();
        source.Stop();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.spatialBlend = spartialBlend;

        source.transform.position = position;
        source.Play();

        StartCoroutine(ReturnToPool(source));
        return source;
    }
    public AudioSource PlayClip(AudioClip clip)
    {
        return PlayClip(clip, Vector3.zero);
    }
    public AudioSource PlayClip(AudioClip clip, Vector3 position)
    {
        return PlayClip(clip, position, 1, 1, 0);
    }

    private IEnumerator ReturnToPool(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        audioSourcePool.Release(source);
    }
}
