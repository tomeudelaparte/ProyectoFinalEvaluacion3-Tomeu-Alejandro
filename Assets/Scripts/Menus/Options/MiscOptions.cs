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

    // Start is called before the first frame update
    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        GetTutorialSkip();
    }

    private void GetTutorialSkip()
    {
        if (!dataPersistence.HasKey("TUTORIAL SKIP"))
        {
            SetTutorialSkip(tutorialSkipIsOn);
        }

        tutorialSkipUI.isOn = dataPersistence.GetBool("TUTORIAL SKIP");
    }

    public void SetTutorialSkip(bool isOn)
    {
        dataPersistence.SetBool("TUTORIAL SKIP", isOn);
    }

}
