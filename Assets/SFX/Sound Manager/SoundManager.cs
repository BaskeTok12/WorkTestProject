using Main_Controller;
using UI.UI_Manager;
using UnityEngine;
using UnityEngine.Audio;

namespace SFX.Sound_Manager
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Mixer")] 
        [SerializeField] private AudioMixerGroup mainMixer;
        [Header("Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource effectsSource;
        [Header("Sounds")] 
        [SerializeField] private AudioClip touchSound;
        [SerializeField] private AudioClip pointPickedSound;
        [SerializeField] private AudioClip destroySound;
        [SerializeField] private AudioClip restartSound;
    
        private const string MasterVolume = "MasterVolume";

        private void OnEnable()
        {
            PlayerBallController.OnDirectionChanged += PlayTouchSound;
            PointController.OnPointPicked += PlayPointPickedSound;
            PlayerBallCollisionController.OnPlayerCircleDestroyed += PlayDestroySound;
            UIManager.OnRestartScreenOpened += PlayRestartSound;
        }

        private void OnDisable()
        {
            PlayerBallController.OnDirectionChanged -= PlayTouchSound;
            PointController.OnPointPicked -= PlayPointPickedSound;
            PlayerBallCollisionController.OnPlayerCircleDestroyed -= PlayDestroySound;
            UIManager.OnRestartScreenOpened -= PlayRestartSound;
        }
    
        private void PlayTouchSound()
        {
            effectsSource.PlayOneShot(touchSound);
        }
    
        private void PlayPointPickedSound()
        {
            effectsSource.PlayOneShot(pointPickedSound);
        }
    
        private void PlayDestroySound()
        {
            effectsSource.PlayOneShot(destroySound);
        }
    
        private void PlayRestartSound()
        {
            musicSource.PlayOneShot(restartSound);
        }
    }
}
