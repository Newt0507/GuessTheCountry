using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData")]
public class SoundData : ScriptableObject
{
    [System.Serializable]
    public class Sound
    {
        public ESound sound;
        public AudioClip clip;
    }

    public List<Sound> soundList;

    
}
