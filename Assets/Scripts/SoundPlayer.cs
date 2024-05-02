using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] private AudioMixerGroup _mixerGroup;

    [SerializeField] private AudioSource _firstSound;
    [SerializeField] private AudioSource _secondSound;
    [SerializeField] private AudioSource _thirdSound;

    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderMusic;

    [SerializeField] private bool _isMuted = false;

    private float _minVolumeValue = -80f;
    private float _maxVolumeValue = 0f;
    private float _converValue = 20f;

    private string _masterVolumeName = "Master";
    private string _soundVolumeName = "Sound";
    private string _musicVolumeName = "Music";

    private void OnEnable()
    {
        _sliderMaster.onValueChanged.AddListener(MasterVolumeChange);
        _sliderSound.onValueChanged.AddListener(SoundVolumeChange);
        _sliderMusic.onValueChanged.AddListener(MusicVolumeChange);
    }

    private void OnDisable()
    {
        _sliderMaster.onValueChanged.RemoveListener(MasterVolumeChange);
        _sliderSound.onValueChanged.RemoveListener(SoundVolumeChange);
        _sliderMusic.onValueChanged.RemoveListener(MusicVolumeChange);
    }

    public void PlayFirstSound()
    {
        _firstSound.Play();
    }

    public void PlaySecondSound()
    {
        _secondSound.Play();
    }

    public void PlayThirdSound()
    {
        _thirdSound.Play();
    }

    public void MuteUnmuteAll()
    {
        _isMuted = !_isMuted;

        if (_isMuted == true)
            _mixerGroup.audioMixer.SetFloat(_masterVolumeName, _minVolumeValue);
        else
            _mixerGroup.audioMixer.SetFloat(_masterVolumeName, _maxVolumeValue);
    }

    public void MasterVolumeChange(float volume)
    {
         _mixerGroup.audioMixer.SetFloat(_masterVolumeName, Mathf.Log10(volume) * _converValue);
    }

    public void SoundVolumeChange(float volume)
    {
        _mixerGroup.audioMixer.SetFloat(_soundVolumeName, Mathf.Log10(volume) * _converValue);
    }

    public void MusicVolumeChange(float volume)
    {
        _mixerGroup.audioMixer.SetFloat(_musicVolumeName, Mathf.Log10(volume) * _converValue);
    }
}