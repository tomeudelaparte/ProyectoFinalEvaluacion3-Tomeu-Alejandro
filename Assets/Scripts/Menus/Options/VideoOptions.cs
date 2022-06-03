using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOptions : MonoBehaviour
{
    [Header("DATE PERSISTENCE")]
    private DataPersistence dataPersistence;

    [Header("SETTINGS MANAGER")]
    private SettingsManager settingsManager;

    [Header("VIDEO SETTINGS")]
    public TMP_Dropdown dropdownDisplayModeUI;
    public TMP_Dropdown dropdownResolutionsUI;
    public Toggle toggleVerticalSyncUI;
    public Slider sliderFramerateLimitUI;
    public Toggle toggleFramerateShowUI;

    [Header("DEFAULT SETTINGS")]
    private int displayModeIndex = 0;
    private int resolutionIndex = 0;
    private bool verticalSyncOn = false;
    private float framesLimitSize = 60f;
    private bool showFramesOn = false;

    // Start is called before the first frame update
    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();
        settingsManager = FindObjectOfType<SettingsManager>();

        settingsManager.GetResolutionsAvailable(dropdownResolutionsUI);

        LoadSavedSettings();
    }

    private void LoadSavedSettings()
    {
        GetDisplayModeList();
        GetResolutionsList();
        GetVerticalSync();
        GetFramerateLimit();
        GetFramerateShow();
    }

    private void GetDisplayModeList()
    {
        if (!dataPersistence.HasKey("DISPLAY MODE"))
        {
            SetDisplayModeList(displayModeIndex);
        }

        dropdownDisplayModeUI.value = dataPersistence.GetInt("DISPLAY MODE");
    }

    private void GetResolutionsList()
    {
        if (!dataPersistence.HasKey("RESOLUTION"))
        {
            SetResolutionsList(resolutionIndex);
        }

        dropdownResolutionsUI.value = dataPersistence.GetInt("RESOLUTION");
    }

    private void GetVerticalSync()
    {
        if (!dataPersistence.HasKey("VERTICAL SYNC"))
        {
            SetVerticalSync(verticalSyncOn);
        }

        toggleVerticalSyncUI.isOn = dataPersistence.GetBool("VERTICAL SYNC");
    }

    private void GetFramerateLimit()
    {
        if (!dataPersistence.HasKey("FPS LIMIT"))
        {
            SetFramerateLimit(framesLimitSize);
        }

        sliderFramerateLimitUI.value = dataPersistence.GetInt("FPS LIMIT");
    }

    private void GetFramerateShow()
    {
        if (!dataPersistence.HasKey("SHOW FPS"))
        {
            SetFramerateShow(showFramesOn);
        }

        toggleFramerateShowUI.isOn = dataPersistence.GetBool("SHOW FPS");

        settingsManager.SetFramerateShow(toggleFramerateShowUI.isOn);
    }

    public void SetDisplayModeList(int index)
    {
        int resolution = dataPersistence.GetInt("RESOLUTION");

        settingsManager.setDisplayModeAndResolution(index, resolution);

        dataPersistence.SetInt("DISPLAY MODE", index);
    }

    public void SetResolutionsList(int index)
    {
        int display = dataPersistence.GetInt("DISPLAY MODE");

        settingsManager.setDisplayModeAndResolution(display, index);

        dataPersistence.SetInt("RESOLUTION", index);
    }

    public void SetVerticalSync(bool isOn)
    {
        settingsManager.SetVerticalSync(isOn);

        dataPersistence.SetBool("VERTICAL SYNC", isOn);
    }

    public void SetFramerateLimit(float size)
    {
        settingsManager.SetFramerateLimit((int)size);

        dataPersistence.SetInt("FPS LIMIT", (int)size);
    }

    public void SetFramerateShow(bool isOn)
    {
        settingsManager.SetFramerateShow(isOn);

        dataPersistence.SetBool("SHOW FPS", isOn);
    }
}