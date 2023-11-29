using Main_Controller;
using Managers.Point_Controller;
using UI.UI_Manager;
using UnityEngine;
using UnityEngine.Audio;

namespace SFX.Sound_Manager
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Mixer")] 
        [SerializeField] private AudioMixerGroup mainMixer;
        [Header("Mixer Parameters")] 
        [SerializeField] private float maximalVolume;
        [Header("Sources")]
        [SerializeField] private AudioSource effectsSource;
        [Header("Sounds")] 
        [SerializeField] private AudioClip touchSound;
        [SerializeField] private AudioClip pointPickedSound;
        [SerializeField] private AudioClip destroySound;
        [SerializeField] private AudioClip restartSound;
    
        private const string MasterVolume = "MasterVolume";
        private const float MinimalMasterVolumeValue = -80f;

        private bool _isEnabled = true;

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
            effectsSource.PlayOneShot(restartSound);
        }
        
        public void ToggleVolume()
        {
            mainMixer.audioMixer.SetFloat(MasterVolume, _isEnabled ? MinimalMasterVolumeValue : maximalVolume);
            
            _isEnabled = !_isEnabled;
        }
    }
}
