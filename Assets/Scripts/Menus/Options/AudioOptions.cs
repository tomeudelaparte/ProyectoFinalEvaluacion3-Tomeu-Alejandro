using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioOptions : MonoBehaviour
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

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        // Carga las opciones
        LoadSavedSettings();
    }

    // Carga las opciones
    private void LoadSavedSettings()
    {
        GetGeneralVolume();
        GetMusicVolume();
        GetEffectsVolume();
    }

    // Obtiene la opcion GeneralVolume
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

    // Obtiene la opcion MusicVolume
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

    // Obtiene la opcion EffectsVolume
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

    // Setea la opcion GeneralVolume
    public void SetGeneralVolume(float volume)
    {
        // Cambia el volumen en el AudioMixer
        audioMixer.SetFloat("General Volume", Mathf.Log10(volume) * 20);

        // Guarda la opcion
        dataPersistence.SetFloat("General Volume", volume);
    }

    // Setea la opcion MusicVolume
    public void SetMusicVolume(float volume)
    {
        // Cambia el volumen en el AudioMixer
        audioMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);

        // Guarda la opcion
        dataPersistence.SetFloat("Music Volume", volume);
    }

    // Setea la opcion EffectsVolume
    public void SetEffectsVolume(float volume)
    {
        // Cambia el volumen en el AudioMixer
        audioMixer.SetFloat("Effects Volume", Mathf.Log10(volume) * 20);

        // Guarda la opcion
        dataPersistence.SetFloat("Effects Volume", volume);
    }
}