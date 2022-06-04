using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    public GameObject framerateCounterUI;
    public TextMeshProUGUI frameLimitUI;

    private Resolution[] resolutions;

    public void GetResolutionsAvailable(TMP_Dropdown dropdown)
    {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();

        System.Array.Reverse(resolutions);

        dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length - 3; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdown.AddOptions(options);

        dropdown.value = currentResolutionIndex;

        dropdown.RefreshShownValue();
    }

    public void UpdateFramerateSlider(float frames)
    {
        frameLimitUI.text = frames.ToString();
    }

    public void setDisplayModeAndResolution(int displayIndex, int resolutionIndex)
    {
        FullScreenMode displayMode = FullScreenMode.ExclusiveFullScreen;

        switch (displayIndex)
        {
            case 0:

                displayMode = FullScreenMode.ExclusiveFullScreen;
                break;

            case 1:

                displayMode = FullScreenMode.FullScreenWindow;
                break;

            case 2:

                displayMode = FullScreenMode.Windowed;
                break;
        }

        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, displayMode);
    }

    public void SetVerticalSync(bool isOn)
    {
        if (isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void SetFramerateLimit(int frames)
    {
        Application.targetFrameRate = frames;
    }

    public void SetFramerateShow(bool isOn)
    {
        if (isOn)
        {
            framerateCounterUI.SetActive(true);
        }
        else
        {
            framerateCounterUI.SetActive(false);
        }
    }
}
