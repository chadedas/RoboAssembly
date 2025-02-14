using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Resolutions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropDown.ClearOptions();

        // กรองความละเอียดที่ซ้ำกันออก
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!filteredResolutions.Any(x => x.width == resolutions[i].width && x.height == resolutions[i].height))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        
        // ใช้ความละเอียดเริ่มต้นเป็น Full Screen 
        if (!PlayerPrefs.HasKey("ResolutionWidth") || !PlayerPrefs.HasKey("ResolutionHeight"))
        {
            Resolution defaultResolution = Screen.currentResolution;
            SetResolution(defaultResolution.width, defaultResolution.height, 0); // Fullscreen mode
        }
        else
        {
            currentResolutionIndex = filteredResolutions.FindIndex(r =>
                r.width == PlayerPrefs.GetInt("ResolutionWidth") && r.height == PlayerPrefs.GetInt("ResolutionHeight"));
        }

        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
        resolutionDropDown.onValueChanged.AddListener(SetResolution);
    }

public void SetResolution(int resolutionIndex)
{
    if (resolutionIndex < 0 || resolutionIndex >= filteredResolutions.Count)
    {
        Debug.LogError("Invalid resolution index");
        return;
    }

    Resolution resolution = filteredResolutions[resolutionIndex];
    int screenMode = PlayerPrefs.GetInt("ScreenMode", 0);

    // บันทึกความละเอียดใน PlayerPrefs
    PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
    PlayerPrefs.SetInt("ResolutionHeight", resolution.height);

    // ตั้งค่าโหมดหน้าจอ
    SetResolution(resolution.width, resolution.height, screenMode);
}

public void SetResolution(int width, int height, int screenMode)
{
    FullScreenMode mode = screenMode == 0 ? FullScreenMode.FullScreenWindow
                          : screenMode == 1 ? FullScreenMode.ExclusiveFullScreen
                          : FullScreenMode.Windowed;

    // ตรวจสอบค่าความละเอียดและตั้งค่าความละเอียด
    if (width <= 0 || height <= 0)
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
    }

    Screen.SetResolution(width, height, mode != FullScreenMode.Windowed);
    Screen.fullScreenMode = mode;
}

}
