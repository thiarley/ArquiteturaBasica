using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace ArquiteturaBasica.Comuns.Seguranca
{
    /// <summary>
    /// Classe que criptografa e descriptografa arquivos.
    /// </summary>
    public class FileCryptography : Encryption
    {
        /// <summary>
        /// Delegate para criar eventos para o processamento da criptografia.
        /// </summary>
        /// <param name="message">Menssagem do que ocorre no momento.</param>
        /// <param name="value">Valor.</param>
        public delegate void FileCryptographyEvent(string message, long value);
        /// <summary>
        /// Evento que informa o progresso durante a criptografia.
        /// </summary>
        public static event FileCryptographyEvent OnCryptographTickProcess;

        /// <summary>
        /// Criptografa o conteúdo de um arquivo removendo o arquivo original.
        /// </summary>
        /// <param name="sInputFilename">Nome do arquivo original (descriptografado).</param>
        /// <param name="sOutputFilename">Nome do arquivo de destino (criptografado).</param>
        public static void EncryptFile(string sInputFilename, string sOutputFilename)
        {
            try
            {
                // Criar um stream de arquivo para ler o arquivo.
                FileStream fsInput = new FileStream(sInputFilename, FileMode.Open,FileAccess.Read);

                FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                // define a chave no decodificador
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sEncryptFileKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sEncryptFileKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                long sizeTotal = new FileInfo(sInputFilename).Length;

                byte[] bytearrayinput = new byte[8192];
                int length;
                while ((length = fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)) > 0)
                {
                    // Escreve por partes para evitar erro de estouro de memória na aplicação.
                    cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

                    if (OnCryptographTickProcess != null)
                    {
                        OnCryptographTickProcess(string.Empty, length);
                    }
                }

                cryptostream.Close();
                fsInput.Close();
                fsEncrypted.Close();
            }
            finally
            {
                if (File.Exists(sInputFilename))
                {
                    File.Delete(sInputFilename);
                }
            }
        }

        /// <summary>
        /// Descriptografa o conteúdo de um arquivo criando um novo.
        /// </summary>
        /// <param name="sInputFilename">Nome do arquivo original (criptografado).</param>
        /// <param name="sOutputFilename">Nome do arquivo de destino (descriptografado).</param>
        public static void DecryptFile(string sInputFilename, string sOutputFilename)
        {
            using (StreamWriter fsDecrypted = new StreamWriter(sOutputFilename))
            {
                // Criar um stream de arquivo para ler o arquivo criptografado.
                using (FileStream fsRead = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                        // Uma chave 64 bits e IV é requerido para esse provedor.
                        DES.Key = ASCIIEncoding.ASCII.GetBytes(sEncryptFileKey);
                        DES.IV = ASCIIEncoding.ASCII.GetBytes(sEncryptFileKey);

                        // Cria a instância de um decryptor DES.
                        ICryptoTransform desdecrypt = DES.CreateDecryptor();
                        // Criar uma stream de criptografia definida para ler e fazer uma descodificação DES transformar os bytes.
                        CryptoStream cryptostreamDecr = new CryptoStream(fsRead, desdecrypt, CryptoStreamMode.Read);

                        // Escreve por partes para evitar erro de estouro de memória na aplicação.
                        StreamReader streamReader = new StreamReader(cryptostreamDecr);
                        char[] bytearrayinput = new char[8192];
                        int length;
                        while ((length = streamReader.Read(bytearrayinput, 0, bytearrayinput.Length)) > 0)
                        {
                            // Imprimir o conteúdo do arquivo descriptografado.
                            fsDecrypted.Write(bytearrayinput, 0, bytearrayinput.Length);

                            if (OnCryptographTickProcess != null)
                            {
                                OnCryptographTickProcess(string.Empty, length);
                            }
                        }
                    }
                    finally
                    {
                        fsDecrypted.Flush();
                        fsDecrypted.Close();
                        fsRead.Flush();
                        fsRead.Close();
                    }
                }
            }
        } 
    }
}
