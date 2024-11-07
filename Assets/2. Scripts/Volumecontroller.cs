using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // 슬라이더의 초기값 설정
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1);

        // 슬라이더 값이 변경될 때 메서드 호출
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);

        // 초기 볼륨을 AudioManager에 설정
        AudioManager.Instance.SetBgmVolum(bgmSlider.value);
        AudioManager.Instance.SetSfxVolum(sfxSlider.value);
    }

    public void SetBgmVolume(float volume)
    {
        AudioManager.Instance.SetBgmVolum(volume); // 볼륨 설정 메서드 호출
        PlayerPrefs.SetFloat("BgmVolume", volume); // 볼륨 값을 저장
    }

    public void SetSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolum(volume); // 볼륨 설정 메서드 호출
        PlayerPrefs.SetFloat("SfxVolume", volume); // 볼륨 값을 저장
    }
}