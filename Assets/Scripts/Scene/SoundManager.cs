using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] musics;
   
    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");

        if(musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();

        Cursor.visible = false;
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play(); 
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
