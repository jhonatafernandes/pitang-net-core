using System;
using System.Security.Cryptography;
using System.Text;

namespace Pitang.Sms.NetCore.Services
{
    public interface ICriptographyService
    {
        public string GetHash(HashAlgorithm hashAlgorithm, string input);

        public bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash);
    

    }
}
