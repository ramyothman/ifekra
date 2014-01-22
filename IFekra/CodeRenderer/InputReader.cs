using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Html
{
    class InputReader
    {
        StreamReader reader;
        int ptr;
        string[] tokens;
        string[] delimeters;
        public InputReader(string InputFilePath,string[] delimeters) 
        { 
            reader = new StreamReader(InputFilePath);
            this.delimeters = delimeters;
            tokens = new string[] { };
            ptr = 0;
        }
        public string Next() 
        {
            while (ptr >= tokens.Length && !reader.EndOfStream)
            {
                tokens = StringTokenizer.Split(reader.ReadLine(), delimeters);
                ptr = 0;
            }
            return tokens[ptr++];
        }
        public bool hasMoreTokens() 
        {
            while (ptr == tokens.Length && !reader.EndOfStream)
            {
                tokens = StringTokenizer.Split(reader.ReadLine(), delimeters);
                ptr = 0;
            }
            return ptr < tokens.Length;
        }
        public void Close() { reader.Close(); }
    }
}
