using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiscOptions : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private DataPersistence dataPersistence;

    [Header("MISC SETTINGS")]
    public Toggle tutorialSkipUI;

    [Header("DEFAULT SETTINGS")]
    private bool tutorialSkipIsOn = false;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        LoadSavedSettings();
    }

    // Carga las opciones
    private void LoadSavedSettings()
    {
        GetTutorialSkip();
    }

    // Obtiene la opcion TutorialSkip
    private void GetTutorialSkip()
    {
        // Si no existe, guarda un valor predeterminado
        if (!dataPersistence.HasKey("TUTORIAL SKIP"))
        {
            SetTutorialSkip(tutorialSkipIsOn);
        }

        // Obtiene el valor guardado
        tutorialSkipUI.isOn = dataPersistence.GetBool("TUTORIAL SKIP");
    }

    // Setea la opcion TutorialSkip
    public void SetTutorialSkip(bool isOn)
    {
        // Guarda la opcion
        dataPersistence.SetBool("TUTORIAL SKIP", isOn);
    }

}
