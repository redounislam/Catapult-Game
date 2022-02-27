using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance = null;

    public AudioSource sfxAuidoSource;
    [SerializeField] private AudioSource backgroundAudioSource;



    [Header("Scanning SFX")]
    public AudioClip NutsSfx;
    public AudioClip BarrelExplosionSfx;
    public AudioClip GiantHitSfx;
    public AudioClip MultiplierHoopSfx;
    public AudioClip PizzaSfx;
    public AudioClip PlayerFallHit1fx;
    public AudioClip PlayerFallHit2SFX;
    public AudioClip PlayerShootSFX;
    public AudioClip StickStrechSFX;
    public AudioClip TowerBreakingSFX; 

    public static SoundManager SharedManager()
    {
        return sharedInstance;
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
   
        sfxAuidoSource = GetComponent<AudioSource>();
        //backgroundAudioSource =  GetComponent<AudioSource>();

    }


    public void PlaySFX(AudioClip audioClip)
    {
        sfxAuidoSource.PlayOneShot(audioClip);
    }

    public void StopSFX()
    {
        sfxAuidoSource.Stop();
    }
    public void PlaySFXDelayed(AudioClip audioClip)
    {
        StartCoroutine(delayedPlaySFX(audioClip));
    }

    public IEnumerator delayedPlaySFX(AudioClip audioClip)
    {
        yield return new WaitForSeconds(1f);
        sfxAuidoSource.PlayOneShot(audioClip);
    }

}
