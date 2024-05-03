using System;
using System.Security.Cryptography;
using System.Text;

namespace SaveSystem.FileSaverSystem
{
    public class AesEncryptionProvider
    {
        private const string Key = "FRDN36C5C25TZ2F9K6ND0JWNG4GO742D"; 
        private const string Iv = "1460352802516006"; 
        private readonly AesCryptoServiceProvider aesCryptoProvider;

        public AesEncryptionProvider()
        {
            aesCryptoProvider = new AesCryptoServiceProvider();
            aesCryptoProvider.BlockSize = 128;
            aesCryptoProvider.KeySize = 256;
            aesCryptoProvider.Key = Encoding.ASCII.GetBytes(Key);
            aesCryptoProvider.IV = Encoding.ASCII.GetBytes(Iv);
            aesCryptoProvider.Mode = CipherMode.CBC;
            aesCryptoProvider.Padding = PaddingMode.PKCS7;
        }
        
        public string AesEncryption(string inputData)
        {
            var txtByteData = Encoding.ASCII.GetBytes(inputData);
            var cryptoTransform = aesCryptoProvider.CreateEncryptor(aesCryptoProvider.Key, aesCryptoProvider.IV);
 
            var result = cryptoTransform.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return Convert.ToBase64String(result);
        }
        
        public string AesDecryption(string inputData)
        {
            var txtByteData = Convert.FromBase64String(inputData);
            var cryptoTransform = aesCryptoProvider.CreateDecryptor();
 
            var result = cryptoTransform.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return Encoding.ASCII.GetString(result);
        }
        
    }
}
