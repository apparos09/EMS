/*
 * References:
 * - https://www.youtube.com/watch?v=li2D_PEdST4
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace util
{
    // Links an audio intro with a main audio loop.
    public class AudioIntroLinker : MonoBehaviour
    {
        // The audio source.
        public AudioSource audioSource;

        // The intro clip of the audio.
        public AudioClip audioIntro;

        // Plays the audio on start.
        public bool playOnStart = true;

        // Start is called before the first frame update
        void Start()
        {
            // If the audio should be played on start.
            if (playOnStart)
                Play();
        }

        // Plays the audio intro, then plays the main audio clip.
        // If 'stopFirst' is set to true, AudioSource.Stop() is called to stop all audio...
        // Before playing the set audio with the intro. This only happens if the audio is currently playing.
        public void Play(bool stopFirst = true)
        {
            // Checks if the audio clip should be stopped first.
            if(stopFirst)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }

            // Plays the audio intro.
            audioSource.PlayOneShot(audioIntro);

            // Schedules the main audio to start playing.
            audioSource.PlayScheduled(AudioSettings.dspTime + audioIntro.length);
        }
    }
}