using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private DataPersistence dataPersistence;

    [Header("AUDIO SETTINGS")]
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider soundfxSlider;

    [Header("AUDIO MIXER")]
    public AudioMixer audioMixer;

    [Header("DEFAULT AUDIO SETTINGS")]
    private float DefaultGeneralVolume = 1f;
    private float DefaultMusicVolume = 0.75f;
    private float DefaultEffectsVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        GetGeneralVolume();
        GetMusicVolume();
        GetEffectsVolume();
    }

    public void GetGeneralVolume()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("General Volume"))
        {
            SetGeneralVolume(DefaultGeneralVolume);
        }

        // Obtiene el valor guardado
        generalSlider.value = dataPersistence.GetFloat("General Volume");
    }

    public void GetMusicVolume()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("Music Volume"))
        {
            SetMusicVolume(DefaultMusicVolume);
        }

        // Obtiene el valor guardado
        musicSlider.value = dataPersistence.GetFloat("Music Volume");
    }

    public void GetEffectsVolume()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("Effects Volume"))
        {
            SetEffectsVolume(DefaultEffectsVolume);
        }

        // Obtiene el valor guardado
        soundfxSlider.value = dataPersistence.GetFloat("Effects Volume");
    }

    public void SetGeneralVolume(float volume)
    {
        audioMixer.SetFloat("General Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("General Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("Music Volume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Effects Volume", Mathf.Log10(volume) * 20);

        dataPersistence.SetFloat("Effects Volume", volume);
    }
}