//using System;
//using System.Security.Cryptography;

//namespace AccountIdentification.Models
//    {
//        public class EncryptionHandler
//        {
//            // The proccess numbered with comments

//            private bool IsCommunicationInitiator;
//            private bool NeedKeyOtherKey;
//            private bool FirstPacketNeeded;

//            Action<byte[]> SendBytes;

//            private RSA Rsa;
//            private Aes Aes;

//            private ICryptoTransform AesEncryptor;
//            private ICryptoTransform AesDecryptor;

//            public EncryptionHandler(bool isCommunicationInitiator, Action<byte[]> sendBytes)
//            {
//                this.SendBytes = sendBytes;
//                this.IsCommunicationInitiator = isCommunicationInitiator;
//                NeedKeyOtherKey = true;
//                FirstPacketNeeded = true;

//                if (IsCommunicationInitiator)
//                {
//                    //STEP 1
//                    Rsa = RSA.Create();
//                    byte[] bytesToSend = System.Text.Encoding.UTF8.GetBytes(Rsa.ToXmlString(false));
//                    SendBytes.Invoke(bytesToSend);
//                }
//                else
//                {
//                    Aes = Aes.Create();
//                    //Aes.Padding = PaddingMode.Zeros;
//                    AesEncryptor = Aes.CreateEncryptor();
//                    AesDecryptor = Aes.CreateDecryptor();
//                }

//            }

//            public byte[] BytesReceived(byte[] bytes)
//            {
//                if (NeedKeyOtherKey)
//                {
//                    if (IsCommunicationInitiator)
//                    {
//                        if (FirstPacketNeeded)
//                        {
//                            //STEP 3
//                            byte[] encryptedAesKey = Rsa.Decrypt(bytes,
//                            RSAEncryptionPadding.OaepSHA1);

//                            Aes = Aes.Create();
//                            Aes.Key = encryptedAesKey;

//                            FirstPacketNeeded = false;
//                        }
//                        else
//                        {
//                            //STEP 4
//                            byte[] encryptedAesIv = Rsa.Decrypt(bytes,
//                            RSAEncryptionPadding.OaepSHA1);

//                            Aes.IV = encryptedAesIv;

//                            //Aes.Padding = PaddingMode.Zeros;
//                            AesEncryptor = Aes.CreateEncryptor();
//                            AesDecryptor = Aes.CreateDecryptor();

//                            NeedKeyOtherKey = false;
//                        }
//                    }
//                    else
//                    {
//                        //STEP 2
//                        string messageReceived = System.Text.Encoding.UTF8.GetString(bytes);

//                        Rsa = RSA.Create();
//                        Rsa.FromXmlString(messageReceived);

//                        Byte[] encryptedAesKey = Rsa.Encrypt(Aes.Key,
//                            RSAEncryptionPadding.OaepSHA1);

//                        SendBytes(encryptedAesKey);


//                        Byte[] encryptedAesIV = Rsa.Encrypt(Aes.IV,
//                            RSAEncryptionPadding.OaepSHA1);

//                        SendBytes(encryptedAesIV);

//                        NeedKeyOtherKey = false;
//                    }
//                    return null;
//                }
//                //STEP 5
//                return DecryptAesData(bytes);
//            }

//            /// <summary>
//            /// Gets dectypted bytes array and encrypt it with the symmetric key
//            /// </summary>
//            /// <param name="toEncrypt">The dectypted bytes array</param>
//            /// <returns></returns>
//            public byte[] EncryptAesData(Byte[] toEncrypt)
//            {
//                if (!NeedKeyOtherKey)
//                    return AesEncryptor.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
//                return null;
//            }
//            /// <summary>
//            /// Gets enctypted bytes array and decrypt it with the symmetric key
//            /// </summary>
//            /// <param name="toDecrypt"></param>
//            /// <returns></returns>
//            public byte[] DecryptAesData(byte[] toDecrypt)
//            {
//                if (!NeedKeyOtherKey)
//                    return AesDecryptor.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
//                return null;
//            }
//        }
//    }
//}

