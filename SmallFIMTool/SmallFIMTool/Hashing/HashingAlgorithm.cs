using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Hashing
{
    public enum AvailableHashAlgorithms
    {
        Sha256 = 1,
        Sha512 = 2
    }

    public class FIMHashAlgorithm
    {
        public int Id { get; set; }
        public string AlgorithmText { get; set; }

        public HashAlgorithm Algorithm
        {
            get
            {
                return new HashAlgorithmFactory().GetHashAlgorithm(AlgorithmText);
            }
        }
    }

    public class HashAlgorithmFactory
    {
        public HashAlgorithm GetHashAlgorithm(string algorithmText)
        {
            HashAlgorithm algorithm;

            switch (algorithmText.ToLower())
            {
                case "sha256":
                    algorithm = new SHA256Managed();
                    break;
                case "sha512":
                    algorithm = new SHA512Managed();
                    break;
                default:
                    algorithm = new SHA256Managed();
                    break;
            }

            return algorithm;
        }
    }
}
