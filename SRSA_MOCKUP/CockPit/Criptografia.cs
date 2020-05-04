using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace COCKPIT
{
    public class Criptografia
    {
        public String key = "kz6hugj9mns4zbc7e52rhvrc0qstnrkiscy7sp5x4t5wrpmspw2c3d3zh3hdyzu5djlwmgnz0dyx16vudad21ipoj0y1h20d00sie1fjicczpe9lgxaxv4e647nmrxriduy2mnpn55tyf10eantu1m";

        #region Costructor

        public Criptografia()
        {

        }

        #endregion

        #region Public Method

        /// <summary>
        /// Encript InClear Text using RC4 method using EncriptionKey
        /// Put the result into CryptedText 
        /// </summary>
        /// <returns>true if success, else false</returns>
        public String Encrypt(String texto)
        {
            this.EncryptionKey = key;
            this.InClearText = texto;

            try
            {
                //
                // indexes used below
                //
                long i = 0;
                long j = 0;

                //
                // Put input string in temporary byte array
                //
                byte[] input = Encoding.UTF8.GetBytes(this.m_sInClearText);

                // 
                // Output byte array
                //
                byte[] output = new byte[input.Length];

                //
                // Local copy of m_nBoxLen
                //
                byte[] n_LocBox = new byte[m_nBoxLen];
                this.m_nBox.CopyTo(n_LocBox, 0);

                //
                //	Len of Chipher
                //
                long ChipherLen = input.Length + 1;

                //
                // Run Alghoritm
                //
                for (long offset = 0; offset < input.Length; offset++)
                {
                    i = (i + 1) % m_nBoxLen;
                    j = (j + n_LocBox[i]) % m_nBoxLen;
                    byte temp = n_LocBox[i];
                    n_LocBox[i] = n_LocBox[j];
                    n_LocBox[j] = temp;
                    byte a = input[offset];
                    byte b = n_LocBox[(n_LocBox[i] + n_LocBox[j]) % m_nBoxLen];
                    output[offset] = (byte)((int)a ^ (int)b);
                }

                //
                // Put result into output string ( CryptedText )
                //
                char[] outarrchar = new char[Encoding.UTF8.GetChars(output).Length];
                outarrchar = Encoding.UTF8.GetChars(output);
                this.m_sCryptedText = new string(outarrchar);
            }
            catch
            {
                //
                // error occured - set retcode to false.
                // 
                return null;
            }

            //
            // return retcode
            //
            return (this.m_sCryptedText);

        }

        /// <summary>
        /// Decript CryptedText into InClearText using EncriptionKey
        /// </summary>
        /// <returns>true if success else false</returns>
        public String Decrypt(String Texto)
        {
            try
            {
                return this.Encrypt(Texto);

            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region Prop definitions
        /// <summary>
        /// Get or set Encryption Key
        /// </summary>
        public string EncryptionKey
        {
            get
            {
                return (this.m_sEncryptionKey);
            }
            set
            {
                //
                // assign value only if it is a new value
                //
                if (this.m_sEncryptionKey != value)
                {
                    this.m_sEncryptionKey = value;

                    //
                    // Used to populate m_nBox
                    //
                    long index2 = 0;


                    //
                    // Perform the conversion of the encryption key from unicode to ansi
                    //
                    byte[] utf8Bytes = Encoding.UTF8.GetBytes(this.m_sEncryptionKey);

                    //
                    // Convert the new byte[] into a char[] and then to string
                    //

                    char[] asciiChars = new char[Encoding.UTF8.GetChars(utf8Bytes).Length];

                    this.m_sEncryptionKeyAscii = new string(asciiChars);

                    //
                    // Populate m_nBox
                    //
                    long KeyLen = m_sEncryptionKey.Length;

                    //
                    // First Loop
                    //
                    for (long count = 0; count < KeyLen; count++)
                    {
                        this.m_nBox[count] = (byte)count;
                    }

                    //
                    // Second Loop
                    //
                    for (long count = 0; count < KeyLen; count++)
                    {
                        index2 = (index2 + m_nBox[count] + asciiChars[count % KeyLen]) % m_nBoxLen;
                        byte temp = m_nBox[count];
                        m_nBox[count] = m_nBox[index2];
                        m_nBox[index2] = temp;
                    }

                }
            }
        }

        public string InClearText
        {
            get
            {
                return (this.m_sInClearText);
            }
            set
            {
                //
                // assign value only if it is a new value
                //
                if (this.m_sInClearText != value)
                {
                    this.m_sInClearText = value;
                }
            }
        }

        public string CryptedText
        {
            get
            {
                return (this.m_sCryptedText);
            }
            set
            {
                //
                // assign value only if it is a new value
                //
                if (this.m_sCryptedText != value)
                {
                    this.m_sCryptedText = value;
                }
            }
        }
        #endregion

        #region Private Fields


        private string CreateKey(int tamanho)
        {
            const string keyCaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890";
            int valormaximo = keyCaracteresValidos.Length;

            Random random = new Random(DateTime.Now.Millisecond);

            StringBuilder key = new StringBuilder(tamanho);

            for (int indice = 0; indice < tamanho; indice++)
                key.Append(keyCaracteresValidos.Substring(random.Next(0, valormaximo), 1));

            return key.ToString();
        }

        //
        // Encryption Key  - Unicode & Ascii version
        //
        private string m_sEncryptionKey = "";
        private string m_sEncryptionKeyAscii = "";
        //
        // It is related to Encryption Key
        //
        protected byte[] m_nBox = new byte[m_nBoxLen];
        //
        // Len of nBox
        //
        static public long m_nBoxLen = 5000;

        //
        // In Clear Text
        //
        private string m_sInClearText = "";
        private string m_sCryptedText = "";

        #endregion

    }
}
