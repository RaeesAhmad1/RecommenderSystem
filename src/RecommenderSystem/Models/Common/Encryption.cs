using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InventoryManagement.Models.Common
{
    //public static class Encryption
    //{
    //    private static bool optimalAsymmetricEncryptionPadding = false;
    //    private readonly static string PublicKey = "MjA0OCE8UlNBS2V5VmFsdWU+PE1vZHVsdXM+djFTTVVyYk5SZW50VDEya0FhWXNRMEh3Y2hjWG9nbnFUWGpYd1NXaGR5Qi9aaTQ5VnF4L0lFdWxSaGFhVjdHOUtENWRmY0I4eEZaZGgyNGJ0MHpZbGFNTlFyRVBNNnQzUEdvZXZmMXVCby9wVnhlcWFocEFkWkIwelNJcjhwTk5UOW52czV5WEN1Q00xRFo0UUR3Q3A3b2U2aXc2ZHZ4VEZNWFZJdW9rSkcrdmlFMWhORDhnbGg0dFVsMWVBdThKT3YyR0tyWmhvTmUxK2tnRzNNUmRueEFGTDQyRDl4eWF5NERvcmpGL2ZjYWNNc3dFYkM3MUo2bFNobnR2YnQ1RnY0elY1bkg0aDhqYzhnV1dQVDUvWG16TElLMmlJRDJ6L3NyeGgvbzdMRkRhWVhXMnVwbUt5VUJQR2k0OGJLUVZKT3JjZU9rd3owWE1nTDFJUk4yWnhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
    //    private readonly static string PrivateKey = "MjA0OCE8UlNBS2V5VmFsdWU+PE1vZHVsdXM+djFTTVVyYk5SZW50VDEya0FhWXNRMEh3Y2hjWG9nbnFUWGpYd1NXaGR5Qi9aaTQ5VnF4L0lFdWxSaGFhVjdHOUtENWRmY0I4eEZaZGgyNGJ0MHpZbGFNTlFyRVBNNnQzUEdvZXZmMXVCby9wVnhlcWFocEFkWkIwelNJcjhwTk5UOW52czV5WEN1Q00xRFo0UUR3Q3A3b2U2aXc2ZHZ4VEZNWFZJdW9rSkcrdmlFMWhORDhnbGg0dFVsMWVBdThKT3YyR0tyWmhvTmUxK2tnRzNNUmRueEFGTDQyRDl4eWF5NERvcmpGL2ZjYWNNc3dFYkM3MUo2bFNobnR2YnQ1RnY0elY1bkg0aDhqYzhnV1dQVDUvWG16TElLMmlJRDJ6L3NyeGgvbzdMRkRhWVhXMnVwbUt5VUJQR2k0OGJLUVZKT3JjZU9rd3owWE1nTDFJUk4yWnhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi8yY1VJS2RlMFB1b2RVaDJQQ3krbFU0aWFvVWtOZ0dOOVhHNmhvcll3c1ovbzdwdTJYZjZmS2E5M09OZ1R0NUpqaW5QL3grZG9ibmFiU1hNNFNwRGJlb3JVRGZBKzhYeDIxTHBCT0FtYUtUVWlkejNjMHlQRXBQZ3lOMlpVb3poUWhjejZlUk01cUdQSlgxU29WMjczM3ZUREFtTEVWS0N4eFRZOHVNSWI3OD08L1A+PFE+djhjYlBmcHh5aXZUelhsV2Q5L3hNK3pRUlJRSk4rTDFIYURiNHYxKzU3dExEb3VlcG03ajI0MkJFZ2U4dTNENmJEanZneWhBWFIxV3IwR09KSjBBb1ZPV2FLLzdvZ3NHZjBnM1dzNzVicWtWSmdNTHZETnFxSVVVd0ZqZml3TllONkJnN0dIdGl2S0VGdmJldTEzcGFxVERyTnFuV0ZQaWFQK1lkQ09xVjNzPTwvUT48RFA+eWVSVDF0UTNjWC9kMUlocFhud0lVOEltRm9vVTY5UWl3YWtiUjR1dWVabXNBR001aVJMOG9WaTFzVXpVTHNRczVRSk1kMklvbTFWdFF1YWtwRUZpZUJxcURvbGtOaUp0WTNDUTN0Zkp4T0szV0J1aVNEUjJ6THEwOEZPc0JjTnp0V2plRXIvendrUm9BYnlsZXdXN281Z2dadDJNWHk4WVRnTSsxQkYvODhVPTwvRFA+PERRPlRGOUxYd1JFbW9HWHFJVkF4UjVlblJJYTR0ZVcwRFhHN1pTbzNKMmRFMFhJSHpQRTYzelBxeGlRSlJFRnZSUEI5cVU1NU41N3UxazZzektGRzltV2JhaXZCbVBHN3dJN0JTZEtQQlNleXMzMUNSMC9hQ1NGdmpTNVRkeFdzYktVU0JyTFhuZWxOS2RkcVJPSkljN0ZiTjNPdXlDY2NoVjkzZGlqNnVSbEtzOD08L0RRPjxJbnZlcnNlUT5rSmYwVHZoNDZjTEQ4OElIVVZ0V3hYaDVsYlNUTWw2ZnB5cFhhUU9laUtpTy9XcnZic21waXdBVEhDQ0pERDhYdDFwbTc5K0hrc21sUjlrYktXR2U4WmNqZHJHdUZlZ3NDUGRpT3VGMVN0a283NWtnblJVY0ZTb1hxSzF1YVgvTWsxTEtDbVpZY3djQ0t2VC9OQUZrWVpVdVNqT3pPckVrRk9VNDdML3VDVE09PC9JbnZlcnNlUT48RD5HbTMyZUZLU0pvODYzZFRFbkFtMVlaRVJRdUZYdldWN1BUcHRLMXdrWXMxVmErc0ZSQnpON3Nza1NIdEUxTXBUbytTQmk2WjBWYmJNY3JIT0dGTUFOQ055Nkh5RzZnOU1pRWJzZWpndzQ2MHJnWUZlWkF1K1RiOG5zMUorR2FNcGNkZGNHa2FPUXMxa0JzaURjZlFZTmMwckNoUVQrMjI5bUVmL3VqUDN6Q1IzcUNzdkZjVTRuMkMwZzBYSWhLQ1dHYXRsbW5MOW9FMWN0MzY4aWZYK0JCUVljUExqSE05TTZaSU9pMWtmR3M2bXhaT0V3cm1BWFB0T0ZweW1tNlZjMUM4WGtVUENCVERtWUZTSFpiaHNaT09IZHpaVVlUa2lmN1VzRk40MjdTSDVrMTNpQTVGRGJTb053bW9kQ0ZrWitENGJNQ2JUZWgwVTNvell6M3FnM1E9PTwvRD48L1JTQUtleVZhbHVlPg==";
    //    public static string Encrypt(string plainText)
    //    {
    //        int keySize = 0;
    //        string publicKeyXml = "";
    //        GetKeyFromEncryptionString(PublicKey, out keySize, out publicKeyXml);
    //        var encrypted = Encrypt(Encoding.UTF8.GetBytes(plainText), keySize, publicKeyXml);

    //        return Convert.ToBase64String(encrypted);
    //    }
    //    private static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
    //    {
    //        if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
    //        int maxLength = GetMaxDataLength(keySize);
    //        if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is { 0 }", maxLength), "data");
    //        if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
    //        if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");
    //        using (var provider = new RSACryptoServiceProvider(keySize))
    //        {
    //            provider.FromXmlString(publicKeyXml);
    //            return provider.Encrypt(data, optimalAsymmetricEncryptionPadding);
    //        }
    //    }
    //    public static string Decrypt(string encryptedText)
    //    {
    //        int keySize = 0;
    //        string publicAndPrivateKeyXml = "";
    //        GetKeyFromEncryptionString(PrivateKey, out keySize, out publicAndPrivateKeyXml);
    //        var decrypted = Decrypt(Convert.FromBase64String(encryptedText), keySize, publicAndPrivateKeyXml);

    //        return Encoding.UTF8.GetString(decrypted);
    //    }
    //    private static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
    //    {
    //        if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
    //        if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
    //        if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");
    //        using (var provider = new RSACryptoServiceProvider(keySize))
    //        {
    //            provider.FromXmlString(publicAndPrivateKeyXml);
    //            return provider.Decrypt(data, optimalAsymmetricEncryptionPadding);
    //        }
    //    }
    //    private static int GetMaxDataLength(int keySize)
    //    {
    //        if (optimalAsymmetricEncryptionPadding)
    //        {
    //            return ((keySize - 384) / 8) + 7;
    //            //return ((keySize – 384) / 8) +7;
    //        }
    //        return ((keySize - 384) / 8) + 37;
    //        //return ((keySize – 384) / 8) +37;
    //    }
    //    private static bool IsKeySizeValid(int keySize)
    //    {
    //        return keySize >= 384 && keySize <= 16384 && keySize % 8 == 0;
    //    }
    //    private static void GetKeyFromEncryptionString(string rawkey, out int keySize, out string xmlKey)
    //    {
    //        keySize = 0;
    //        xmlKey = "";
    //        if (rawkey != null && rawkey.Length > 0)
    //        {
    //            byte[] keyBytes = Convert.FromBase64String(rawkey);
    //            var stringKey = Encoding.UTF8.GetString(keyBytes);
    //            if (stringKey.Contains("!"))
    //            {
    //                var splittedValues = stringKey.Split(new char[] { '!' }, 2);
    //                try
    //                {
    //                    keySize = int.Parse(splittedValues[0]);
    //                    xmlKey = splittedValues[1];
    //                }
    //                catch (Exception e) { }
    //            }
    //        }
    //    }
    //}




    //class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var keyGenrator = new RSACryptographyKeyGenerator();
    //        var keys = keyGenrator.GenerateKeys(RSAKeySize.Key2048);
    //        Console.WriteLine("nnn Public Key Text: " + keys.PublicKey);
    //        Console.WriteLine("nnn—————————————-nnn");
    //        Console.WriteLine("nnn Private Key Text: " + keys.PrivateKey);
    //        Console.WriteLine("nnn—————————————-nnn");
    //        Console.ReadKey();
    //    }
    //}


    //public enum RSAKeySize
    //{
    //    Key512 = 512,
    //    Key1024 = 1024,
    //    Key2048 = 2048,
    //    Key4096 = 4096
    //}
    //public class RSAKeysTypes
    //{
    //    public string PublicKey { get; set; }
    //    public string PrivateKey { get; set; }
    //}
    //public class RSACryptographyKeyGenerator
    //{
    //    public RSAKeysTypes GenerateKeys(RSAKeySize rsaKeySize)
    //    {
    //        int keySize = (int)rsaKeySize;
    //        if (keySize % 2 != 0 || keySize < 512)
    //            throw new Exception("Key should be multiple of two and greater than 512.");
    //        var rsaKeysTypes = new RSAKeysTypes();
    //        using (var provider = new RSACryptoServiceProvider(keySize))
    //        {
    //            var publicKey = provider.ToXmlString(false);
    //            var privateKey = provider.ToXmlString(true);
    //            var publicKeyWithSize = IncludeKeyInEncryptionString(publicKey, keySize);
    //            var privateKeyWithSize = IncludeKeyInEncryptionString(privateKey, keySize);
    //            rsaKeysTypes.PublicKey = publicKeyWithSize;
    //            rsaKeysTypes.PrivateKey = privateKeyWithSize;
    //        }
    //        return rsaKeysTypes;
    //    }
    //    private string IncludeKeyInEncryptionString(string publicKey, int keySize)
    //    {
    //        return Convert.ToBase64String(Encoding.UTF8.GetBytes(keySize.ToString() + "!" + publicKey));
    //    }
    //}



    public class Security
    {
        public static string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                string EncrptKey = Common.Value;
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                str = str.Replace(" ", "+");
                string DecryptKey = Common.Value;
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byte[] inputByteArray = new byte[str.Length];

                byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
        }
    }



}