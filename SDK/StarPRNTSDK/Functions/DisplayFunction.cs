using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using StarIO_Extension;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace StarPRNTSDK.Functions
{
    class DisplayFunction
    {
        public static void AppendClearScreen(IDisplayCommandBuilder builder)
        {
            builder.AppendClearScreen();
        }

        public static void AppendTextPattern(IDisplayCommandBuilder builder, int number)
        {
          //builder.AppendClearScreen();
            builder.AppendCursorMode(DisplayCursorMode.Off);
            builder.AppendSpecifiedPosition(1, 1);

            byte[] pattern1 = { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x30, 0x31, 0x32, 0x33,
                                0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47 };

            byte[] pattern2 = { 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a, 0x5b,
                                0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f };

            byte[] pattern3 = { 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f, 0x80, 0x81, 0x82, 0x83,
                                0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8a, 0x8b, 0x8c, 0x8d, 0x8e, 0x8f, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97 };

            byte[] pattern4=  { 0x98, 0x99, 0x9a, 0x9b, 0x9c, 0x9d, 0x9e, 0x9f, 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xab,
                                0xac, 0xad, 0xae, 0xaf, 0xb0, 0xb1, 0xb2, 0xb3, 0xb4, 0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xbb, 0xbc, 0xbd, 0xbe, 0xbf };

            byte[] pattern5 = { 0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce, 0xcf, 0xd0, 0xd1, 0xd2, 0xd3,
                                0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 0xdc, 0xdd, 0xde, 0xdf, 0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5, 0xe6, 0xe7 };

            byte[] pattern6 = { 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed, 0xee, 0xef, 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb,
                                0xfc, 0xfd, 0xfe, 0xff, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
    
            switch (number)
            {
                default: builder.AppendBytes(pattern1); break;
                case 1 : builder.AppendBytes(pattern2); break;
                case 2 : builder.AppendBytes(pattern3); break;
                case 3 : builder.AppendBytes(pattern4); break;
                case 4 : builder.AppendBytes(pattern5); break;
                case 5 : builder.AppendBytes(pattern6); break;
            }

        }


        public static async Task ApppendGraphicPattern(IDisplayCommandBuilder builder, int number)
        {
            //builder.AppendClearScreen();
            builder.AppendCursorMode(DisplayCursorMode.Off);
            //builder.AppendSpecifiedPosition(1, 1);

            BitmapDecoder bitmapDecoder = null;
            switch (number)
            {
                default:
                    bitmapDecoder = await CreateDisplayImageAsync("DisplayImage1.png");
                    await builder.AppendBitmapAsync(bitmapDecoder, true); break;
                case 1:
                    bitmapDecoder = await CreateDisplayImageAsync("DisplayImage2.png");
                    await builder.AppendBitmapAsync(bitmapDecoder, true); break;
            }

        }

        private static async Task<BitmapDecoder> CreateDisplayImageAsync(string resouceName)
        {
            BitmapDecoder logoData = null;

            StorageFile logoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/" + resouceName));
            using (IRandomAccessStreamWithContentType logoStream = await logoFile.OpenReadAsync())
            {
                logoData = await BitmapDecoder.CreateAsync(logoStream);
            }
            logoFile = null;

            return logoData;
        }

        public static void AppendCharacterSet(IDisplayCommandBuilder builder, StarIO_Extension.DisplayInternationalType internationalType, StarIO_Extension.DisplayCodePageType codePageType)
        {
          //builder.AppendClearScreen();
            builder.AppendCursorMode(DisplayCursorMode.Off);
            builder.AppendSpecifiedPosition(1, 1);

            builder.AppendInternational(internationalType);
            builder.AppendCodePage(codePageType);

            byte[] pattern1 = { 0x2d, 0x20, 0x20, 0x20, 0x23, 0x24, 0x40, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x20, 0x20, 0x20, 0x2d,
                                0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xab, 0xac, 0xad, 0xae, 0xaf, 0xb0, 0xb1, 0xb2, 0xb3 };

            byte[] pattern2 = { 0x2d, 0x20, 0x20, 0x20, 0x23, 0x24, 0x40, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x20, 0x20, 0x20, 0x2d,
                                0x88, 0xa0, 0x88, 0xa1, 0x88, 0xa2, 0x88, 0xa3, 0x88, 0xa4, 0x88, 0xa5, 0x88, 0xa6, 0x88, 0xa7, 0x88, 0xa8, 0x88, 0xa9 };

            byte[] pattern3 = { 0x2d, 0x20, 0x20, 0x20, 0x23, 0x24, 0x40, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x20, 0x20, 0x20, 0x2d,
                                0xb0, 0xa1, 0xb0, 0xa2, 0xb0, 0xa3, 0xb0, 0xa4, 0xb0, 0xa5, 0xb0, 0xa6, 0xb0, 0xa7, 0xb0, 0xa8, 0xb0, 0xa9, 0xb0, 0xaa };

            byte[] pattern4 = { 0x2d, 0x20, 0x20, 0x20, 0x23, 0x24, 0x40, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x20, 0x20, 0x20, 0x2d,
                                0xa4, 0x40, 0xa4, 0x41, 0xa4, 0x42, 0xa4, 0x43, 0xa4, 0x44, 0xa4, 0x45, 0xa4, 0x46, 0xa4, 0x47, 0xa4, 0x48, 0xa4, 0x49 };

            byte[] pattern5 = { 0x2d, 0x20, 0x20, 0x20, 0x23, 0x24, 0x40, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x20, 0x20, 0x20, 0x2d,
                                0xb0, 0xa1, 0xb0, 0xa2, 0xb0, 0xa3, 0xb0, 0xa4, 0xb0, 0xa5, 0xb0, 0xa6, 0xb0, 0xa7, 0xb0, 0xa8, 0xb0, 0xa9, 0xb0, 0xaa };

            switch (codePageType)
            {
                default                                    : builder.AppendBytes(pattern1); break; // CP437,Katakana,CP850,CP860,CP863,CP865,CP1252,CP866,CP852,CP858
                case DisplayCodePageType.Japanese          : builder.AppendBytes(pattern2); break;
                case DisplayCodePageType.SimplifiedChinese : builder.AppendBytes(pattern3); break;
                case DisplayCodePageType.TraditionalChinese: builder.AppendBytes(pattern4); break;
                case DisplayCodePageType.Hangul            : builder.AppendBytes(pattern5); break;
            }
        }


        public static void AppendTurnOn(IDisplayCommandBuilder builder, bool turnOn)
        {
            //builder.AppendClearScreen();
            //builder.AppendCursorMode(CursorMode.Off);
            //builder.AppendSpecifiedPosition(1, 1);

            //IBuffer otherData1 = System.Text.Encoding.UTF8.GetBytes("Star Micronics            ").AsBuffer();
            //IBuffer otherData2 = System.Text.Encoding.UTF8.GetBytes("Star Micronics").AsBuffer();

            //builder.AppendData(otherData1);
            //builder.AppendData(otherData2);

            builder.AppendTurnOn(turnOn);

        }


        public static void AppendCursorMode(IDisplayCommandBuilder builder, DisplayCursorMode cursorMode)
        {
          //builder.AppendClearScreen();
            builder.AppendCursorMode(DisplayCursorMode.Off);
            builder.AppendSpecifiedPosition(1, 1);

            IBuffer otherData1 = System.Text.Encoding.UTF8.GetBytes("Star Micronics      ").AsBuffer();
            IBuffer otherData2 = System.Text.Encoding.UTF8.GetBytes("Total :        12345").AsBuffer();

            builder.AppendData(otherData1);
            builder.AppendData(otherData2);

            builder.AppendSpecifiedPosition(20, 2);

            builder.AppendCursorMode(cursorMode);

        }

        public static void AppendContrastMode(IDisplayCommandBuilder builder, DisplayContrastMode contrastMode)
        {
            ////builder.AppendClearScreen();
            //builder.AppendCursorMode(CursorMode.Off);
            //builder.AppendSpecifiedPosition(1, 1);

            //IBuffer otherData1 = System.Text.Encoding.UTF8.GetBytes("Star Micronics            ").AsBuffer();
            //IBuffer otherData2 = System.Text.Encoding.UTF8.GetBytes("Star Micronics").AsBuffer();

            //builder.AppendData(otherData1);
            //builder.AppendData(otherData2);

            builder.AppendContrastMode(contrastMode);

        }


        public static void AppendUserDefinedCharacter(IDisplayCommandBuilder builder, bool set)
        {
            //builder.AppendClearScreen();
            builder.AppendCursorMode(DisplayCursorMode.Off);
            builder.AppendSpecifiedPosition(1, 1);

            builder.AppendInternational(DisplayInternationalType.USA);
            builder.AppendCodePage(DisplayCodePageType.Japanese);

            if (set)
            {
                builder.AppendUserDefinedCharacter(0, 0x20, new byte[] { 0x00, 0x00, 0x32, 0x00, 0x49, 0x00, 0x49, 0x7f, 0x26, 0x48, 0x00, 0x48, 0x00, 0x30, 0x00, 0x00 });

                builder.AppendUserDefinedDbcsCharacter(0, 0x8140, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x20, 0x04, 0x90, 0x04, 0x90, 0x02, 0x60,
                                                                               0x00, 0x00, 0x07, 0xf0, 0x04, 0x80, 0x04, 0x80, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            }
            else
            {
                builder.AppendUserDefinedCharacter(0, 0x00, null);

                builder.AppendUserDefinedDbcsCharacter(0, 0x00, null);
            }

            byte[] pattern = { 0x5b, 0x20, 0x20, 0x53, 0x74, 0x61, 0x72, 0x20, 0x4d, 0x69, 0x63, 0x72, 0x6f, 0x6e, 0x69, 0x63, 0x73, 0x20, 0x20, 0x5d,
                               0x5b, 0x81, 0x40, 0x81, 0x40, 0x83, 0x58, 0x83, 0x5e, 0x81, 0x5b, 0x90, 0xb8, 0x96, 0xa7, 0x81, 0x40, 0x81, 0x40, 0x5d };

            builder.AppendBytes(pattern);
        }

    }

}
