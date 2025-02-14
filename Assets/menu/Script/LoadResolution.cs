using UnityEngine;

public class LoadScreenSettings : MonoBehaviour
{
    private Resolutions resolutions;
    private ScreenModeSetting screenModeSetting;

    void Start()
    {
        resolutions = FindObjectOfType<Resolutions>();
        screenModeSetting = FindObjectOfType<ScreenModeSetting>();

        LoadSettings();
    }

private void LoadSettings()
{
    // โหลดค่าความละเอียดจาก PlayerPrefs
    int width = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
    int height = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);
    int screenMode = PlayerPrefs.GetInt("ScreenMode", 0); // ค่าเริ่มต้นเป็น 0 (FullScreenWindow)

    // ตรวจสอบค่าความละเอียดก่อนการตั้งค่า
    if (width <= 0 || height <= 0)
    {
        width = Screen.currentResolution.width;
        height = Screen.currentResolution.height;
    }

    // ตั้งค่าความละเอียดและโหมด
    resolutions.SetResolution(width, height, screenMode);
    screenModeSetting.SetScreenMode(screenMode); // อัปเดตค่าดรอปดาวน์ของโหมดหน้าจอ
}

}
