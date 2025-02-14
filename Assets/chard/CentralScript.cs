using UnityEngine;
using System.Collections;
using Gaskellgames.CameraController;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.Animations;
using UnityEngine.UI;

public class CentralScript : MonoBehaviour
{
    public static CentralScript Instance;
    private int currentStep = 1; //1
    private int stack1 = 0; //0
    private int stack2 = 0; //0
    private int stack3 = 0; //0
    private GameObject destroyObject;
    [SerializeField]
    private CameraSwitcher cameraSwitcher;
    int cameraIndex = 1;

    private bool isProcessing;
    private bool isActive = false;
    private int receivedButtonNumber = -1;
    private bool isErrorStepsActive = false; // ตัวแปรสถานะของ error_steps
    private bool isErrorToolsActive = false; // ตัวแปรสถานะของ error_tools

    // เพิ่มตัวแปร GameObject error_steps
    [SerializeField] public GameObject error_steps;
    [SerializeField] public GameObject error_tools;
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject loading_undo;
    [SerializeField] public GameObject InformationTools1_WhatAssembly;
    [SerializeField] public GameObject InformationTools1_WhatTools;
    [SerializeField] public GameObject InformationTools1_Size;
    [SerializeField] public GameObject InformationTools2_Howdo;
    [SerializeField] public GameObject ErrorInformation1;
    [SerializeField] public GameObject HOIST_STEP1;
    [SerializeField] public GameObject HOIST_STEP3;
    [SerializeField] public GameObject BACK;
    public GameObject[] step1;
    public GameObject[] step2;
    public GameObject[] step3;
    public GameObject[] step4;
    public GameObject[] step5;
    public GameObject[] step6;
    public GameObject[] step7;
    public GameObject[] step8;
    public GameObject[] step9;
    public GameObject[] step10;
    public GameObject[] step11;
    public GameObject[] step12;
    public GameObject[] step100;
    public GameObject[] step101;
    public GameObject[] step102;
    public GameObject[] step103;
    public GameObject[] step104;
    public GameObject[] step105;
    public GameObject[] step106;
    public GameObject[] step107;
    public GameObject[] step108;
    public GameObject[] step109;
    public GameObject[] step110;
    public GameObject[] step111;
    public GameObject[] step112;
    public GameObject[] step113;
    public GameObject[] step114;
    public GameObject[] step115;
    public GameObject[] step116;
    public GameObject[] step117;
    public GameObject[] step118;
    public GameObject[] step119;
    public GameObject[] step120;
    public GameObject[] step121;
    public GameObject[] step122;
    public GameObject[] step123;
    public GameObject[] tools100;
    public GameObject[] tools102;
    public GameObject[] tools104;
    public GameObject[] tools106;
    public GameObject[] tools108;
    public GameObject[] tools109;
    public GameObject[] tools110;
    public GameObject[] tools111;
    public GameObject[] tools112;
    public GameObject[] tools113;
    public GameObject[] tools114;
    public GameObject[] tools115;
    public GameObject[] tools116;
    public GameObject[] tools117;
    public GameObject[] tools118;
    public GameObject[] tools119;
    public GameObject[] tools120;
    public GameObject[] tools121;
    public GameObject[] tools122;

    private Dictionary<int, GameObject[]> stepDictionary;
    private Dictionary<int, GameObject[]> stepToolsDictionary;
    private List<int> undoHistory = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (error_steps != null)
        {
            error_steps.SetActive(false);
        }
        if (error_tools != null)
        {
            error_tools.SetActive(false);
        }

    }

    void Start()
    {
        loading.SetActive(false);
        loading_undo.SetActive(false);
        HOIST_STEP1.SetActive(false);
        HOIST_STEP3.SetActive(false);
        ErrorInformation1.SetActive(false);


        stepDictionary = new Dictionary<int, GameObject[]>
    {
        { 1, step1 },
        { 2, step2 },
        { 3, step3 },
        { 4, step4 },
        { 5, step5 },
        { 6, step6 },
        { 7, step7 },
        { 8, step8 },
        { 9, step9 },
        { 10, step10 },
        { 11, step11 },
        { 12, step12 },
        { 100, step100 },
        { 101, step101 },
        { 102, step102 },
        { 103, step103 },
        { 104, step104 },
        { 105, step105 },
        { 106, step106 },
        { 107, step107 },
        { 108, step108 },
        { 109, step109 },
        { 110, step110 },
        { 111, step111 },
        { 112, step112 },
        { 113, step113 },
        { 114, step114 },
        { 115, step115 },
        { 116, step116 },
        { 117, step117 },
        { 118, step118 },
        { 119, step119 },
        { 120, step120 },
        { 121, step121 },
        { 122, step122 },
        { 123, step123}
    };

        stepToolsDictionary = new Dictionary<int, GameObject[]>
    {
        { 100, tools100 },
        { 102, tools102 },
        { 104, tools104 },
        { 106, tools106 },
        { 108, tools108 },
        { 109, tools109 },
        { 110, tools110 },
        { 111, tools111 },
        { 112, tools112 },
        { 113, tools113 },
        { 114, tools114 },
        { 115, tools115 },
        { 116, tools116 },
        { 117, tools117 },
        { 118, tools118 },
        { 119, tools119 },
        { 120, tools120 },
        { 121, tools121 },
        { 122, tools122 }
    };
        undo_changeModel(112);
    }
    public void SetActiveState(bool activeState)
    {
        isActive = activeState;
        Debug.Log("CentralScript active state updated: " + isActive);
    }

    public bool GetActiveState()
    {
        return isActive;
    }
    public void ReceiveGroupNumber(int groupNumber, GameObject[] groupObjects)
    {
        if (!isProcessing)
        {
HashSet<int> NOT_M10 = new HashSet<int> { 100, 102, 104, 106, 118, 122 }; //117 119 108
            HashSet<int> MOTOR = new HashSet<int> { 101, 103, 105, 107, 119, 123 };
            HashSet<int> NOTNEW = new HashSet<int> { 120, 116 };
            HashSet<int> NOTAXIS456 = new HashSet<int> { 108 };
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
            int sumOfConnecterMotor1 = CONNECTER_MOTOR1.Sum();
            int sumOfConnecterMotor2 = CONNECTER_MOTOR2.Sum();
            int sumOfConnecterMotor3 = CONNECTER_MOTOR3.Sum();
            if (receivedButtonNumber == 0 && CONNECTER_MOTOR1.Contains(groupNumber) ||
                 CONNECTER_MOTOR2.Contains(groupNumber) ||
                 CONNECTER_MOTOR3.Contains(groupNumber))
            {
                if (stack1 < sumOfConnecterMotor1 && stack2 == 0 && stack3 == 0 && !CONNECTER_MOTOR2.Contains(groupNumber) && !CONNECTER_MOTOR3.Contains(groupNumber))
                {
                    if (receivedButtonNumber == 0)
                    {
                        isProcessing = true;
                        AboutInformation(groupNumber);
                        TriggerAnimationForGroup(stepDictionary[groupNumber], "A1");
                        stack1 += groupNumber;
                        AddUndo(groupNumber);

                        if (stack1 >= sumOfConnecterMotor1)
                        {
                            cameraIndex = 2;
                            currentStep = 2;
                        }

                        if (cameraIndex > 50)
                        {
                            cameraIndex = 0;
                        }

                        StartCoroutine(DestroyAllObjectsInStep(groupNumber, 2.5f, cameraIndex));
                    }
                    else if (receivedButtonNumber != 0)
                    {
                        if (error_tools != null && !isErrorToolsActive)
                        {
                            error_tools.SetActive(true);
                            isErrorToolsActive = true;
                            StartCoroutine(HideErrorToolsWithDelay(3f));
                        }
                    }
                }
                else if (stack1 >= sumOfConnecterMotor1 && stack3 == 0 && stack2 < sumOfConnecterMotor2 && !CONNECTER_MOTOR1.Contains(groupNumber) && !CONNECTER_MOTOR3.Contains(groupNumber))
                {
                    if (CONNECTER_MOTOR2.Contains(groupNumber))
                    {
                        if (receivedButtonNumber == 0)
                        {
                            isProcessing = true;
                            AboutInformation(groupNumber);
                            TriggerAnimationForGroup(stepDictionary[groupNumber], "A1");
                            stack2 += groupNumber;
                            AddUndo(groupNumber);
                            if (stack2 >= sumOfConnecterMotor2)
                            {
                                cameraIndex = 3;
                                currentStep = 3;
                            }

                            if (cameraIndex > 50)
                            {
                                cameraIndex = 0;
                            }

                            StartCoroutine(DestroyAllObjectsInStep(groupNumber, 2.5f, cameraIndex));
                        }
                    }
                    else if (receivedButtonNumber != 0)
                    {
                        if (error_tools != null && !isErrorToolsActive)
                        {
                            error_tools.SetActive(true);
                            isErrorToolsActive = true;
                            StartCoroutine(HideErrorToolsWithDelay(3f));
                        }
                    }
                }
                else if (stack1 >= sumOfConnecterMotor1 && stack2 >= sumOfConnecterMotor2 && stack3 < sumOfConnecterMotor3 && !CONNECTER_MOTOR1.Contains(groupNumber) && !CONNECTER_MOTOR2.Contains(groupNumber))
                {
                    if (CONNECTER_MOTOR3.Contains(groupNumber) && receivedButtonNumber == 0)
                    {
                        if (receivedButtonNumber == 0)
                        {
                            isProcessing = true;
                            AboutInformation(groupNumber);
                            TriggerAnimationForGroup(stepDictionary[groupNumber], "A1");
                            stack3 += groupNumber;
                            AddUndo(groupNumber);
                            if (stack3 >= sumOfConnecterMotor3)
                            {
                                cameraIndex = 4;
                                currentStep = 99;
                            }

                            if (cameraIndex > 50)
                            {
                                cameraIndex = 0;
                            }

                            StartCoroutine(DestroyAllObjectsInStep(groupNumber, 2.5f, cameraIndex));
                        }
                        else if (receivedButtonNumber != 0)
                        {
                            if (error_tools != null && !isErrorToolsActive)
                            {
                                error_tools.SetActive(true);
                                isErrorToolsActive = true;
                                StartCoroutine(HideErrorToolsWithDelay(3f));
                            }
                        }

                    }
                }
                else if (error_steps != null && !isErrorStepsActive)
                {
                    error_steps.SetActive(true);
                    isErrorStepsActive = true;
                    StartCoroutine(HideErrorStepsWithDelay(3f));
                }
            }
            else if (receivedButtonNumber != 0 && CONNECTER_MOTOR1.Contains(groupNumber) ||
                 CONNECTER_MOTOR2.Contains(groupNumber) ||
                 CONNECTER_MOTOR3.Contains(groupNumber))
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }
            else if (groupNumber == currentStep &&
            stack1 >= sumOfConnecterMotor1 &&
            stack2 >= sumOfConnecterMotor2 &&
            stack3 >= sumOfConnecterMotor3)
            {
                if (NOT_M10.Contains(groupNumber) && receivedButtonNumber == 1)
                {

                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 16f;
                    AddUndo(groupNumber);
                    if (groupNumber == 106)
                    {
                        HOIST_STEP1.SetActive(true);
                    }
                    //แก้แล้ว 25/11
                    else if (groupNumber == 118)
                    {
                        HOIST_STEP3.SetActive(true);
                    }
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (MOTOR.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 0f;
                    AddUndo(groupNumber);
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                if (AXIS456.Contains(groupNumber) && receivedButtonNumber == 6)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 2.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance1.Contains(groupNumber) && receivedButtonNumber == 2)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 4f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance2.Contains(groupNumber) && receivedButtonNumber == 5)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 13.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance3.Contains(groupNumber) && receivedButtonNumber == 4)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 3f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance4.Contains(groupNumber) && receivedButtonNumber == 9)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalanceALL_step1.Contains(groupNumber) && receivedButtonNumber == 8)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 3.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalanceALL_step2.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 0f;
                    AddUndo(groupNumber);
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));

                }
                else if (AXIS2.Contains(groupNumber) && receivedButtonNumber == 6)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 6.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 7f + delayAnimationTrigger, cameraIndex));
                }
                else if (NOTAXIS456.Contains(groupNumber) && receivedButtonNumber == 1)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 34f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
                else if (NOTNEW.Contains(groupNumber) && receivedButtonNumber == 1)
                {
                    isProcessing = true;
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 2.5f;
                    AddUndo(groupNumber);
                    StartCoroutine(ToolAnimation(stepToolsDictionary[groupNumber], groupNumber, delayAnimationTrigger));
                    StartCoroutine(delayAnimation(stepDictionary[groupNumber], delayAnimationTrigger));
                    int cameraIndex = (groupNumber - 95);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(DestroyAllObjectsInStep(groupNumber, 5f + delayAnimationTrigger, cameraIndex));
                }
            }
            else if (!NOT_M10.Contains(groupNumber) && receivedButtonNumber == 1)
            {
                // แสดง error_steps
                if (error_steps != null && !isErrorStepsActive)
                {
                    error_steps.SetActive(true);
                    isErrorStepsActive = true;
                    StartCoroutine(HideErrorStepsWithDelay(3f));
                }
            }
            else if (!MOTOR.Contains(groupNumber) && receivedButtonNumber == 0)
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }
            else if (!AXIS2.Contains(groupNumber) && !AXIS456.Contains(groupNumber) && receivedButtonNumber == 6)
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }
            else if (!CounterBalance1.Contains(groupNumber) && receivedButtonNumber == 2)
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }
            else if (!CounterBalance2.Contains(groupNumber) && receivedButtonNumber == 5)
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }
            else if (!CounterBalance3.Contains(groupNumber) && receivedButtonNumber == 4)
            {
                // แสดง error_tools
                if (error_tools != null && !isErrorToolsActive)
                {
                    error_tools.SetActive(true);
                    isErrorToolsActive = true;
                    StartCoroutine(HideErrorToolsWithDelay(3f));
                }
            }



        }
        else if (isProcessing)
        {
            Debug.Log("Action is already in progress.");
        }
        else
        {
            // แสดง error_steps และซ่อน error_tools หากต้องการ
            if (error_steps != null && error_tools != null && !isErrorStepsActive)
            {
                error_steps.SetActive(true);
                error_tools.SetActive(false);
                isErrorStepsActive = true;
                StartCoroutine(HideErrorStepsWithDelay(3f));
            }
        }
    }

    // ปรับปรุงฟังก์ชันการซ่อน error_tools

    private IEnumerator ToolAnimation(GameObject[] toolObject, int groupNumber, float delay)
    {

        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject obj in toolObject)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("A1");
            }
        }
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(false);
        }
    }
    private IEnumerator HideErrorToolsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (error_tools != null)
        {
            error_tools.SetActive(false);
            isErrorToolsActive = false; // รีเซ็ตสถานะ
        }
    }

    private void AboutInformation(int groupnumber)
    {
        // เข้าถึง Text Component และเปลี่ยนข้อความ
        if (groupnumber <= 12)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "คอนเนคเตอร์มอเตอร์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดคอนเนคเตอร์มอเตอร์ ต้องหมุนที่ปลายหัวคอนเนคเตอร์ ในทิศทางทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 100)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 4";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 4 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 101)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 4";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 4 ต้องความระมัดระวังในการถอด เนื่องจากมีเพลาด้านในอยู่ติดกับมอเตอร์ สามารถโค้งงอได้";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 102)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 5";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 4 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 103)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 5";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 5 ต้องความระมัดระวังในการถอด เนื่องจากมีเพลาด้านในอยู่ติดกับมอเตอร์ ไม่สามารถโค้งงอได้ ดึงมาตรงๆ ได้เลย";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 104)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 6";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 4 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 105)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 6";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 6 ต้องความระมัดระวังในการถอด เนื่องจากมีเพลาด้านในอยู่ติดกับมอเตอร์ สามารถโค้งงอได้";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 106)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 3";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 5 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 107)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 3";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 3 ต้องความระมัดระวังในการถอด และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 108)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตแขนส่วนบน";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10 , M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตแขนส่วนบน น็อต(วงนอก) ขนาด M10 และน็อต(วงใน) ขนาด M14 ต้องใช้รอกคอยประคองเสมอ หลังจากถอดแล้วจะทำให้แขนส่วนบนหลุดออกมาทันที หากไม่มีรอกจะเกิดอันตราย";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 109)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "แขนส่วนบน";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ชะแลง";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดแขนส่วนบน ใช้ชะแลงงัดเพื่อให้แขนส่วนบนหลุดออกมา ค่อนข้างแน่นมาก และอย่าลืมรอกคอยประคองแขนด้วย";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 118)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 2 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M10 หมุนทวนเข็มนาฬิกา จำนวน 4ชิ้น";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 119)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 2 ต้องความระมัดระวังในการถอด และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 110)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "สลักเคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อค";
            InformationTools1_Size.GetComponent<Text>().text = "M19";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดสลักเคาเตอร์บาลานซ์ ต้องใช้ลูกบล็อคหกเหลี่ยมขนาด M19 หมุนทวนเข็มนาฬิกา และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 111)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "สลักเคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ตัวกระตุกเคาเตอร์บาลานซ์";
            InformationTools1_Size.GetComponent<Text>().text = "M16";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดสลักเคาเตอร์บาลานซ์ ต้องใช้ตัวกระตุกเคาเตอร์บาลานซ์ขนาด M16 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 113)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "ฝาปิด";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ไขควงแบน";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดฝาปิด ต้องใช้ไขควงแบนงัดออกมา และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 114)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "โอริง";
            InformationTools1_WhatTools.GetComponent<Text>().text = "คีมถ่างแหวน";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดโอริง ต้องใช้คีมถ่างแหวน เพื่อนำแหวนโอริงออกมาจากแบริ่งเคาเตอร์บาลานซ์";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 115)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "โอริง";
            InformationTools1_WhatTools.GetComponent<Text>().text = "คีมถ่างแหวน";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดโอริง ต้องใช้คีมถ่างแหวน เพื่อนำแหวนโอริงออกมาจากแบริ่งเคาเตอร์บาลานซ์";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 116)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตใต้เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "น็อตใต้เคาเตอร์บาลานซ์ ต้องใช้ลูกบล็อคเดือยโผล่ขนาด M14 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 112)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ค้อนยาง";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดเคาเตอร์บาลานซ์ ต้องใช้ค้อนยางตอกไปที่หัวเคาเตอร์บาลานซ์ ให้หลุดออกจากตัวแขนกลอุตสาหกรรม";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 120)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตเกียร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "น็อตเกียร์แกน2 ต้องใช้ลูกบล็อคเดือยโผล่ขนาด M14 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 121)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "แขนแกน2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ชะแลง";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดแขนแกน 2 ต้องใช้ชะแลงงัดแขนแกน 2 ออกมา และใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว 25/11
        else if (groupnumber == 117)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ , รอก";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดเคาเตอร์บาลานซ์ ต้องใช้รอกยกออก เนื่องจากเคาเตอร์บาลานซ์มีน้ำหนักที่สูงมาก";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 122)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 1";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดน็อตมอเตอร์แกน 1 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M10 หมุนทวนเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 123)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 1";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การถอดมอเตอร์แกน 1 ต้องความระมัดระวังในการถอด";
            ErrorInformation1.SetActive(false);
        }
    }
    // ปรับปรุงฟังก์ชันการซ่อน error_steps
    private IEnumerator HideErrorStepsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (error_steps != null)
        {
            error_steps.SetActive(false);
            isErrorStepsActive = false; // รีเซ็ตสถานะ
        }
    }

    public void ReceiveButtonNumber(int buttonNumber)
    {
        receivedButtonNumber = buttonNumber;
        Debug.Log("Received button number: " + receivedButtonNumber);
    }

    private void TriggerAnimationForGroup(GameObject[] groupObjects, string triggerName)
    {
        foreach (GameObject obj in groupObjects)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(triggerName);
            }
        }
    }
    private IEnumerator delayAnimation(GameObject[] groupObjects, float delay)
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(delay);
        TriggerAnimationForGroup(groupObjects, "A1");
    }
    // ตัวอย่างการลูปผ่านทุก step
    private IEnumerator DestroyAllObjectsInStep(int groupNumber, float delay, int cameraIndex)
    {
        // ตรวจสอบว่า groupNumber อยู่ใน Dictionary หรือไม่
        if (stepDictionary.ContainsKey(groupNumber))
        {
            GameObject[] selectedStep = stepDictionary[groupNumber];

            // ลูปผ่านทุกตัวใน selectedStep และทำการลบ
            foreach (GameObject obj in selectedStep)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    StartCoroutine(DestroyObjectWithDelay(groupNumber, obj, delay, cameraIndex));
                }
            }

            // รอให้การลบเสร็จสิ้นก่อนจะดำเนินการขั้นตอนถัดไป
            yield return new WaitForSeconds(delay);
        //แก้แล้ว 25/11
            if (groupNumber == 112) { changeModel(groupNumber); }

            currentStep++;
            isProcessing = false;
            loading.SetActive(false); // ซ่อน UI ของการโหลด


        }
        else
        {
            Debug.LogError("Step for group " + groupNumber + " not found.");
        }
    }

    // การลบและการเปลี่ยนกล้อง (ไม่เปลี่ยนแปลงจากโค้ดที่คุณให้มา)
    private IEnumerator DestroyObjectWithDelay(int groupNumber, GameObject obj, float delay, int cameraIndex)
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            obj.SetActive(false); // เปลี่ยนจาก Destroy เป็น SetActive(false)
            Debug.Log(obj.name + " has been deactivated after " + delay + " seconds.");
        }

        if (cameraSwitcher != null)
        {
            if (isActive)
            {
                cameraSwitcher.SwitchToCamera(0);
                Debug.Log("Switching to cameraIndex: " + cameraIndex);
            }
            else if(groupNumber == 123){
                cameraSwitcher.SwitchToCamera(0);
            }else
            {
                cameraSwitcher.SwitchToCamera(cameraIndex);
                Debug.Log("Switching to cameraIndex: " + cameraIndex);
            }
        }
        else
        {
            Debug.LogError("CameraSwitcher is not assigned");
        }
    }

    public int GetCurrentStep()
    {
        return currentStep;
    }
    public int GetStack1()
    {
        return stack1;
    }
    public int GetStack2()
    {
        return stack2;
    }
    public int GetStack3()
    {
        return stack3;
    }
    public int GetSumStack1()
    {
        return stack1;
    }
    public int GetIsProcessing()
    {
        return isProcessing ? 1 : 0; // แปลง bool เป็น int (1 สำหรับ true และ 0 สำหรับ false)
    }








    //ประกอบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบบ



    public void undoStep()
    {
           HashSet<int> NOT_M10 = new HashSet<int> { 100, 102, 104, 106, 118, 122 }; //117 119 108
            HashSet<int> MOTOR = new HashSet<int> { 101, 103, 105, 107, 119, 123 };
            HashSet<int> NOTNEW = new HashSet<int> { 120, 116 };
            HashSet<int> NOTAXIS456 = new HashSet<int> { 108 };
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
        if (!isProcessing)
        {

            int size = undoHistory.Count;
            Debug.LogWarning("size : " + size);
            if (size <= 36 && size > 0)
            {
                isProcessing = true;
                loading_undo.SetActive(true);
                int lastUndoRemoved = GetLastUndoAndRemove();
                Debug.LogWarning("lastUndoRemoved : " + lastUndoRemoved);
                if (CONNECTER_MOTOR1.Contains(lastUndoRemoved))
                {
                    AboutInformation(lastUndoRemoved);
                    float delayAnimation = 2.5f;
                    cameraIndex = 1;
                    currentStep = 1;
                    stack1 -= lastUndoRemoved;
                    foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                    {
                        obj.SetActive(true);
                    }
                    StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimation, cameraIndex));

                }
                else if (CONNECTER_MOTOR2.Contains(lastUndoRemoved))
                {
                    AboutInformation(lastUndoRemoved);
                    float delayAnimation = 2.5f;
                    cameraIndex = 2;
                    currentStep = 2;
                    stack2 -= lastUndoRemoved;
                    foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                    {
                        obj.SetActive(true);
                    }
                    StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimation, cameraIndex));
                    cameraSwitcher.SwitchToCamera(cameraIndex);

                }
                else if (CONNECTER_MOTOR3.Contains(lastUndoRemoved))
                {
                    AboutInformation(lastUndoRemoved);
                    float delayAnimation = 2.5f;
                    cameraIndex = 3;
                    currentStep = 3;
                    stack3 -= lastUndoRemoved;
                    foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                    {
                        obj.SetActive(true);
                    }
                    StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimation, cameraIndex));
                    cameraSwitcher.SwitchToCamera(cameraIndex);

                }
                else if (lastUndoRemoved >= 100)
                {

                    if (NOT_M10.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float ToolsdelayAnimation = 16f;
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                        StartCoroutine(ShowTools(stepToolsDictionary[lastUndoRemoved], delayAnimationAfter));
                        StartCoroutine(UNDO_ToolAnimation(lastUndoRemoved, stepToolsDictionary[lastUndoRemoved], ToolsdelayAnimation, delayAnimationAfter + 0.5f));
                    }
                    else if (MOTOR.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        //float ToolsdelayAnimation = 0f;
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (NOTNEW.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float ToolsdelayAnimation = 2.5f;
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                        StartCoroutine(ShowTools(stepToolsDictionary[lastUndoRemoved], delayAnimationAfter));
                        StartCoroutine(UNDO_ToolAnimation(lastUndoRemoved, stepToolsDictionary[lastUndoRemoved], ToolsdelayAnimation, delayAnimationAfter + 0.5f));
                    }
                    else if (NOTAXIS456.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float ToolsdelayAnimation = 34f;
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                        StartCoroutine(ShowTools(stepToolsDictionary[lastUndoRemoved], delayAnimationAfter));
                        StartCoroutine(UNDO_ToolAnimation(lastUndoRemoved, stepToolsDictionary[lastUndoRemoved], ToolsdelayAnimation, delayAnimationAfter + 0.5f));
                    }
                    else if (AXIS456.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (CounterBalance1.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float ToolsdelayAnimation = 4f;
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                        StartCoroutine(ShowTools(stepToolsDictionary[lastUndoRemoved], delayAnimationAfter));
                        StartCoroutine(UNDO_ToolAnimation(lastUndoRemoved, stepToolsDictionary[lastUndoRemoved], ToolsdelayAnimation, delayAnimationAfter + 0.5f));
                    }
                    else if (CounterBalance2.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (CounterBalance3.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (CounterBalance4.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (CounterBalanceALL_step1.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (CounterBalanceALL_step2.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 5f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                    else if (AXIS2.Contains(lastUndoRemoved))
                    {
                        AboutInformation(lastUndoRemoved);
                        float delayAnimationAfter = 7f;
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep -= 1;
                        foreach (GameObject obj in stepDictionary[lastUndoRemoved])
                        {
                            obj.SetActive(true);
                        }
                        StartCoroutine(Undo_TriggerAnimationForGroup(lastUndoRemoved, stepDictionary[lastUndoRemoved], "A3", delayAnimationAfter, cameraIndex));
                    }
                }
            }
        }
        // On Step 1 Connecter Motor 2
    }
    private IEnumerator UNDO_ToolAnimation(int lastUndoRemoved, GameObject[] toolObject, float delay, float delayAnimation)
    {
        yield return new WaitForSeconds(delayAnimation);
        foreach (GameObject obj in toolObject)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("A3");
            }
        }
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(false);
        }
        if (lastUndoRemoved >= 100)
        {
            isProcessing = false;
            loading_undo.SetActive(false);
        }



    }
    private IEnumerator ShowTools(GameObject[] toolObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(true);
        }

    }

    private IEnumerator Undo_TriggerAnimationForGroup(int lastUndoRemoved, GameObject[] groupObjects, string triggerName, float delay, int cameraIndex)
    {
        //แก้แล้ว 25/11
        HashSet<int> NotHaveTools = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 101, 103, 105, 107, 109, 119, 111, 113, 114, 115, 112, 121, 117, 123 };
        undo_changeModel(lastUndoRemoved);
        if (lastUndoRemoved <= 106)
        {
            HOIST_STEP1.SetActive(false);
        }
        //แก้แล้ว 25/11
        else if (lastUndoRemoved <= 118)
        {
            HOIST_STEP3.SetActive(false);
        }


        if (cameraSwitcher != null)
        {
            if (isActive)
            {
                cameraSwitcher.SwitchToCamera(0);
            }
            else
            {
                cameraSwitcher.SwitchToCamera(cameraIndex);
            }
        }
        else
        {
            Debug.LogError("CameraSwitcher is not assigned");
        }


        foreach (GameObject obj in groupObjects)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(triggerName);
            }
        }
        yield return new WaitForSeconds(delay);
        if (NotHaveTools.Contains(lastUndoRemoved))
        {
            //แก้แล้ว 25/11
            if (lastUndoRemoved == 117)
            {
                foreach (GameObject obj in groupObjects)
                {
                    if (obj.name == "HOIST")
                    {
                        obj.SetActive(false);
                    }
                }
            }
            undo_changeModel(lastUndoRemoved);
            isProcessing = false;
            loading_undo.SetActive(false);
        }

    }
    public void AddUndo(int value)
    {
        // ถ้าขนาดของ undoHistory มากกว่า 11 (มี 12 ค่าแล้ว)
        if (undoHistory.Count >= 36)
        {
            // ลบค่าที่เก่าที่สุดออก (ตำแหน่งแรก)
            undoHistory.RemoveAt(0);
        }

        // เพิ่มข้อมูลใหม่ที่ตำแหน่งท้ายสุด
        undoHistory.Add(value);
    }
    public int GetLastUndoAndRemove()
    {
        if (undoHistory.Count > 0)
        {
            int lastValue = undoHistory[undoHistory.Count - 1]; // ดูค่าสุดท้าย
            undoHistory.RemoveAt(undoHistory.Count - 1); // ลบค่าสุดท้ายออก
            return lastValue;
        }
        return -1; // ถ้าไม่มีค่าใน list
    }

    // ฟังก์ชันเพื่อดูค่าสุดท้ายโดยไม่ลบ
    public int PeekLastUndo()
    {
        if (undoHistory.Count > 0)
        {
            return undoHistory[undoHistory.Count - 1]; // ดูค่าสุดท้ายโดยไม่ลบ
        }
        return -1; // ถ้าไม่มีค่าใน list
    }

    private void changeModel(int groupNumber)
    {
        //แก้แล้ว 25/11
        if (groupNumber == 112)
        {
            foreach (GameObject obj in stepDictionary[groupNumber])
            {
                obj.SetActive(false);
            }
            //แก้แล้ว 25/11
            foreach (GameObject obj in stepDictionary[117])
            {
                if (obj.name != "HOIST") { obj.SetActive(true); }

            }
        }
    }
    private void undo_changeModel(int lastnum)
    {
        //แก้แล้ว 25/11
        if (lastnum == 112)
        {
            foreach (GameObject obj in stepDictionary[lastnum])
            {

                obj.SetActive(true);
            }
            //แก้แล้ว 25/11
            foreach (GameObject obj in stepDictionary[117])
            {
                obj.SetActive(false);
            }
        }
        //แก้แล้ว 25/11
        else if (lastnum == 117)
        {
            foreach (GameObject obj in stepDictionary[lastnum])
            {
                if (obj.name != "HOIST")
                {
                    obj.SetActive(true);
                }

            }
            //แก้แล้ว 25/11
            foreach (GameObject obj in stepDictionary[112])
            {
                obj.SetActive(false);
            }
        }
    }
}
