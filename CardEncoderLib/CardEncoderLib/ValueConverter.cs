using System;

namespace CardEncoderLib
{
    internal static class ValueConverter
    {
        public static string addTrailingBits(string hex)
        {
            for (int i = hex.Length; i < 32; i++)
            {
                hex = hex + "0";
            }
            return hex;
        }

        public static string convertDecimalToHex(decimal num)
        {
            return convertStringToHex(num.ToString());
        }

        public static string convertHexToDecimal(string hex)
        {
            try
            {
                string asciiString = "";
                while (hex.Length >= 2)
                {
                    asciiString += System.Convert.ToDecimal(System.Convert.ToInt32(hex.Substring(0, 2), 16)).ToString();
                    hex = hex.Substring(2, hex.Length - 2);
                }
                return asciiString.TrimEnd(new char[] { '\0' });
            }
            catch
            {
                return "";
            }
        }

        public static string convertIntToHex(int num)
        {
            return convertStringToHex(num.ToString());
        }

        public static int convertHexToInt(string hex)
        {
            try
            {
                if (hex != "")
                {
                    return int.Parse(convertHexToString(hex));
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static string convertStringToHex(string ascii)
        {
            try
            {
                if (ascii == null) ascii = "";
                string hex = "";
                int temp;
                foreach (char c in ascii)
                {
                    temp = c;
                    hex += string.Format("{0:x2}", System.Convert.ToInt32(temp.ToString()));
                }
                return hex;
            }
            catch
            {
                return "";
            }
        }

        public static string convertHexToString(string hex)
        {
            try
            {
                string asciiString = "";
                while (hex.Length >= 2)
                {
                    asciiString += System.Convert.ToChar(System.Convert.ToInt32(hex.Substring(0, 2), 16)).ToString();
                    hex = hex.Substring(2, hex.Length - 2);
                }
                return asciiString.TrimEnd(new char[] { '\0' });
            }
            catch
            {
                return "";
            }
        }

        public static String byteToHEX(Byte ib)
        {
            String _str = String.Empty;
            try
            {
                char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
                'B', 'C', 'D', 'E', 'F' };
                char[] ob = new char[2];
                ob[0] = Digit[(ib >> 4) & 0X0F];
                ob[1] = Digit[ib & 0X0F];
                _str = new String(ob);
            }
            catch
            {
                throw new Exception("Error converting HEX characters");
            }
            return _str;
        }

        public static byte[] ToDigitsBytes(string theHex)
        {
            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0) ? 1 : 0)];
            for (int i = 0; i < bytes.Length; i++)
            {
                char lowbits = theHex[i * 2];
                char highbits;

                if ((i * 2 + 1) < theHex.Length)
                    highbits = theHex[i * 2 + 1];
                else

                    highbits = '0';

                int a = (int)GetHexBitsValue((byte)lowbits);
                int b = (int)GetHexBitsValue((byte)highbits);
                bytes[i] = (byte)((a << 4) + b);
            }

            return bytes;
        }

        public static byte GetHexBitsValue(byte ch)
        {
            byte sz = 0;
            if (ch <= '9' && ch >= '0')
                sz = (byte)(ch - 0x30);
            if (ch <= 'F' && ch >= 'A')
                sz = (byte)(ch - 0x37);
            if (ch <= 'f' && ch >= 'a')
                sz = (byte)(ch - 0x57);

            return sz;
        }
    }
}