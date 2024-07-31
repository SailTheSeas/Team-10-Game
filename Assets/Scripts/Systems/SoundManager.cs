using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip personaClip, attackClip, gunClip, blockClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttack()
    {
        audioPlayer.PlayOneShot(attackClip);
    }

    public void PlayPersona()
    {
        audioPlayer.PlayOneShot(personaClip);
    }

    public void PlayGun()
    {
        audioPlayer.PlayOneShot(gunClip);
    }

    public void PlayBlock()
    {
        audioPlayer.PlayOneShot(gunClip);
    }
}
