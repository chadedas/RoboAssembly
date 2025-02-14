using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingbar : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;
    public float speed = 0.0f;
    int isProcessing = CentralScript.Instance.GetIsProcessing();
   

    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
        gameObject.SetActive(false);  // เริ่มต้นโดยซ่อนไว้
    }
    void Update()
    {
        if (isProcessing == 1)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true); // แสดง loading bar เมื่อเริ่ม process
            }

            if (imageComp.fillAmount != 1f)
            {
                imageComp.fillAmount += Time.deltaTime * speed;
            }
            else
            {
                imageComp.fillAmount = 0.0f;
            }
        }
        else if (gameObject.activeSelf)
        {
            gameObject.SetActive(false); // ซ่อน loading bar เมื่อ process จบ
            Debug.Log("Action is already in progress.");
        }
    }
}