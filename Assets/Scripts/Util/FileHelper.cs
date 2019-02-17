using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace FileHelper
{
    public class FileRead
    {
        public async Task<Byte[]> ReadAllBytesAsync(string path)
        {
            byte[] result;
            using (FileStream SourceStream = File.Open(path, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }
            return result;
        }
    }
}

