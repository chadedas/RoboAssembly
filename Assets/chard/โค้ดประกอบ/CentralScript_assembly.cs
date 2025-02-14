using UnityEngine;
using System.Collections;
using Gaskellgames.CameraController;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.Animations;
using UnityEngine.UI;

public class CentralScript_assembly : MonoBehaviour
{
    public static CentralScript_assembly Instance;
    private int currentStep = 123; //1
    private int stack1 = 36; //0
    private int stack2 = 19; //0
    private int stack3 = 23; //0
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
    [SerializeField] public Material winwolk;
    [SerializeField] public Material ORANGE;
    [SerializeField] public Material BLACK;
    [SerializeField] public Material METAL;
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
        Renderer renderer = GetComponent<Renderer>();
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
        for (int i = 1; i <= 12; i++)
        {
            foreach (GameObject obj in stepDictionary[i])
            {
                obj.SetActive(false);
            }
        }
        for (int i = 100; i <= 123; i++)
        {
            foreach (GameObject obj in stepDictionary[i])
            {
                obj.SetActive(false);
            }
        }
        ShowObjectNextStep(currentStep);



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
        {//แก้แล้ว
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
            Debug.LogError("currentStep" + currentStep);
            if (receivedButtonNumber == 0 && CONNECTER_MOTOR1.Contains(groupNumber) ||
                 CONNECTER_MOTOR2.Contains(groupNumber) ||
                 CONNECTER_MOTOR3.Contains(groupNumber))
            {
                if (currentStep == 99 && stack2 == sumOfConnecterMotor2 && stack1 == sumOfConnecterMotor1 && stack3 != 0)
                {
                    if (CONNECTER_MOTOR3.Contains(groupNumber))
                    {
                        if (receivedButtonNumber == 0)
                        {
                            AboutInformation(groupNumber);
                            float delayAnimationTrigger = 3f;
                            AddUndo(groupNumber);
                            stack3 -= groupNumber;
                            Debug.LogError(stack3);
                            if (stack3 == 0)
                            {
                                cameraIndex = 2;
                            }
                            else
                            {
                                cameraIndex = 3;
                            }

                            if (cameraIndex > 50)
                            {
                                cameraIndex = 0;
                            }
                            StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
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
                else if (currentStep == 99 && stack1 == sumOfConnecterMotor1 && stack3 == 0 && stack2 != 0)
                {
                    if (CONNECTER_MOTOR2.Contains(groupNumber))
                    {
                        if (receivedButtonNumber == 0)
                        {
                            AboutInformation(groupNumber);
                            float delayAnimationTrigger = 3f;
                            AddUndo(groupNumber);
                            stack2 -= groupNumber;
                            if (stack2 == 0)
                            {
                                cameraIndex = 1;
                            }
                            else
                            {
                                cameraIndex = 2;
                            }

                            if (cameraIndex > 50)
                            {
                                cameraIndex = 0;
                            }
                            StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
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
                else if (currentStep == 99 && stack2 == 0 && stack3 == 0 && stack1 != 0)
                {
                    if (CONNECTER_MOTOR1.Contains(groupNumber))
                    {
                        if (receivedButtonNumber == 0)
                        {
                            AboutInformation(groupNumber);
                            float delayAnimationTrigger = 3f;
                            AddUndo(groupNumber);
                            stack1 -= groupNumber;
                            if (stack1 == 0)
                            {
                                cameraIndex = 0;
                            }
                            else
                            {
                                cameraIndex = 1;
                            }

                            if (cameraIndex > 50)
                            {
                                cameraIndex = 0;
                            }
                            StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
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
                    AboutInformation(groupNumber);
                    float ToolsdelayAnimation = 16f;
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    if (groupNumber == 106)
                    {
                        HOIST_STEP1.SetActive(false);
                    }
                    //แก้แล้ว
                    else if (groupNumber == 118)
                    {
                        HOIST_STEP3.SetActive(false);
                    }
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                    StartCoroutine(Assembly_ShowTools(stepToolsDictionary[groupNumber], delayAnimationTrigger));
                    StartCoroutine(Assembly_ToolAnimation(groupNumber, stepToolsDictionary[groupNumber], delayAnimationTrigger, ToolsdelayAnimation, cameraIndex));

                }
                else if (MOTOR.Contains(groupNumber) && receivedButtonNumber == 0)
                {

                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                if (AXIS456.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance1.Contains(groupNumber) && receivedButtonNumber == 2)
                {
                    AboutInformation(groupNumber);
                    float ToolsdelayAnimation = 4f;
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                    StartCoroutine(Assembly_ShowTools(stepToolsDictionary[groupNumber], delayAnimationTrigger));
                    StartCoroutine(Assembly_ToolAnimation(groupNumber, stepToolsDictionary[groupNumber], delayAnimationTrigger, ToolsdelayAnimation, cameraIndex));
                }
                else if (CounterBalance2.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance3.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalance4.Contains(groupNumber) && receivedButtonNumber == 9)
                {
                    AboutInformation(groupNumber);
                    float ToolsdelayAnimation = 4f;
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                    StartCoroutine(Assembly_ShowTools(stepToolsDictionary[groupNumber], delayAnimationTrigger));
                    StartCoroutine(Assembly_ToolAnimation(groupNumber, stepToolsDictionary[groupNumber], delayAnimationTrigger, ToolsdelayAnimation, cameraIndex));
                }
                else if (CounterBalanceALL_step1.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                else if (CounterBalanceALL_step2.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }
                else if (AXIS2.Contains(groupNumber) && receivedButtonNumber == 0)
                {
                    AboutInformation(groupNumber);
                    float delayAnimationTrigger = 7f;
                    AddUndo(groupNumber);
                    //StartCoroutine(delayAnimation(stepDictionary[groupNumber], ToolsdelayAnimation));
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                }

                else if (NOTAXIS456.Contains(groupNumber) && receivedButtonNumber == 1)
                {
                    AboutInformation(groupNumber);
                    float ToolsdelayAnimation = 34f;
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                    StartCoroutine(Assembly_ShowTools(stepToolsDictionary[groupNumber], delayAnimationTrigger));
                    StartCoroutine(Assembly_ToolAnimation(groupNumber, stepToolsDictionary[groupNumber], delayAnimationTrigger, ToolsdelayAnimation, cameraIndex));
                }
                else if (NOTNEW.Contains(groupNumber) && receivedButtonNumber == 1)
                {
                    AboutInformation(groupNumber);
                    AboutInformation(groupNumber);
                    float ToolsdelayAnimation = 2.5f;
                    float delayAnimationTrigger = 5f;
                    AddUndo(groupNumber);
                    int cameraIndex = ((groupNumber - 1) - 96);
                    if (cameraIndex > 50) cameraIndex = 0;
                    StartCoroutine(Assembly_TriggerAnimationForGroup(groupNumber, stepDictionary[groupNumber], "A3", delayAnimationTrigger, cameraIndex));
                    StartCoroutine(Assembly_ShowTools(stepToolsDictionary[groupNumber], delayAnimationTrigger));
                    StartCoroutine(Assembly_ToolAnimation(groupNumber, stepToolsDictionary[groupNumber], delayAnimationTrigger, ToolsdelayAnimation, cameraIndex));

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
            //loading.SetActive(true);
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
    private IEnumerator Assembly_ToolAnimation(int groupNumber, GameObject[] toolObject, float delayAnimation, float delayTools, int cameraIndex)
    {
        loading.SetActive(true);
        isProcessing = true;
        yield return new WaitForSeconds(delayAnimation);
        foreach (GameObject obj in toolObject)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("A3");
            }
        }
        yield return new WaitForSeconds(delayTools);
        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(false);
        }
        if (groupNumber >= 100)
        {
            if (cameraSwitcher != null)
            {
                if (groupNumber >= 100)
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
                else if (groupNumber == 99)
                {
                    cameraSwitcher.SwitchToCamera(3);
                }

            }
                         if (groupNumber == 118)
                {
                    foreach (GameObject obj in stepDictionary[121])
                    {
                        if (obj.name == "HOIST")
                        {
                            obj.SetActive(false);
                        }
                    }
                }

            currentStep--;
            ShowObjectNextStep(groupNumber);

            isProcessing = false;
            loading.SetActive(false);
        }
    }
    private IEnumerator Assembly_ShowTools(GameObject[] toolObject, float delay)
    {
        loading.SetActive(true);
        isProcessing = true;
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in toolObject)
        {
            obj.SetActive(true);
        }

    }

    private IEnumerator Assembly_TriggerAnimationForGroup(int groupNumber, GameObject[] groupObjects, string triggerName, float delay, int cameraIndex)
    {
        loading.SetActive(true);
        isProcessing = true;
        //แก้แล้ว
        HashSet<int> NotHaveTools = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 101, 103, 105, 107, 109, 119, 111, 113, 112, 121, 117, 123 };
        if (groupNumber <= 106 || groupNumber > 109) { HOIST_STEP1.SetActive(false); }
        //แก้แล้ว
        if (groupNumber <= 118 || groupNumber > 121) { HOIST_STEP3.SetActive(false); }
        foreach (GameObject obj in groupObjects)
        {
            Renderer objectRenderer = obj.GetComponent<Renderer>();
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                if (objectRenderer != null && ORANGE != null && BLACK != null && METAL != null && obj.name != "HOIST")
                {
                    if (obj.name == "BLACK")
                    {
                        objectRenderer.material = BLACK;
                    }
                    else if (obj.name == "ORANGE")
                    {
                        objectRenderer.material = ORANGE;
                    }
                    else if (obj.name == "METAL")
                    {
                        objectRenderer.material = METAL;
                    }
                }
                animator.SetTrigger(triggerName);
            }
        }
        yield return new WaitForSeconds(delay);


        if (NotHaveTools.Contains(groupNumber))
        {
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
                if (groupNumber < 100)
                {

                }
                else
                {

                    currentStep--;
                }
                //แก้แล้ว
               
                if (groupNumber == 117)
                {
                    foreach (GameObject obj in stepDictionary[groupNumber])
                    {
                        if (obj.name == "HOIST")
                        {
                            obj.SetActive(false);
                        }
                    }
                }


                ShowObjectNextStep(groupNumber);

                isProcessing = false;
                loading.SetActive(false);
            }


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
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่คอนเนคเตอร์มอเตอร์ ต้องหมุนที่ปลายหัวคอนเนคเตอร์ ในทิศทางตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 100)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 4";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 4 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 101)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 4";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 4 ต้องใช้ความระมัดระวังในการใส่ เพื่อไม่ให้เพลาทางด้านในเกิดความเสียหาย";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 102)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 5";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 5 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 103)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 5";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 5 ต้องใส่อย่างระมัดระวังเพื่อไม่ให้เพลาทางด้านในเสียหาย";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 104)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 6";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 6 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 105)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 6";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 6 ต้องใส่อย่างระมัดระวังเพื่อไม่ให้เพลาทางด้านในเสียหาย";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 106)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 3";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M8";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 3 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M8 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 107)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 3";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 3 ต้องใส่ด้วยความระมัดระวังและต้องใช้รอกคอยประคองแขนกลอุตสาหกรรม";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 108)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตแขนส่วนบน";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10 , M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตแขนส่วนบน ต้องใช้รอกคอยประคองให้แขนติดกับตำแหน่ง";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 109)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "แขนส่วนบน";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ , รอก";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่แขนส่วนบน ต้องใช้รอกช่วยยกเคาเตอร์บาลานซ์เพื่อยกขึ้นไปยังตำแหน่งที่ต้องการ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 118)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 2 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M10 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 119)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 2 ต้องใส่อย่างระมัดระวังพร้อมใช้งาน";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 110)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "สลักเคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อค";
            InformationTools1_Size.GetComponent<Text>().text = "M19";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่สลักเคาเตอร์บาลานซ์ ต้องใช้ลูกบล็อค 6 เหลี่ยม ขนาด M19 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรม";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 111)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "สลักเคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่สลักเคาเตอร์บาลานซ์ ต้องใช้มือ หรือค้อนยาง ตอกให้เข้าล็อคเข้าที่";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 113)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "ฝาปิด";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่ฝาปิด สามารถใช้มือดันเข้าไปในรู ให้ตรงล็อคได้เลย หรืออาจใช้ค้อนยางตอกเพื่อให้แน่นขึ้น";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 114)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "โอลิง";
            InformationTools1_WhatTools.GetComponent<Text>().text = "คีมถ่างแหวน";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่โอลิง ต้องใช้คีมถ่างแหวน ถ่างแหวนเพื่อนำแหวนเข้าไปตรงล็อคเคาเตอร์บาลานซ์";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 115)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "โอลิง";
            InformationTools1_WhatTools.GetComponent<Text>().text = "คีมถ่างแหวน";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่โอลิง ต้องใช้คีมถ่างแหวน ถ่างแหวนเพื่อนำแหวนเข้าไปตรงล็อคเคาเตอร์บาลานซ์";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 116)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตใต้เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตใต้เคาเตอร์บาลานซ์ ต้องใช้ลูกบล็อคเดือยโผล่ขนาด M14 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 112)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ , รอก";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "ยกเคาเตอร์บาลานซ์ให้อยู่ตำแหน่งที่ตรงล็อคของแขนแกน 2 และพอดีกับ 3เหลี่ยมตัวยึดเคาเตอร์บาลานซ์";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 120)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตเกียร์แกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M14";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตเกียร์แกน 2 ต้องใช้ลูกบล็อคเดือยโผล่ขนาด M14 และต้องใช้รอกคอยประคองแขนกลอุตสาหกรรมด้วย";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 121)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "แขนแกน 2";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ , รอก";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่แขนแกน 2 ต้องใช้รอกคอยประคองแขนกลอุตสาหกรรม และไปตำแหน่งที่ต้องการ";
            ErrorInformation1.SetActive(true);
        }
        //แก้แล้ว
        else if (groupnumber == 117)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "เคาเตอร์บาลานซ์";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ , รอก";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่เคาเตอร์บาลานซ์ ต้องใช้รอกประคองมาไว้ตำแหน่งที่ต้องการ เนื่องจาก เคาเตอร์บาลานซ์มีน้ำหนักเยอะ";
            ErrorInformation1.SetActive(true);
        }
        else if (groupnumber == 122)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "น็อตมอเตอร์แกน 1";
            InformationTools1_WhatTools.GetComponent<Text>().text = "ลูกบล็อคเดือยโผล่";
            InformationTools1_Size.GetComponent<Text>().text = "M10";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่น็อตมอเตอร์แกน 1 ต้องใช้ชุดบล็อคเดือยโผล่ ขนาด M10 หมุนตามเข็มนาฬิกา";
            ErrorInformation1.SetActive(false);
        }
        else if (groupnumber == 123)
        {
            InformationTools1_WhatAssembly.GetComponent<Text>().text = "มอเตอร์แกน 1";
            InformationTools1_WhatTools.GetComponent<Text>().text = "มือ";
            InformationTools1_Size.GetComponent<Text>().text = "-";
            InformationTools2_Howdo.GetComponent<Text>().text = "การใส่มอเตอร์แกน 1 ต้องใช้ความระมัดระวังในการใส่และมั่นใจว่าเชื่อมต่อได้อย่างถูกต้อง";
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


    public void ShowObjectNextStep(int groupNumber)
    {
        groupNumber -= 1;
        if (currentStep >= 100)
        {
            foreach (GameObject targetObject in stepDictionary[currentStep])
            {
                undo_changeModel(currentStep);
                targetObject.SetActive(true); // เปิดใช้งานวัตถุ
                Renderer objectRenderer = targetObject.GetComponent<Renderer>(); // รับ Renderer ของวัตถุ

                if (objectRenderer != null && winwolk != null && targetObject.name != "HOIST" && targetObject.name != "SKIP")
                {
                    objectRenderer.material = winwolk; // เปลี่ยนวัสดุของวัตถุ
                }
                else
                {
                    Debug.LogError("Renderer or newMaterial is not assigned for " + targetObject.name);
                }
            }
        }
        else if (currentStep == 99 && stack1 == 36 && stack2 == 19 && stack3 == 23)
        {
            for (int i = 11; i <= 12; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(true);
                    Renderer objectRenderer = targetObject.GetComponent<Renderer>();
                    if (objectRenderer != null && winwolk != null && targetObject.name != "HOIST" && targetObject.name != "SKIP")
                    {
                        objectRenderer.material = winwolk;
                    }
                }
            }

        }
        else if (currentStep == 99 && stack3 == 0 && stack2 == 19 && stack1 == 36)
        {
            for (int i = 9; i <= 10; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(true);
                    Renderer objectRenderer = targetObject.GetComponent<Renderer>();
                    if (objectRenderer != null && winwolk != null && targetObject.name != "HOIST" && targetObject.name != "SKIP")
                    {
                        objectRenderer.material = winwolk;
                    }
                }
            }

        }
        else if (currentStep == 99 && stack2 == 0 && stack3 == 0 && stack1 == 36)
        {
            for (int i = 1; i <= 8; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(true);
                    Renderer objectRenderer = targetObject.GetComponent<Renderer>();
                    if (objectRenderer != null && winwolk != null && targetObject.name != "HOIST" && targetObject.name != "SKIP")
                    {
                        objectRenderer.material = winwolk;
                    }
                }
            }

        }

    }

    public void undo_ShowObjectNextStep(int groupNumber)
    {
        Debug.LogError("groupNumber : " + groupNumber);
        if (groupNumber == 106) { HOIST_STEP1.SetActive(true); }
        //แก้แล้ว
        else if (groupNumber == 118) {

             HOIST_STEP3.SetActive(true); 
             foreach (GameObject obj in stepDictionary[121]){
                if(obj.name == "HOIST"){
                        obj.SetActive(true);
                }
             }
             }
        foreach (GameObject targetObject in stepDictionary[groupNumber])
        {
            targetObject.SetActive(true); // เปิดใช้งานวัตถุ
            Renderer objectRenderer = targetObject.GetComponent<Renderer>(); // รับ Renderer ของวัตถุ

            if (objectRenderer != null && winwolk != null && targetObject.name != "HOIST" && targetObject.name != "SKIP")
            {
                objectRenderer.material = winwolk; // เปลี่ยนวัสดุของวัตถุ
            }
            else
            {
                Debug.LogError("Renderer or newMaterial is not assigned for " + targetObject.name);
            }
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
                    cameraIndex = 1;
                    currentStep = 99;
                    stack1 += lastUndoRemoved;
                    StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                }
                else if (CONNECTER_MOTOR2.Contains(lastUndoRemoved))
                {
                    cameraIndex = 2;
                    currentStep = 99;
                    stack2 += lastUndoRemoved;
                    StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));

                }
                else if (CONNECTER_MOTOR3.Contains(lastUndoRemoved))
                {
                    cameraIndex = 3;
                    currentStep = 99;
                    stack3 += lastUndoRemoved;
                    StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));

                }
                else if (lastUndoRemoved >= 100)
                {

                    if (NOT_M10.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (MOTOR.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (NOTNEW.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (NOTAXIS456.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (AXIS456.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalance1.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalance2.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalance3.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalance4.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalanceALL_step1.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (CounterBalanceALL_step2.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                    else if (AXIS2.Contains(lastUndoRemoved))
                    {
                        cameraIndex = lastUndoRemoved - 96;
                        currentStep += 1;
                        StartCoroutine(UndostepFinal(stepDictionary[lastUndoRemoved], lastUndoRemoved, cameraIndex));
                    }
                }
            }
        }
        // On Step 1 Connecter Motor 2
    }

    private IEnumerator UndostepFinal(GameObject[] gameObjects, int groupNumber, int cameraIndex)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
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

        undo_NotShowObjectNextStep(groupNumber);
        undo_ShowObjectNextStep(groupNumber);
        undo_changeModel(groupNumber);
        yield return new WaitForSeconds(2f);
        isProcessing = false;
        loading_undo.SetActive(false);
    }


    public void undo_NotShowObjectNextStep(int undoNum)
    {

        int sumUndoNum = undoNum - 1;
        if (sumUndoNum <= 106 || sumUndoNum > 109) { HOIST_STEP1.SetActive(false); }
        //แก้แล้ว
        if (sumUndoNum < 117 || sumUndoNum > 121) { HOIST_STEP3.SetActive(false); }
        if (sumUndoNum >= 100)
        {
            foreach (GameObject targetObject in stepDictionary[sumUndoNum])
            {
                if (targetObject.name != "HOIST")
                {
                    //แก้แล้ว
                    if (sumUndoNum == 112)
                    {
                        Renderer objectRenderer = targetObject.GetComponent<Renderer>();
                        if (objectRenderer != null && ORANGE != null && BLACK != null && METAL != null && targetObject.name != "HOIST")
                        {
                            if (targetObject.name == "BLACK")
                            {
                                //แก้แล้ว
                                objectRenderer.material = BLACK;
                                undo_changeModel(117);
                            }
                            else if (targetObject.name == "ORANGE")
                            {
                                objectRenderer.material = ORANGE;
                            }
                            else if (targetObject.name == "METAL")
                            {
                                objectRenderer.material = METAL;
                            }
                        }
                    }
                    else
                    {
                        targetObject.SetActive(false);
                    }

                }

            }
        }
        //ถ้าถอด 3 อยู่ แล้ว
        if (undoNum == 100)
        {
            for (int i = 11; i <= 12; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(false);
                }
            }
        }
        else if (undoNum == 12 || undoNum == 11)
        {
            for (int i = 9; i <= 10; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(false);
                }
            }
        }
        else if (undoNum == 9 || undoNum == 10)
        {
            for (int i = 1; i <= 8; i++)
            {
                foreach (GameObject targetObject in stepDictionary[i])
                {
                    targetObject.SetActive(false);
                }
            }
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
        //แก้แล้ว
        if (groupNumber == 117)
        {
            foreach (GameObject obj in stepDictionary[groupNumber])
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in stepDictionary[112])
            {
                if (obj.name != "HOIST") { obj.SetActive(true); }

            }
        }
    }
    private void undo_changeModel(int lastnum)
    {
        //แก้แล้ว
        if (lastnum == 112)
        {
            foreach (GameObject obj in stepDictionary[lastnum])
            {

                obj.SetActive(true);
            }
            foreach (GameObject obj in stepDictionary[117])
            {
                obj.SetActive(false);
            }
        }
        else if (lastnum == 117)
        {
            //แก้แล้ว
            foreach (GameObject obj in stepDictionary[lastnum])
            {
                if (obj.name != "HOIST")
                {
                    obj.SetActive(true);
                }

            }
            foreach (GameObject obj in stepDictionary[112])
            {
                obj.SetActive(false);
            }
        }
    }
}
