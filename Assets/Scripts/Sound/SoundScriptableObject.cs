using UnityEngine;

[CreateAssetMenu(menuName = "Param/Sound")]
public class SoundScriptableObject : ScriptableObject
{
    public AudioClip[] soundList;
}