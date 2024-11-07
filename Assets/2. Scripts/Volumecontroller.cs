using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // �����̴��� �ʱⰪ ����
        bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1);

        // �����̴� ���� ����� �� �޼��� ȣ��
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);

        // �ʱ� ������ AudioManager�� ����
        AudioManager.Instance.SetBgmVolum(bgmSlider.value);
        AudioManager.Instance.SetSfxVolum(sfxSlider.value);
    }

    public void SetBgmVolume(float volume)
    {
        AudioManager.Instance.SetBgmVolum(volume); // ���� ���� �޼��� ȣ��
        PlayerPrefs.SetFloat("BgmVolume", volume); // ���� ���� ����
    }

    public void SetSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolum(volume); // ���� ���� �޼��� ȣ��
        PlayerPrefs.SetFloat("SfxVolume", volume); // ���� ���� ����
    }
}