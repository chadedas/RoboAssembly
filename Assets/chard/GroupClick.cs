using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class GroupClick : MonoBehaviour
{
    public int groupNumber; // กำหนดตัวเลขกลุ่ม 1, 2 หรือ 3
    public GameObject[] groupObjects; // ออปเจคในกลุ่มนี้
    public float outlineWidth = 6f; // ขนาดของ Outline Width ที่จะกำหนด

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // ปิด Outline ของทุกออปเจคในกลุ่มเมื่อเคอร์เซอร์อยู่ที่ UI
            foreach (GameObject obj in groupObjects)
            {
                Outline outline = obj.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false; // ปิดการแสดง Outline
                }
            }
        }
    }
    void OnMouseDown()
    {
if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Send the group number to the central script
                CentralScript.Instance.ReceiveGroupNumber(groupNumber, groupObjects);
                Debug.Log("Clicked on a 3D object");
                UpdateOutlineColor(); // Update outline color on click
            }
            else
            {
                Debug.Log("Clicked on the UI");
            }
    }
    private void OnMouseEnter()
    {
        // เปิดการแสดง Outline ให้กับออปเจคทุกตัวในกลุ่ม
        UpdateOutlineColor();
    }

    private void OnMouseExit()
    {
        // ปิดการแสดง Outline ให้กับออปเจคทุกตัวในกลุ่ม
        foreach (GameObject obj in groupObjects)
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }

    private void UpdateOutlineColor()
    {
        int currentStep = CentralScript.Instance.GetCurrentStep();
        int ToolsNumber = ToggleButtonsMultiple.Instance.GetToolsNumber();
        int stack1 = CentralScript.Instance.GetStack1();
        int stack2 = CentralScript.Instance.GetStack2();
        int stack3 = CentralScript.Instance.GetStack3();

        foreach (GameObject obj in groupObjects)
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline != null)
            {
HashSet<int> NOT_M10 = new HashSet<int> { 100, 102, 104, 106, 118, 122 , 120, 116 , 108 }; //117 119 108
            HashSet<int> MOTOR = new HashSet<int> { 101, 103, 105, 107, 119, 123 };
            HashSet<int> AXIS456 = new HashSet<int> { 109 };
            HashSet<int> CounterBalance1 = new HashSet<int> { 110 };
            HashSet<int> CounterBalance2 = new HashSet<int> { 111 };
            HashSet<int> CounterBalance3 = new HashSet<int> { 113 };
            HashSet<int> CounterBalance4 = new HashSet<int> { 114, 115 };
            HashSet<int> CounterBalanceALL_step1 = new HashSet<int> { 112 };
            HashSet<int> CounterBalanceALL_step2 = new HashSet<int> { 117 };
            HashSet<int> AXIS2 = new HashSet<int> { 121 };
            HashSet<int> CONNECTER_MOTOR1 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            HashSet<int> CONNECTER_MOTOR2 = new HashSet<int> { 9, 10 };
            HashSet<int> CONNECTER_MOTOR3 = new HashSet<int> { 11, 12 };
                if (stack1 >= 36 && stack2 >= 19 && stack3 >= 23){
                    if (currentStep == groupNumber)
                    {
                        if(NOT_M10.Contains(groupNumber) && ToolsNumber == 1){outline.OutlineColor = Color.green;}
                        else if(MOTOR.Contains(groupNumber) && ToolsNumber == 0){outline.OutlineColor = Color.green;}
                        else if(AXIS456.Contains(groupNumber) && ToolsNumber == 6){outline.OutlineColor = Color.green;}
                        else if(CounterBalance1.Contains(groupNumber) && ToolsNumber == 2){outline.OutlineColor = Color.green;}
                        else if(CounterBalance2.Contains(groupNumber) && ToolsNumber == 5){outline.OutlineColor = Color.green;}
                        else if(CounterBalance3.Contains(groupNumber) && ToolsNumber == 4){outline.OutlineColor = Color.green;}
                        else if(AXIS2.Contains(groupNumber) && ToolsNumber == 6){outline.OutlineColor = Color.green;}
                        else if(CounterBalance4.Contains(groupNumber) && ToolsNumber == 9){outline.OutlineColor = Color.green;}
                        else if(CounterBalanceALL_step1.Contains(groupNumber) && ToolsNumber == 8){outline.OutlineColor = Color.green;}
                        else if(CounterBalanceALL_step2.Contains(groupNumber) && ToolsNumber == 0){outline.OutlineColor = Color.green;}
                        
                        else
                        {
                          outline.OutlineColor = Color.red;
                        }
                    
                    
                    
                    }
                    else
                    {
                        outline.OutlineColor = Color.red;
                    }
                }
                //step1
                else if(stack2 == 0 && stack3 == 0 && CONNECTER_MOTOR1.Contains(groupNumber)&& ToolsNumber == 0)
                {
                    outline.OutlineColor = Color.green;
                }
                //step2
                else if(stack1 >= 36 && stack3 == 0 &&  CONNECTER_MOTOR2.Contains(groupNumber) && ToolsNumber == 0)
                {
                    outline.OutlineColor = Color.green;
                }
                //step3
                else if(stack1 >= 36 && stack2 >= 19 && CONNECTER_MOTOR3.Contains(groupNumber)&& ToolsNumber == 0)
                {
                    outline.OutlineColor = Color.green;
                }
                else
                    {
                        outline.OutlineColor = Color.red;
                    }

                
                outline.OutlineWidth = outlineWidth; // ตั้งค่าขนาดของ Outline
                outline.enabled = true;
            }
        }
    }
}