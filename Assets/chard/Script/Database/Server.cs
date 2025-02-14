using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class Server : MonoBehaviour
{

    [SerializeField] GameObject welcomePanel;
    [SerializeField] Text user;
    [SerializeField] GameObject loginPanel;
    [SerializeField] Text firstNameText; // New field for first name
    [SerializeField] Text dateText;  // New field for date time
    [Space]
    [SerializeField] InputField username;
    [SerializeField] InputField password;

    [SerializeField] Text errorMessages;
    [SerializeField] GameObject progressCircle;

    [SerializeField] Button loginButton;
    [SerializeField] Button logoutButton;
    [SerializeField] Button requestButton;
    [SerializeField] Button revokeButton;
    [SerializeField] Button closeButton_ErrorLogin;
    [SerializeField] Button closeButton_ErrorAdmin;
    [SerializeField] GameObject accessAddSuccess;
    [SerializeField] GameObject accessRemoveSuccess;
    [SerializeField] GameObject accessNotmatch;
    [SerializeField] GameObject panelErrorLogin;
    [SerializeField] GameObject panelErrorAdmin;
    [SerializeField] Text internet;
    [SerializeField] Text userAccess;
    [SerializeField] Text error_admin;
    private float nextCheckTime = 0f;  // ตัวแปรสำหรับเก็บเวลาที่จะตรวจสอบครั้งถัดไป
    private float checkInterval = 3f;  // ตั้งเวลาตรวจสอบทุก 3 วินาที
    private const string TestUrl = "http://www.google.com";
    private string hwid;
    // ฟังก์ชันที่ใช้ส่งข้อมูลไปยัง API
    public void SendHWIDResetRequest(string username)
    {
        StartCoroutine(SendRequest(username));
    }

    // คำขอที่ส่งไปยัง API
    private IEnumerator SendRequest(string username)
    {
        string url = "https://manage.np-robotics.com/api/resetHWID/index.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("key", "dkh7COrSPT29axm");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                string filePath = Path.Combine(Application.streamingAssetsPath, "data", "hwid.dat");

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        Debug.Log("ไฟล์ hwid.dat ถูกลบสำเร็จ");
                        accessAddSuccess.SetActive(false);
                accessRemoveSuccess.SetActive(true);
                accessNotmatch.SetActive(false);

                revokeButton.gameObject.SetActive(false);
                requestButton.gameObject.SetActive(true);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Debug.LogError("ไม่มีสิทธิ์เข้าถึงไฟล์: " + ex.Message);
                        panelErrorAdmin.gameObject.SetActive(true);
                        error_admin.text = "ไม่มีสิทธิ์เข้าถึงไฟล์ โปรดเปิดโปรแกรมด้วยวิธี Run as Administrator";
                    }
                    catch (IOException ex)
                    {
                        Debug.LogError("ไม่สามารถลบไฟล์ได้ อาจมีการใช้งานไฟล์อยู่: " + ex.Message);
                        panelErrorAdmin.gameObject.SetActive(true);
                        error_admin.text = "ไม่สามารถลบไฟล์ได้ อาจมีการใช้งานไฟล์อยู่: " + ex.Message;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("เกิดข้อผิดพลาดขณะลบไฟล์: " + ex.Message);
                        panelErrorAdmin.gameObject.SetActive(true);
                        error_admin.text = "เกิดข้อผิดพลาดขณะลบไฟล์: " + ex.Message;
                    }
                }
                else
                {
                    Debug.LogWarning("ไฟล์ hwid.dat ไม่พบ");
                }
                
            }
            else
            {
                Debug.LogError("Error sending request: " + www.error);
            }
        }
    }
    public void SendHWIDToServer(string username)
    {
        StartCoroutine(SendHWID(username));
    }

    private IEnumerator SendHWID(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("hwid", EncryptHWID(hwid));
        form.AddField("username", username);

        Debug.Log("Sending HWID: " + EncryptHWID(hwid));  // ตรวจสอบค่า HWID
        Debug.Log("Sending username: " + username);  // ตรวจสอบค่า username

        using (UnityWebRequest www = UnityWebRequest.Post("https://manage.np-robotics.com/api/hwid/index.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("HWID sent successfully: " + www.downloadHandler.text);
                Debug.Log("HWID has been successfully sent.");
                string filePath = Path.Combine(Application.streamingAssetsPath, "data", "hwid.dat");
                string directoryPath = Path.Combine(Application.streamingAssetsPath, "data");

                try
                {
                    // ตรวจสอบและสร้างไดเรกทอรีหากยังไม่มี
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                        Debug.Log("สร้างโฟลเดอร์ data สำเร็จ");
                    }

                    // เขียนไฟล์ hwid.dat
                    File.WriteAllText(filePath, EncryptHWID(hwid));
                    Debug.Log("ไฟล์ hwid.dat ถูกสร้างขึ้นแล้ว");

                    // ตรวจสอบว่าการสร้างไฟล์สำเร็จ
                    if (File.Exists(filePath))
                    {
                        Debug.Log("ตรวจสอบสำเร็จ: ไฟล์ hwid.dat ถูกสร้างและสามารถเข้าถึงได้");
                        accessAddSuccess.SetActive(true);
                accessRemoveSuccess.SetActive(false);
                accessNotmatch.SetActive(false);

                revokeButton.gameObject.SetActive(true);
                requestButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning("ไฟล์ hwid.dat ไม่พบหลังจากสร้าง");
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.LogError("ไม่มีสิทธิ์เข้าถึงโฟลเดอร์หรือไฟล์: " + ex.Message);
                    panelErrorAdmin.gameObject.SetActive(true);
                    error_admin.text = "ไม่มีสิทธิ์เข้าถึงไฟล์ โปรดเปิดโปรแกรมด้วยวิธี Run as Administrator";
                }
                catch (IOException ex)
                {
                    Debug.LogError("ข้อผิดพลาดในการสร้างไฟล์หรือโฟลเดอร์: " + ex.Message);
                    panelErrorAdmin.gameObject.SetActive(true);
                    error_admin.text = "ข้อผิดพลาดในการสร้างไฟล์หรือโฟลเดอร์: " + ex.Message;
                }
                catch (Exception ex)
                {
                    Debug.LogError("เกิดข้อผิดพลาดที่ไม่คาดคิด: " + ex.Message);
                    panelErrorAdmin.gameObject.SetActive(true);
                    error_admin.text = "เกิดข้อผิดพลาดที่ไม่คาดคิด: " + ex.Message;
                }

                
            }
            else
            {
                Debug.LogError("Error sending HWID: " + www.error);
            }
        }
    }

    private string EncryptHWID(string hwid)
    {
        if (string.IsNullOrEmpty(hwid))
        {
            Debug.LogError("HWID is null or empty.");
            return string.Empty; // หรือส่งค่ากลับที่เหมาะสมหากต้องการใช้ค่าเริ่มต้น
        }

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hwid);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
    void Start()
    {
        hwid = SystemInfo.deviceUniqueIdentifier;
        hwid += "64nUH&EzLk^eq@Q}.iMnTU'9IEDX2PS8T4]!XBv";
        Debug.Log("hwid:" + hwid);
        requestButton.onClick.AddListener(OnRequestButtonClicked);
        revokeButton.onClick.AddListener(OnRevokeButtonClicked);
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        logoutButton.onClick.AddListener(OnLogoutButtonClicked);
        closeButton_ErrorLogin.onClick.AddListener(closeButton_ErrorLoginButtonClicked);
        closeButton_ErrorAdmin.onClick.AddListener(closeButton_ErrorAdminButtonClicked);

        CheckInternetConnection();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.isFocused)
            {
                password.Select();
            }
            else if (password.isFocused)
            {
                username.Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (loginButton.interactable)
            {
                OnLoginButtonClicked();
            }
        }
        // ตรวจสอบว่าเวลาปัจจุบันมากกว่าหรือเท่ากับเวลาที่ตั้งไว้ในการตรวจสอบครั้งถัดไป
        if (Time.time >= nextCheckTime)
        {
            // เรียกใช้งานฟังก์ชันในการตรวจสอบอินเทอร์เน็ต
            CheckInternetConnectionAsync();

            // อัปเดตเวลาถัดไปที่จะทำการตรวจสอบ
            nextCheckTime = Time.time + checkInterval;
        }
    }
    private async void CheckInternetConnectionAsync()
    {
        bool isConnected = await IsInternetAvailableAsync();

        // คุณสามารถใช้ isConnected เพื่อจัดการกับสถานะการเชื่อมต่ออินเทอร์เน็ตเพิ่มเติมได้
    }


    public void OnLoginButtonClicked()
    {
        loginButton.interactable = false;
        progressCircle.SetActive(true);
        StartCoroutine(Login());
    }

    public void OnRequestButtonClicked()
    {
        SendHWIDToServer(user.text);
    }

    public void OnRevokeButtonClicked()
    {
        SendHWIDResetRequest(user.text);
    }

    private async Task<bool> IsInternetAvailableAsync()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("ไม่มีการเชื่อมต่อเครือข่าย");
            internet.text = "ไม่มีการเชื่อมต่อเครือข่าย";
            internet.color = Color.red;
            return false;
        }

        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(5); // กำหนด Timeout
                var response = await httpClient.GetAsync(TestUrl);
                if (response.IsSuccessStatusCode)
                {
                    Debug.Log("เชื่อมต่ออินเทอร์เน็ตได้!");
                    internet.text = "เชื่อมต่ออินเทอร์เน็ตได้!";
                    internet.color = Color.green;
                    return true;
                }
            }
        }
        catch
        {
            // หากเกิดข้อผิดพลาด เช่น เซิร์ฟเวอร์ไม่ตอบสนอง
        }

        Debug.Log("มีเครือข่าย แต่ไม่สามารถเข้าถึงอินเทอร์เน็ตได้");
        internet.text = "มีเครือข่าย แต่ไม่สามารถเข้าถึงอินเทอร์เน็ตได้";
        internet.color = Color.red;
        return false;
    }

    public void closeButton_ErrorLoginButtonClicked()
    {
        panelErrorLogin.gameObject.SetActive(false);
    }

    public void closeButton_ErrorAdminButtonClicked()
    {
        panelErrorAdmin.gameObject.SetActive(false);
    }

    public void OnLogoutButtonClicked()
    {
        SceneManager.LoadScene("UpdatePatch");
    }

    IEnumerator Login()
    {
        Dictionary<string, string> formData = new();
        formData["username"] = username.text;
        formData["password"] = password.text;


        using UnityWebRequest www = UnityWebRequest.Post("https://manage.np-robotics.com/api/index.php", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("Response: " + response);

            // ถ้าผลลัพธ์เริ่มต้นด้วย "ECHO DATABASE - "
            if (response.StartsWith("ECHO DATABASE - "))
            {
                // ตรวจสอบ HWID
                Dictionary<string, string> formData_hwidMYSQL = new();
                formData_hwidMYSQL["hwid"] = EncryptHWID(hwid);

                using UnityWebRequest www_mysql = UnityWebRequest.Post("https://manage.np-robotics.com/api/checkhwidMYSQL/index.php", formData_hwidMYSQL);
                yield return www_mysql.SendWebRequest();

                if (www_mysql.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + www_mysql.error);
                }
                else
                {
                    string[] data = response.Split(new[] { " - " }, StringSplitOptions.None);

                    if (data.Length >= 4)
                    {
                        user.text = data[1]; // Username
                        dateText.text = data[3]; // วันที่เพิ่มในฐานข้อมูล
                    }
                    string response_mysql = www_mysql.downloadHandler.text;
                    Debug.Log("Response from HWID check: " + response_mysql);

                    // ตรวจสอบว่า response_mysql เริ่มต้นด้วย "HWIDuser="
                    if (response_mysql.StartsWith("HWIDuser="))
                    {
                        string usernameFromApi = response_mysql.Substring("HWIDuser=".Length); // ดึง username จาก response_mysql
                        if (usernameFromApi != data[1])
                        {
                            Debug.Log("คุณได้ยืนยันสิทธิ์ที่ยูสเซอร์ " + usernameFromApi + " แล้ว โปรดลบสิทธิ์ก่อน");
                            panelErrorLogin.gameObject.SetActive(true);
                            errorMessages.text = "คุณได้ยืนยันสิทธิ์ที่ยูสเซอร์ " + usernameFromApi + " แล้ว โปรดลบสิทธิ์ก่อน";
                            userAccess.text = usernameFromApi;
                        }
                        else if (usernameFromApi == data[1])
                        {

                            if (data.Length >= 4)
                            {

                                if (string.IsNullOrEmpty(data[2]))
                                {
                                    Debug.Log("hwid ยังไม่มีในฐานข้อมูล");
                                    firstNameText.text = "ยังไม่มีในฐานข้อมูล";
                                    requestButton.gameObject.SetActive(true);
                                    revokeButton.gameObject.SetActive(false);
                                    loginPanel.gameObject.SetActive(false);
                                    welcomePanel.gameObject.SetActive(true);
                                    // ขอสิทธิ์คอมพิวเตอร์เครื่องนี้
                                    // logout
                                }
                                else
                                {
                                    if (data[2] == EncryptHWID(hwid))
                                    {
                                        Debug.Log("hwid มีในฐานข้อมูล และตรงกัน");
                                        firstNameText.text = "ตรงกัน";
                                        revokeButton.gameObject.SetActive(true);
                                        requestButton.gameObject.SetActive(false);
                                        loginPanel.gameObject.SetActive(false);
                                        welcomePanel.gameObject.SetActive(true);
                                        // ลบสิทธิ์คอมพิวเตอร์เครื่องนี้
                                        // logout
                                    }
                                    else
                                    {
                                        Debug.Log("hwid มีในฐานข้อมูล และไม่ตรงกัน");
                                        firstNameText.text = "ไม่ตรงกัน";
                                        welcomePanel.gameObject.SetActive(true);
                                        loginPanel.gameObject.SetActive(false);
                                        revokeButton.gameObject.SetActive(false);
                                        requestButton.gameObject.SetActive(false);
                                        // แจ้งเตือนต้องลบสิทธิ์เครื่องเก่าก่อนจึงจะขอสิทธิ์ได้
                                        accessAddSuccess.gameObject.SetActive(false);
                                        accessRemoveSuccess.gameObject.SetActive(false);
                                        accessNotmatch.gameObject.SetActive(true);
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogError("Response format is incorrect. Expected at least 4 parts.");
                                errorMessages.text = "ไม่สามารถเชื่อมต่อกับเซิฟเวอร์ได้";
                            }
                        }
                        else
                        {
                            Debug.Log("Error !");
                            errorMessages.text = "ERROR 404";
                        }
                        // สามารถแสดง username บน UI ได้
                        // user.text = usernameFromApi; // แสดง username ที่ตรวจพบจาก HWID
                    }
                    else
                    {
                        if (data.Length >= 4)
                        {

                            if (string.IsNullOrEmpty(data[2]))
                            {
                                Debug.Log("hwid ยังไม่มีในฐานข้อมูล");
                                firstNameText.text = "ยังไม่มีในฐานข้อมูล";
                                requestButton.gameObject.SetActive(true);
                                revokeButton.gameObject.SetActive(false);
                                loginPanel.gameObject.SetActive(false);
                                welcomePanel.gameObject.SetActive(true);
                                // ขอสิทธิ์คอมพิวเตอร์เครื่องนี้
                                // logout
                            }
                            else
                            {
                                if (data[2] == EncryptHWID(hwid))
                                {
                                    Debug.Log("hwid มีในฐานข้อมูล และตรงกัน");
                                    firstNameText.text = "ตรงกัน";
                                    revokeButton.gameObject.SetActive(true);
                                    requestButton.gameObject.SetActive(false);
                                    loginPanel.gameObject.SetActive(false);
                                    welcomePanel.gameObject.SetActive(true);
                                    // ลบสิทธิ์คอมพิวเตอร์เครื่องนี้
                                    // logout
                                }
                                else
                                {
                                    Debug.Log("hwid มีในฐานข้อมูล และไม่ตรงกัน");
                                    firstNameText.text = "ไม่ตรงกัน";
                                    welcomePanel.gameObject.SetActive(true);
                                    loginPanel.gameObject.SetActive(false);
                                    revokeButton.gameObject.SetActive(false);
                                    requestButton.gameObject.SetActive(false);
                                    // แจ้งเตือนต้องลบสิทธิ์เครื่องเก่าก่อนจึงจะขอสิทธิ์ได้
                                    accessAddSuccess.gameObject.SetActive(false);
                                    accessRemoveSuccess.gameObject.SetActive(false);
                                    accessNotmatch.gameObject.SetActive(true);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Response format is incorrect. Expected at least 4 parts.");
                            errorMessages.text = "ไม่สามารถเชื่อมต่อกับเซิฟเวอร์ได้";
                        }
                    }
                }
            }
            else if (response.StartsWith("SERVER: error,"))
            {
                Debug.LogError("Invalid username or password.");
                errorMessages.text = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง";
            }
            else
            {
                Debug.LogError("Unexpected response format: " + response);
            }
        }

        loginButton.interactable = true;
        progressCircle.SetActive(false);
    }


    public void CheckInternetConnection()
    {
        // ตรวจสอบเบื้องต้นด้วย Application.internetReachability
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("ไม่มีการเชื่อมต่อเครือข่าย");
            internet.text = "ไม่มีการเชื่อมต่อเครือข่าย";
            internet.color = Color.red;
            return;
        }

        Debug.Log("ตรวจพบเครือข่าย! กำลังตรวจสอบการเข้าถึงอินเทอร์เน็ตจริง...");
        internet.text = "กำลังตรวจสอบ...";
        internet.color = Color.yellow;
        StartCoroutine(CheckInternetCoroutine());
    }

    private System.Collections.IEnumerator CheckInternetCoroutine()
    {
        bool isConnected = false;

        using (var webClient = new WebClient())
        {
            try
            {
                // ลองดาวน์โหลดข้อมูลจาก URL
                webClient.DownloadString(TestUrl);
                isConnected = true;
            }
            catch
            {
                isConnected = false;
            }
        }

        // แสดงผลการตรวจสอบ
        if (isConnected)
        {
            Debug.Log("เชื่อมต่ออินเทอร์เน็ตได้!");
            internet.text = "เชื่อมต่ออินเทอร์เน็ตได้!";
            internet.color = Color.green;
        }
        else
        {
            Debug.Log("มีเครือข่าย แต่ไม่สามารถเข้าถึงอินเทอร์เน็ตได้");
            internet.text = "มีเครือข่าย แต่ไม่สามารถเข้าถึงอินเทอร์เน็ตได้";
            internet.color = Color.red;
        }

        yield return null;
    }





}