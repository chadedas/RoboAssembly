using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleRotate : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;
    public float rotateSpeed = 200f;
    private float currentvalue;

    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();

        // เริ่มต้นโดยซ่อนออปเจค
        SetVisibility(false);
    }

    void Update()
    {
        // ตรวจสอบค่า isProcessing จาก CentralScript
        bool isProcessing = CentralScript.Instance.GetIsProcessing() == 1;

        // แสดงหรือซ่อนออปเจคตามค่า isProcessing
        SetVisibility(isProcessing);

        if (isProcessing)
        {
            // หมุนออปเจคเมื่อ isProcessing เป็น true
            currentvalue += Time.deltaTime * rotateSpeed;
            rectComponent.transform.rotation = Quaternion.Euler(0f, 0f, -72f * (int)currentvalue);
        }
    }

    private void SetVisibility(bool isVisible)
    {
        // ใช้ CanvasGroup หรือ SetActive ตามความต้องการ
        gameObject.SetActive(isVisible);
    }
}
