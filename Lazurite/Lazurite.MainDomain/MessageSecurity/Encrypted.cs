﻿using Lazurite.IOC;
using Lazurite.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lazurite.MainDomain.MessageSecurity
{
    [DataContract]
    public class Encrypted<T>
    {
        private static Dictionary<string, SecureEncoding> Cached = new Dictionary<string, SecureEncoding>();
        private static SecureEncoding GetSecureEncoding(string key)
        {
            if (!Cached.ContainsKey(key))
            {
                Cached.Add(key, new SecureEncoding(key));
            }
            return Cached[key];
        }

        [DataMember]
        public string Data { get; set; }

        public Encrypted()
        {
            //do nothing
        }

        public Encrypted(T obj, string secretKey): this()
        {
            var serializer = SerializersFactory.GetSerializer<T>();
            Data = GetSecureEncoding(secretKey).Encrypt(serializer.Serialize(obj));
            ServerTime = DateTime.Now;
        }

        [DataMember]
        public DateTime ServerTime { get; set; }

        public T Decrypt(string secretKey)
        {
            try
            {
                var secureEncoding = GetSecureEncoding(secretKey);
                var serializer = SerializersFactory.GetSerializer<T>();
                var decryptedRaw = secureEncoding.Decrypt(Data);
                return (T)serializer.Deserialize(decryptedRaw.TrimEnd('\0'));
            }
            catch (Exception e)
            {
                if (Singleton.Any<ILogger>())
                    Singleton.Resolve<ILogger>()
                        .WarnFormat(e, "Ошибка при расшифровке строки");
                throw new DecryptException(e);
            }
        }
    }

    public class DecryptException : Exception
    {
        public DecryptException(Exception inner) : base("Ошибка при расшифровке строки", inner)
        {
            //do nothing
        }
    }
}
