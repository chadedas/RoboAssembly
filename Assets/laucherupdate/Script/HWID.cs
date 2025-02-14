using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System;

public class HWIDSender : MonoBehaviour
{
    private string hwid = SystemInfo.deviceUniqueIdentifier; // ดึง HWID ของอุปกรณ์
    private string apiUrl = "https://manage.np-robotics.com/api/hwid/index.php"; // URL ของ API

    public void SendHWIDToServer(string username)
    {
        StartCoroutine(SendHWID(username));
    }

    private string EncryptHWID(string hwid)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hwid);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
    private IEnumerator SendHWID(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("hwid", EncryptHWID(hwid));
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("HWID sent successfully: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending HWID: " + www.error);
            }
        }
    }
}
