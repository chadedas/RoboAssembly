using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    // ตัวแปรสำหรับ UI ป๊อปอัปและปุ่ม
    [SerializeField] private GameObject popupUI;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    void Start()
    {
        // ตั้งค่าเริ่มต้นให้ป๊อปอัปปิดอยู่ และปุ่มแสดงให้เห็น
        popupUI.SetActive(true);
        closeButton.gameObject.SetActive(true);
        openButton.gameObject.SetActive(false);

        // เพิ่ม Listener ให้ปุ่มเปิดและปิด
        openButton.onClick.AddListener(ShowPopup);
        closeButton.onClick.AddListener(HidePopup);
    }

    // ฟังก์ชันแสดงป๊อปอัป
private void ShowPopup()
{
    Debug.Log("Showing Popup");
    popupUI.SetActive(true);
    openButton.gameObject.SetActive(false);
    closeButton.gameObject.SetActive(true);
}

private void HidePopup()
{
    Debug.Log("Hiding Popup");
    popupUI.SetActive(false);
    openButton.gameObject.SetActive(true);
    closeButton.gameObject.SetActive(false);
}

}
