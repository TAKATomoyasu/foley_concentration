using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sound
{
    [Serializable]
    public class ResultPlayer
    {
        [SerializeField] private AudioClip stepIs;
        [SerializeField] private AudioClip[] step1s;
        [SerializeField] private AudioClip[] step10;
        [SerializeField] private AudioClip[] step10s;

        [SerializeField] private AudioSource _audioSource;

        public void PlayResult(int stepNum)
        {
            PlayDelayed(stepNum).Forget();
        }

        async UniTask PlayDelayed(int stepNum)
        {
            int s = stepNum % 10;
            int d = stepNum / 10;
            Debug.Log("d: " + d + "s: " + s);

            await PlayAndWait(stepIs);

            if (s == 0) //10, 20
            {
                await PlayAndWait(step10s[d - 1]);
            }
            else
            {
                if (d > 0) // 10-,20-
                {
                    await PlayAndWait(step10[d - 1]);
                    await PlayAndWait(step1s[s - 1]);
                }
                else
                {
                    // 1,2,3
                    await PlayAndWait(step1s[s - 1]);
                }
            }
        }

        private async UniTask PlayAndWait(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            await UniTask.Delay((int) (clip.length * 1000));
        }
    }
}