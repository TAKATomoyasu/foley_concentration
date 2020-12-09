using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SynthTest : MonoBehaviour
{
    [SerializeField] private SoundScriptableObject soundSet;
    private AudioSource _audioSource;
    private int counter;
    private const string Keys = "awsedftgyhuj";
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        // var keySet = Keys.
        // Console.WriteLine(keySet);
        
        
        Observable.Return(0)
            .Delay(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                _audioSource.PlayOneShot(soundSet.soundList[counter]);
                
            });
    }

    // Update is called once per frame
    void Update()
    {
    }
}