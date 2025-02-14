using UnityEngine;
using TMPro;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown ScreenModeDropDown;
    private Resolutions resolutions;

    void Start()
    {
        resolutions = FindObjectOfType<Resolutions>();
        int val = PlayerPrefs.GetInt("ScreenMode", 0); // ถ้าไม่มีการตั้งค่า จะตั้งค่าเป็น 0 (FullScreenWindow)
        ScreenModeDropDown.value = val;
        ScreenModeDropDown.onValueChanged.AddListener(SetScreenMode);
    }

    public void SetScreenMode(int index)
    {
        PlayerPrefs.SetInt("ScreenMode", index); // เก็บค่าที่เลือกไว้ใน PlayerPrefs
        resolutions.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), index);
    }
}
