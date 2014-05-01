using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Importer.MadTV
{
    public class MadTVEncoding : UTF7Encoding
    {
        public MadTVEncoding()
        {
            DecoderFallback = new MadTVMapper("~");
        }
    }

    public class MadTVMapper : DecoderFallback
    {
        public string DefaultString;
        internal Dictionary<ushort, ulong> mapping;

        public MadTVMapper()
            : this("*")
        {
        }

        public MadTVMapper(string defaultString)
        {
            this.DefaultString = defaultString;

            // Create table of mappings
            mapping = new Dictionary<ushort, ulong>();
            mapping.Add(0x81, (char)'ü');
            mapping.Add(0x84, (char)'ä');
            mapping.Add(0x8E, (char)'Ä');
            mapping.Add(0x94, (char)'ö');
            mapping.Add(0x99, (char)'Ö');
            mapping.Add(0x9A, (char)'Ü');
            mapping.Add(0xE1, (char)'ß');
        }

        public override DecoderFallbackBuffer CreateFallbackBuffer()
        {
            return new MadTVFallbackBuffer(this);
        }

        public override int MaxCharCount
        {
            get { return 3; }
        }
    }



    public class MadTVFallbackBuffer : DecoderFallbackBuffer
    {
        int count = -1;                   // Number of characters to return
        int index = -1;                   // Index of character to return
        MadTVMapper fb;
        string charsToReturn;

        public MadTVFallbackBuffer(MadTVMapper fallback)
        {
            this.fb = fallback;
        }

        public override bool Fallback(byte[] bytesUnknown, int index)
        {
            // Return false if there are already characters to map.
            if (count >= 1) return false;

            // Determine number of characters to return.
            charsToReturn = String.Empty;

            ushort key = 0;
            if (bytesUnknown.Length == 2)
                key = Convert.ToUInt16(bytesUnknown);
            else
                key = bytesUnknown[0];

            if (fb.mapping.ContainsKey(key))
            {
                byte[] bytes = BitConverter.GetBytes(fb.mapping[key]);
                int ctr = 0;
                foreach (var byt in bytes)
                {
                    if (byt > 0)
                    {
                        ctr++;
                        charsToReturn += (char)byt;
                    }
                }
                count = ctr;
            }
            else
            {
                // Return default.
                charsToReturn = fb.DefaultString;
                count = 1;
            }
            this.index = charsToReturn.Length - 1;

            return true;
        }

        public override char GetNextChar()
        {
            // We'll return a character if possible, so subtract from the count of chars to return.
            count--;
            // If count is less than zero, we've returned all characters.
            if (count < 0)
                return '\u0000';

            this.index--;
            return charsToReturn[this.index + 1];
        }

        public override bool MovePrevious()
        {
            // Original: if count >= -1 and pos >= 0
            if (count >= -1)
            {
                count++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int Remaining
        {
            get { return count < 0 ? 0 : count; }
        }

        public override void Reset()
        {
            count = -1;
            index = -1;
        }
    }
}
