using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += (sender, args) =>
        {
            PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
        };
        DeliveryManager.Instance.OnRecipeFailed += (sender, args) =>
        {
            PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
        };
        CuttingCounter.OnAnyCut += (sender, args) =>
        {
            CuttingCounter cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        };
        PlayerController.Instance.OnPickedObject += (sender, args) =>
        {
            PlaySound(audioClipRefsSO.objectPickup, PlayerController.Instance.transform.position);
        };
        BaseCounter.OnAnyObjectDroped += (sender, args) =>
        {
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
        };
        TrashCounter.OnAnyObjectTrashed += (sender, args) =>
        {
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        };
        
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void ChangeVolume()
    { 
        volume += 0.1f; 
        if (volume > 1)
            volume = 0;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
    
}
