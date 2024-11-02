using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

namespace RM_EM
{
    public class MatchAudio : EM_GameAudio
    {
        [Header("MatchAudio")]

        // The manager
        public MatchManager manager;

        // The BGM source.
        public AudioSource bgmSource;

        // The BGM source control.
        public AudioSourceLooper bgmSourceLooper;

        // The SFX source.
        public AudioSource sfxSource;

        [Header("Audio Clips/BGMs")]

        // The normal BGM
        public AudioClip normalMatchBgm;

        // Normal BGM Loop
        public Vector2 normalMatchLoopPoints;

        // The final match BGM
        public AudioClip bossMatchBgm;

        // Boss match loop
        public Vector2 bossMatchLoopPoints;

        // The match BGM. 0, 1 = Normal, 2 = Boss
        [Tooltip("The number of the BGM being used for matches. 1 is normal, and 2 is boss.")]
        public int matchBgmNumber = 0;

        // The results BGM.
        public AudioClip resultsBgm;

        // Match Results loop
        public Vector2 resultsLoopPoints;

        [Header("Audio Clips/SFXs")]

        // The match start sound effect.
        public AudioClip matchStartSfx;

        // The value select SFX.
        public AudioClip puzzleValueSelectSfx;

        // The keyboard click SFX.
        public AudioClip keyboardClickSfx;

        // The ball hit SFX.
        public AudioClip ballHitSfx;

        // Wrong answer sfx.
        public AudioClip wrongAnswerSfx;

        // Power use SFX.
        public AudioClip powerUseSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (manager == null)
                manager = MatchManager.Instance;


            // Plays the match BGM to start.
            PlayMatchBgm();
        }


        // BACKGROUND MUSIC //
        // Normal Match BGM
        public void PlayNormalMatchBgm()
        {
            bgmSource.Stop();

            // Loop Points
            bgmSourceLooper.clipStart = normalMatchLoopPoints.x;
            bgmSourceLooper.clipEnd = normalMatchLoopPoints.y;

            // Replace and play clip.
            bgmSource.clip = normalMatchBgm;
            bgmSource.Play();
        }

        // Final Match BGM
        public void PlayBossMatchBgm()
        {
            // Stops Audio
            bgmSource.Stop();

            // Loop Points
            bgmSourceLooper.clipStart = bossMatchLoopPoints.x;
            bgmSourceLooper.clipEnd = bossMatchLoopPoints.y;

            // Replace and play clip.
            bgmSource.clip = bossMatchBgm;
            bgmSource.Play();
        }


        // Plays the match BGM.
        public void PlayMatchBgm()
        {
            switch(matchBgmNumber)
            {
                case 0:
                case 1: // Normal
                    PlayNormalMatchBgm();
                    break;
                     
                case 2: // Boss
                    PlayBossMatchBgm();
                    break;
            }
        }

        // Sets the match BGM and plays it.
        public void PlayMatchBgm(int num)
        {
            matchBgmNumber = num;
            PlayMatchBgm();
        }


        // Results BGM
        public void PlayResultsBgm()
        {
            // Stops Audio
            bgmSource.Stop();

            // Loop Points
            bgmSourceLooper.clipStart = resultsLoopPoints.x;
            bgmSourceLooper.clipEnd = resultsLoopPoints.y;


            // Replace and play clips.
            bgmSource.clip = resultsBgm;
            bgmSource.Play();
        }



        // SOUND EFFECTS //
        // Match Start SFX
        public void PlayMatchStartSfx()
        {
            sfxSource.PlayOneShot(matchStartSfx);
        }

        // Puzzle Value Select SFX
        public void PlayPuzzleValueSelectSfx()
        {
            sfxSource.PlayOneShot(puzzleValueSelectSfx);
        }

        // Plays the keyboard click SFX
        public void PlayKeypadClickSfx()
        {
            sfxSource.PlayOneShot(keyboardClickSfx);
        }

        // Ball Hit SFX
        public void PlayBallHitSfx()
        {
            sfxSource.PlayOneShot(ballHitSfx);
        }

        // Plays the wrong answer SFX.
        public void PlayWrongAnswerSfx()
        {
            sfxSource.PlayOneShot(wrongAnswerSfx);
        }

        // Power Use SFX
        public void PlayPowerUseSfx()
        {
            sfxSource.PlayOneShot(powerUseSfx);
        }

    }
}