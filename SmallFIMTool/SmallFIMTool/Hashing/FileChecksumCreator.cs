using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Hashing
{
    public class FileChecksumCreator
    {
        public static string GetFileChecksum(string filePath, HashAlgorithm algorithm)
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] checksum = GetFileChecksumBytes(stream, algorithm);
                return GetFileChecksumStringFromBytes(checksum);
            }
        }

        private static byte[] GetFileChecksumBytes(Stream fileStream, HashAlgorithm algorithm)
        {
            byte[] checksum = algorithm.ComputeHash(fileStream);

            return checksum;
        }

        private static string GetFileChecksumStringFromBytes(byte[] checksum)
        {
            return BitConverter.ToString(checksum).Replace("-", string.Empty);
        }
    }
}
