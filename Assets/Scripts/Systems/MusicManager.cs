using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] fightMusic;
    [SerializeField] private AudioSource fightMusicSource;
    private DataHolder DH;
    // Start is called before the first frame update
    void Start()
    {
        DH = FindAnyObjectByType<DataHolder>();
        if (DH != null)
        {
            fightMusicSource.PlayOneShot(fightMusic[DH.GetCurrentCombat()]);
            /*switch (DH.GetCurrentCombat())
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
