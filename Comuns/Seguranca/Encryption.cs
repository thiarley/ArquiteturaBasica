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
    /// Classe com definições para as classes de criptografia.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// Chave 64 bits para os arquivos.
        /// </summary>
        protected static string sEncryptFileKey = "\t8???Z??";

        /// <summary>     
        /// Vetor de bytes utilizados para a criptografia (Chave Externa)     
        /// </summary>     
        protected static byte[] bIV = 
                { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
                    0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        /// <summary>     
        /// Representação de valor em base 64 (Chave Interna)    
        /// O Valor representa a transformação para base64 de     
        /// um conjunto de 32 caracteres (8 * 32 = 256bits)    
        /// A chave é: "Criptografia"
        /// Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("Criptografia xxxxxx"))
        /// </summary>     
        protected const string cryptoKey = "Q3JpcHRvZ3JhZmlhIHByb2pldG8gRGFiaUF0bGFudGU=";

        /// <summary>
        /// Função para gerar Chave de 64 bits.
        /// </summary>
        /// <returns></returns>
        protected static string GenerateKey()
        {
            // Criar uma instância do algoritmo Symetric. Chave e IV é gerado automaticamente.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use a chave gerada automaticamente para criptografia.
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
    }
}
