using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private const string KeyFileName = "playerKey.txt";
    private const string IVFileName = "playerIV.txt";

    public (byte[] key, byte[] iv) GenerateKeyAndIV()
    {
        byte[] key = new byte[32]; // AES 256-bit key
        byte[] iv = new byte[16]; // AES block size is 128-bit (16 bytes)

        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(key);
            rng.GetBytes(iv);
        }

        SaveKeys(key, iv);
        return (key, iv);
    }

    private void SaveKeys(byte[] key, byte[] iv)
    {
        string keyPath = Application.persistentDataPath + "/" + KeyFileName;
        string ivPath = Application.persistentDataPath + "/" + IVFileName;

        File.WriteAllText(keyPath, Convert.ToBase64String(key));
        File.WriteAllText(ivPath, Convert.ToBase64String(iv));
    }

    public (byte[] key, byte[] iv) LoadKeys()
    {
        string keyPath = Application.persistentDataPath + "/" + KeyFileName;
        string ivPath = Application.persistentDataPath + "/" + IVFileName;

        byte[] key = Convert.FromBase64String(File.ReadAllText(keyPath));
        byte[] iv = Convert.FromBase64String(File.ReadAllText(ivPath));

        return (key, iv);
    }

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + KeyFileName))
        {
            GenerateKeyAndIV();
        }
    }
}
