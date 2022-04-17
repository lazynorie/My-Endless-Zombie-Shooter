using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> cliplist;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAKfireSound()
    {
        audio.clip = cliplist[0];
        audio.Play();
    }

    public void playAKReloadSoundClip()
    {
        audio.clip = cliplist[1];
        audio.Play();
    }
}
