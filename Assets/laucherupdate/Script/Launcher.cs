using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;


public class Launcher : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button checkUpdateButton;
    [SerializeField] Button updateNowButton;
    [SerializeField] Button notNowButton;
    [SerializeField] Text alertText;
    [SerializeField] GameObject updatePopup;
    [SerializeField] GameObject accessPopup;
    [SerializeField] Button accessNowButton;
    [SerializeField] Button accessnotNowButton;
    [SerializeField] Button accessManage;

    private string currentVersion = "0.0.2"; // Current application version
    private string versionCheckURL = "https://manage.np-robotics.com/api/checkversion/index.php";
    private string downloadURL = "https://manage.np-robotics.com/upload/RobotAssembly.exe";

    private string EncryptHWID(string hwid)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hwid);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
    void Start()
    {
        // Attach button click listeners
        startButton.onClick.AddListener(StartApplication);
        checkUpdateButton.onClick.AddListener(CheckForUpdate);

        updateNowButton.onClick.AddListener(UpdateNow);
        notNowButton.onClick.AddListener(NotNow);

        accessNowButton.onClick.AddListener(accessNow);
        accessnotNowButton.onClick.AddListener(accessNotNow);

        accessManage.onClick.AddListener(accessNow);

        // Hide update-related UI elements at the start
        updateNowButton.gameObject.SetActive(false);
        notNowButton.gameObject.SetActive(false);
        updatePopup.SetActive(false);

        accessPopup.SetActive(false);
        accessnotNowButton.gameObject.SetActive(false);
        accessNowButton.gameObject.SetActive(false);

        // Check for updates on startup if internet is reachable
        
    }

    // Start the application without checking for updates
    void StartApplication()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "data", "hwid.dat");

        if (File.Exists(filePath))
        {
            Debug.Log("พบไฟล์ hwid.dat");

            // อ่านค่าจากไฟล์
            string fileEncryptedHWID = File.ReadAllText(filePath).Trim();

            // สร้าง HWID ปัจจุบัน
            string currentHWID = SystemInfo.deviceUniqueIdentifier;
            currentHWID += "64nUH&EzLk^eq@Q}.iMnTU'9IEDX2PS8T4]!XBv";

            // เข้ารหัส HWID ปัจจุบัน
            string encryptedHWID = EncryptHWID(currentHWID);

            // เปรียบเทียบว่า HWID ตรงกับที่เก็บไว้ในไฟล์หรือไม่
            if (fileEncryptedHWID == encryptedHWID)
            {
                Debug.Log("HWID ตรงกัน");
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Debug.Log("HWID ไม่ตรงกัน");
                ShowAccessPrompt();
            }
        }
        else
        {
            Debug.Log("ไม่พบไฟล์ hwid.dat");
            ShowAccessPrompt();
        }

        Debug.Log("Starting application without internet connection.");
        
    }

    // Check for updates
    void CheckForUpdate()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            alertText.text = "No internet connection.";
            return;
        }

        StartCoroutine(CheckUpdateCoroutine());
    }

    // Coroutine to check the latest version
IEnumerator CheckUpdateCoroutine()
{
    Dictionary<string, string> formData = new Dictionary<string, string>
    {
        { "version", currentVersion }
    };

    using UnityWebRequest www = UnityWebRequest.Post(versionCheckURL, formData);
    yield return www.SendWebRequest();

    if (www.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error: " + www.error);
        alertText.text = "Error checking for updates.";
    }
    else
    {
        string response = www.downloadHandler.text;
        Debug.Log("Response: " + response);

        if (response.StartsWith("Version:"))
        {
            string versionFromServer = response.Substring(8).Trim(); // Extract the version after "Version: "
            if (IsVersionNewer(currentVersion, versionFromServer))
            {
                Debug.Log("New version available: " + versionFromServer);
                ShowUpdatePrompt();
            }
            else
            {
                Debug.Log("Application is up-to-date.");
                alertText.text = "You have the latest version.";
            }
        }
        else if (response.Contains("SERVER: error, version not found"))
        {
            Debug.LogError("Version not found in the database.");
            alertText.text = "The specified version was not found. Please check the version number.";
            ShowUpdatePrompt();
        }
        else if (response.Contains("SERVER: error, please enter a valid version"))
        {
            Debug.LogError("Invalid version format.");
            alertText.text = "Please enter a valid version.";
        }
        else
        {
            Debug.Log("Unexpected response format.");
            alertText.text = "Could not check for updates. Please try again.";
        }
    }
}


    // Compare versions
bool IsVersionNewer(string currentVersion, string latestVersion)
{
    try
    {
        string[] currentParts = currentVersion.Split('.');
        string[] latestParts = latestVersion.Split('.');

        for (int i = 0; i < 3; i++)
        {
            int currentPart = int.Parse(currentParts[i]);
            int latestPart = int.Parse(latestParts[i]);

            if (latestPart > currentPart)
            {
                return true; // Latest version is newer
            }
            else if (latestPart < currentPart)
            {
                return false; // Current version is newer
            }
        }

        // If all parts are equal, the versions are the same
        return false;
    }
    catch (Exception e)
    {
        Debug.LogError("Version comparison failed: " + e.Message);
        return false;
    }
}

    // Show the update prompt
    void ShowUpdatePrompt()
    {
        alertText.text = "A new version is available!";
        updatePopup.SetActive(true);
        updateNowButton.gameObject.SetActive(true);
        notNowButton.gameObject.SetActive(true);
    }
    void ShowAccessPrompt()
    {
        alertText.text = "AccessPrompt";
        accessPopup.SetActive(true);
        accessNowButton.gameObject.SetActive(true);
        accessnotNowButton.gameObject.SetActive(true);
    }
    // Update now action
    void UpdateNow()
    {
        Debug.Log("Updating to the latest version...");
        Application.OpenURL(downloadURL);
        updatePopup.SetActive(false);
    }
    void accessNow()
    {
        Debug.Log("Access Now");
        SceneManager.LoadScene("LoginScenes");
        accessPopup.SetActive(false);
    }
    // Skip update action
    void NotNow()
    {
        Debug.Log("Skipping");
        updatePopup.SetActive(false);
    }
    void accessNotNow()
    {
        Debug.Log("Skipping");
        accessPopup.SetActive(false);
    }
}
