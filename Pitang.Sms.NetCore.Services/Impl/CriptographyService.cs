using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Pitang.Sms.NetCore.Services.Impl
{
    public class CriptographyService : ICriptographyService
    {
        public string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            // Converta a string em uma array de bytes e calcula o hash..
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria uma nova string pra coletar os bytes
            var sBuilder = new StringBuilder();

            // Loop em cada byte dos dados do hash
            // e formata cada uma como uma string hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Retorna a string hexadecimal
            return sBuilder.ToString();
        }

        public bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Faz o Hash do input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Cria uma StringComparer para comparar os hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
