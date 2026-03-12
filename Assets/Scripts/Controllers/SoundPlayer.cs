using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource soundAudioSource;
        public static SoundPlayer Instance;
        [SerializeField] private  SoundsHolder soundsHolder;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void PlaySFX(string sfxName)
        {
            AudioClip audioClip = GetAudioClip(sfxName);
            if (audioClip != null)
            {
                soundAudioSource.PlayOneShot(audioClip);
            }
        }

        AudioClip GetAudioClip(string sfxName)
        {
            for(int i=0;i<soundsHolder.audioClips.Length;i++)
            {
                if (soundsHolder.audioClips[i].name == sfxName)
                    return soundsHolder.audioClips[i];
            }
            return null;
        }
    }
}
