using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonaSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip attackOneClip, attackTwoClip, attackThreeClip, attackFourClip;
    [SerializeField] private AudioClip impactClip;
    // Start is called before the first frame update
    
    public void PlayAttackOne()
    {
        audioPlayer.PlayOneShot(attackOneClip);
    }
    public void PlayAttackTwo()
    {
        audioPlayer.PlayOneShot(attackTwoClip);
    }
    public void PlayAttackThree()
    {
        audioPlayer.PlayOneShot(attackThreeClip);
    }
    public void PlayAttackFour()
    {
        audioPlayer.PlayOneShot(attackFourClip);
    }

    public void PlayImpact()
    {
        audioPlayer.PlayOneShot(impactClip);
    }
}
