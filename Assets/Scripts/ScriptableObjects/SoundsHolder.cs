using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundsHolder : ScriptableObject
{
    public AudioClip[] audioClips;
}