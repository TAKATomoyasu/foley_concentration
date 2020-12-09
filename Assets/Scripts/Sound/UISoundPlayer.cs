using System.Collections;
using UnityEngine;

namespace Sound
{
    public class UISoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip correct;
        [SerializeField] private AudioClip incorrect;
        [SerializeField] private AudioClip noCard;
        [SerializeField] private AudioClip clear;
        [SerializeField] private AudioSource audioSource;

        public void PlayCorrect()
        {
            StartCoroutine(DelayPlay(correct, 1));
        }

        public void PlayWrong()
        {
            StartCoroutine(DelayPlay(incorrect, 1));
        }

        public void PlayNoCard()
        {
            audioSource.PlayOneShot(noCard);
        }

        public void PlayClear()
        {
            StartCoroutine(DelayPlay(clear, 2));
        }

        IEnumerator DelayPlay(AudioClip clip, float delay)
        {
            yield return new WaitForSeconds(delay);
            audioSource.PlayOneShot(clip);
        }
    }
}