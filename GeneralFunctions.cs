using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace UnlaunchEditor
{
    class GeneralFunctions
    {
        public static byte[] Background;
        public static byte[] UnlaunchFile;

        public static void InitializeUnlaunch()
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(UnlaunchFile)))
            {
                Console.WriteLine("[InitializeUnlanuch]: Extracting Background \t\t   - 0x48F0 (GIF)");
                Background = GetBytesAtAddress(0x48F0, 0x3c70);
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'TopScreen_Title'\t   - 0xE5FE (Text)");
                Strings.TopScreen_Title = FormatString(GetBytesAtAddress(0xE5FE, 27), new byte[] { 0x00 });
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'INUN_MountingEMMC'\t   - 0xE63A (Text)");
                Strings.INUN_MountingEMMC = GetBytesAtAddress(0xE63A, 21);
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'IN_Finished'\t\t   - 0xE6D1 (Text)");
                Strings.IN_Finished = FormatString(GetBytesAtAddress(0xE6D1, 23), new byte[] { 0x00 });
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'BottomScreen_CustomText' - 0xE7E8 (Text)");
                Strings.BottomScreen_CustomText = FormatString(GetBytesAtAddress(0xE7E8, 24), new byte[] { 0x00 });
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'BottomScreen_URL'\t   - 0xE802 (Text)");
                Strings.BottomScreen_URL = FormatString(GetBytesAtAddress(0xE802, 24), new byte[] { 0x00 });
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'MM_Size_PUB'\t\t   - 0xE81D (Text)");
                Strings.MM_Size_PUB = GetBytesAtAddress(0xE81D, 8);
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'MM_Size_PRV'\t\t   - 0xE827 (Text)");
                Strings.MM_Size_PRV = GetBytesAtAddress(0xE827, 3);
                Console.WriteLine("[InitializeUnlanuch]: Extracting 'HTI_ManualMethodDesc'\t   - 0xE984 (Text)");
                Strings.HTI_ManualMethodDesc = GetBytesAtAddress(0xE984, 3);
            }
            Console.WriteLine();
            FixStrings();
        }

        public static void OpacityUpdate(bool active)
        {
            if (active)
            {
                if (Form1.ActiveForm != null) { Form1.ActiveForm.Opacity = 1; }
                if (Actions.ActiveForm != null) { Actions.ActiveForm.Opacity = 1; }
            }
            else
            {
                if (Form1.ActiveForm != null) { Form1.ActiveForm.Opacity = 0.5; }
                if (Actions.ActiveForm != null) { Actions.ActiveForm.Opacity = 0.5; }
            }
        }

        public static void FixStrings()
        {
            Console.WriteLine("[FixStrings]: 'IN_Finished' \t\t('0x46' -> '0x65')");
            ReplaceBytes(
                Strings.MM_Size_PUB,               // 'Installation Complete' text
                new byte[] { 0x46 },
                new byte[] { 0x65 });
            Console.WriteLine("[FixStrings]: 'BottomScreen_URL' \t('0x80 0x01 0xD1' -> '0x70 0x72 0x6F')");
            ReplaceBytes(
                Strings.BottomScreen_URL,          // Bottom Screen link
                new byte[] { 0x80, 0x01, 0xD1 },
                new byte[] { 0x70, 0x72, 0x6F });
            Console.WriteLine("[FixStrings]: 'BottomScreen_CustomText' ('0x1B' -> '0x6C')");
            ReplaceBytes(
                Strings.BottomScreen_CustomText,   // Bottom Screen Custom Text
                new byte[] { 0x1B },
                new byte[] { 0x6C });
            Console.WriteLine("[FixStrings]: 'MM_Size_PUB' \t\t('0x1D' -> '0x65')");
            ReplaceBytes(
                Strings.MM_Size_PUB,               // Bottom Screen Custom Text
                new byte[] { 0x1D },
                new byte[] { 0x65 });
        }

        private static byte[] FormatString(byte[] data, byte[] invalidChars = null)
        {
            List<byte> filteredData = new List<byte>();
            foreach (byte b in data)
            {
                if (!invalidChars.Contains(b))
                {
                    filteredData.Add(b);
                }
            }
            return filteredData.ToArray();
        }

        #region http://stackoverflow.com/questions/5132890/ddg#5132938 - "C# Replace bytes in Byte[]" (slightly edited)
        public static int ContainsBytes(byte[] sourceData, byte[] pattern)
        {
            int index = -1;
            int matchIndex = 0;
            // handle the complete source array
            for (int i = 0; i < sourceData.Length; i++)
            {
                if (sourceData[i] == pattern[matchIndex])
                {
                    if (matchIndex == (pattern.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else if (sourceData[i] == pattern[0]) { matchIndex = 1;}
                else { matchIndex = 0; }

            }
            return index;
        }

        public static byte[] ReplaceBytes(byte[] sourceData, byte[] pattern, byte[] newData)
        {
            byte[] dst = new byte[sourceData.Length - pattern.Length + newData.Length]; ;
            int index = ContainsBytes(sourceData, pattern);
            if (index >= 0)
            {
                Buffer.BlockCopy(sourceData, 0, dst, 0, index);
                Buffer.BlockCopy(newData, 0, dst, index, newData.Length);
                Buffer.BlockCopy(
                    sourceData,
                    index + pattern.Length,
                    dst,
                    index + newData.Length,
                    sourceData.Length - (index + pattern.Length));
            }
            return dst;
        }
        #endregion

        private static byte[] GetBytesAtAddress(long address, int count)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(UnlaunchFile)))
            {
                br.BaseStream.Position = address;
                return br.ReadBytes(count);
            }
        }

        public static bool isValidHeader(byte[] data)
        {
            if (data[0] == 0x47 && // 'G'
                data[1] == 0x49 && // 'I' 
                data[2] == 0x46 && // 'F'
                data[3] == 0x38 && // '8'
                data[4] == 0x39 && // '9'
                data[5] == 0x61)   // 'a'
            {
                return true;
            }
            return false;
        }

        public static bool DoesBackgroundFit(byte[] data)
        {
            if (data.Length > 0x3C70) { return false; } else { return true; }
        }
    }
}
