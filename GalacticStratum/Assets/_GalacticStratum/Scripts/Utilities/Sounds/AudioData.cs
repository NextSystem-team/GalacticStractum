using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio")]
public class AudioData : ScriptableObject
{
    public string soundName;
    public AudioClip soundClip;
}
