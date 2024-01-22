using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public class Helper
    {
        public static byte[] getBytes(string stringBytes, char delimeter)
        {
            string[] arrayStr = stringBytes.Split(delimeter);
            byte[] bytesResult = new byte[arrayStr.Length];
            byte tmpByte;
            int counter = 0;
            foreach (string str in arrayStr)
            {
                if (byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out tmpByte))
                {
                    bytesResult[counter] = tmpByte;
                    counter++;
                }
                else
                {
                    return null;
                }
            }
            return bytesResult;
        }

        public static byte[] getBytes(string stringBytes)
        {
            if (stringBytes.Trim() == "")
                return null;

            string fString = "";
            int counter = 0;

            for (int i = 0; i < stringBytes.Length; i++)
            {
                if (stringBytes[i] == ' ')
                    continue;

                if (counter > 0)
                    if ((counter % 2) == 0)
                        fString += " ";

                fString += stringBytes[i].ToString();

                counter++;
            }

            return getBytes(fString, ' ');
        }

        public static int byteToInt(byte[] data, bool isLittleEndian)
        {
            byte[] tmpArry = new byte[data.Length];
            Array.Copy(data, tmpArry, tmpArry.Length);

            if (tmpArry.Length != 4)
            {
                if (isLittleEndian)
                    Array.Resize(ref tmpArry, 4);
                else
                {
                    Array.Reverse(tmpArry);
                    Array.Resize(ref tmpArry, 4);
                    Array.Reverse(tmpArry);
                }
            }

            if (isLittleEndian)
                return (tmpArry[3] << 24) + (tmpArry[2] << 16) + (tmpArry[1] << 8) + tmpArry[0];
            else
                return (tmpArry[0] << 24) + (tmpArry[1] << 16) + (tmpArry[2] << 8) + tmpArry[3];
        }

        public static int byteToInt(byte[] data)
        {
            byte[] tmpData = new byte[data.Length];

            Array.Copy(data, tmpData, data.Length);

            if (tmpData.Length != 4)
            {
                Array.Reverse(tmpData);
                Array.Resize(ref tmpData, 4);
                Array.Reverse(tmpData);
            }
            return (tmpData[0] << 24) + (tmpData[1] << 16) + (tmpData[2] << 8) + tmpData[3];
        }

        public static byte[] intToByte(int num)
        {
            byte[] tmpByte = new byte[4];

            tmpByte[0] = (byte)((num >> 24) & 0xFF);
            tmpByte[1] = (byte)((num >> 16) & 0xFF);
            tmpByte[2] = (byte)((num >> 8) & 0xFF);
            tmpByte[3] = (byte)(num & 0xFF);

            return tmpByte;
        }

        public static byte[] intToByte(UInt32 num)
        {
            byte[] tmpByte = new byte[4];

            tmpByte[0] = (byte)((num >> 24) & 0xFF);
            tmpByte[1] = (byte)((num >> 16) & 0xFF);
            tmpByte[2] = (byte)((num >> 8) & 0xFF);
            tmpByte[3] = (byte)(num & 0xFF);

            return tmpByte;
        }

        public static string byteAsString(byte[] b, int startIndx, int len, bool spaceinbetween)
        {
            if (b.Length < startIndx + len)
                Array.Resize(ref b, startIndx + len);

            byte[] newByte = new byte[len];
            Array.Copy(b, startIndx, newByte, 0, len);

            return byteAsString(newByte, spaceinbetween);
        }

        public static string byteAsString(byte[] tmpbytes, bool spaceinbetween)
        {
            if (tmpbytes == null)
                return "";

            string tmpStr = string.Empty;
            for (int i = 0; i < tmpbytes.Length; i++)
            {
                tmpStr += string.Format("{0:X2}", tmpbytes[i]);

                if (spaceinbetween)
                    tmpStr += " ";
            }
            return tmpStr;
        }

        public static bool byteArrayIsEqual(byte[] array1, byte[] array2, int lenght)
        {
            if (array1.Length < lenght)
                return false;

            if (array2.Length < lenght)
                return false;


            for (int i = 0; i < lenght; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        public static bool byteArrayIsEqual(byte[] array1, byte[] array2)
        {
            return byteArrayIsEqual(array1, array2, array2.Length);
        }

        public static byte[] appendArrays(byte[] arr1, byte[] arr2)
        {
            byte[] c = new byte[arr1.Length + arr2.Length];
            Buffer.BlockCopy(arr1, 0, c, 0, arr1.Length);
            Buffer.BlockCopy(arr2, 0, c, arr1.Length, arr2.Length);
            return c;
        }

        public static byte[] appendArrays(byte[] arr1, byte arr2)
        {
            byte[] c = new byte[1 + arr1.Length];
            Buffer.BlockCopy(arr1, 0, c, 0, arr1.Length);
            c[arr1.Length] = arr2;
            return c;
        }

        public static byte[] appendArrays2(byte arr1, byte[] arr2)
        {
            byte[] c = new byte[1 + arr2.Length];
            arr2.CopyTo(c, 1);
            c[0] = arr1;
            arr2 = c;
            return c;
        }

    }
}
