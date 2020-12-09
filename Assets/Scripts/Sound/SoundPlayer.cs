using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundScriptableObject soundSet;
        [SerializeField] private SoundScriptableObject[] soundSetList;
        [SerializeField] private SoundScriptableObject alphabet;
        [SerializeField] private bool keyCall;

        private CancellationTokenSource _ct;

        private KeyCode[] soundSwitchKeys =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7
        };


        private void Start()
        {
            _ct = new CancellationTokenSource();
        }

        public void PlayCardSound(int triggerNum, int soundNum)
        {
            _ct.Cancel();
            _ct = new CancellationTokenSource();
            Debug.Log("Play: " + soundNum);
            PlayDelay(triggerNum, soundNum, _ct.Token).Forget();
        }

        async UniTask PlayDelay(int triggerNum, int soundNum, CancellationToken ct = default(CancellationToken))
        {
            if (keyCall)
            {
                audioSource.clip = alphabet.soundList[triggerNum];
                audioSource.Play();
                await UniTask.Delay((int) (alphabet.soundList[triggerNum].length * 1000));
            }

            audioSource.clip = soundSet.soundList[soundNum];
            audioSource.Play();

            if (soundSet.soundList[soundNum].length > 4)
            {
                await UniTask.Delay(4000);
                if (ct.IsCancellationRequested)
                {
                    // キャンセルされていたらOperationCanceledExceptionをスロー
                    Debug.Log("Cancel");
                    throw new OperationCanceledException(ct);
                }

                Debug.Log("Stop");
                audioSource.Stop();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                keyCall = !keyCall;
            }

            for (int i = 0; i < soundSwitchKeys.Length; i++)
            {
                if (Input.GetKeyDown(soundSwitchKeys[i])) soundSet = soundSetList[i];
            }
        }
    }
}