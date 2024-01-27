using System.Collections.Generic;
using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.Extra;
using VgGames.Core.InjectModule;
using VgGames.Core.RandomValue;
using VgGames.Game.RuntimeData;
using VgGames.Game.Signals;

namespace VgGames.Game.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour, IInit
    {
        [Inject] private EventBus _events;
        [Inject] private SettingsData _settingsData;

        //[SerializeField] private AudioClip music;
        [SerializeField] private AudioClip coin;
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip explosion;
        [SerializeField] private RandomFloat pinchEffect = new(0.9f, 1.1f);

        private AudioSource _source;

        private Dictionary<SoundType, AudioClip> _audioClips;

        public void Init()
        {
            SourceInstall();
            InstallClips();
            
            _events.Add<SoundSignal>(PlaySound);
        }
        
        private void SourceInstall()
        {
            _source = GetComponent<AudioSource>();
            //_source.clip = music;
            _source.playOnAwake = false;
            _source.loop = false;
            //_source.Play();
        }

        private void InstallClips()
        {
            _audioClips = new Dictionary<SoundType, AudioClip>()
            {
                { SoundType.None, null },
                { SoundType.Click, click },
                { SoundType.Coin, coin },
                { SoundType.Explosion, explosion }
            };
        }

        private void PlaySound(SoundSignal signal)
        {
            if(!_settingsData.Sound) return;
            if (_audioClips.TryGetValue(signal.Type, out var clip))
            {
                _source.pitch = pinchEffect;
                _source.PlayOneShot(clip);
            }
        }

        private void OnDestroy()
        {
            _events.Remove<SoundSignal>(PlaySound);
        }
    }
}