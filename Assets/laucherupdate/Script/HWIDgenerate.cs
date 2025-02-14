using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class HWIDGenerator : MonoBehaviour
{
    // ฟังก์ชันสำหรับการเข้ารหัสด้วย SHA256
    private string EncryptHWID(string hwid)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(hwid);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower(); // แปลงเป็น string
        }
    }

    // ฟังก์ชันเริ่มต้นที่จะทำการสร้างไฟล์ hwid.dat
    void Start()
{
    // รับค่า HWID ปัจจุบัน
    string currentHWID = SystemInfo.deviceUniqueIdentifier;

    // เข้ารหัส HWID ปัจจุบัน
    string encryptedHWID = EncryptHWID(currentHWID);

    // ระบุเส้นทางไฟล์ hwid.dat
    string filePath = Path.Combine(Application.streamingAssetsPath, "data", "hwid.dat");

    // ตรวจสอบว่าโฟลเดอร์ "data" มีอยู่หรือไม่ ถ้าไม่ให้สร้าง
    if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "data")))
    {
        Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "data"));
    }

    // เขียนค่าเข้ารหัสลงในไฟล์
    File.WriteAllText(filePath, encryptedHWID);

    Debug.Log("ไฟล์ hwid.dat ถูกสร้างขึ้นแล้ว");
}

}
