using StarIO_Extension;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace StarPRNTSDK.Functions
{
    class APIFunctions
    {
        public static IBuffer CreateGenericData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.").AsBuffer();
            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e };

            builder.BeginDocument();

            builder.AppendData(otherData);
            builder.AppendByte(0x0a);

            builder.AppendBytes(bytes, bytes.Length);
            builder.AppendByte(0x0a);
            
            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateFontStyleData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            builder.BeginDocument();

            builder.AppendData(otherData);
            builder.AppendFontStyle(FontStyleType.B);
            builder.AppendData(otherData);
            builder.AppendFontStyle(FontStyleType.A);
            builder.AppendData(otherData);
            builder.AppendFontStyle(FontStyleType.B);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateInitializationData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            builder.BeginDocument();

            builder.AppendData(otherData);
            builder.AppendMultiple(2, 2);
            builder.AppendData(otherData);
            builder.AppendFontStyle(FontStyleType.B);
            builder.AppendData(otherData);
            builder.AppendInitialization(InitializationType.Command);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateCodePageData(Emulation emulation)
        {
            byte[] bytes2 = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x0a };
            byte[] bytes3 = new byte[] { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x0a };
            byte[] bytes4 = new byte[] { 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 0x0a };
            byte[] bytes5 = new byte[] { 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 0x0a };
            byte[] bytes6 = new byte[] { 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f, 0x0a };
            byte[] bytes7 = new byte[] { 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f, 0x0a };
            byte[] bytes8 = new byte[] { (byte)0x80, (byte)0x81, (byte)0x82, (byte)0x83, (byte)0x84, (byte)0x85, (byte)0x86, (byte)0x87, (byte)0x88, (byte)0x89, (byte)0x8a, (byte)0x8b, (byte)0x8c, (byte)0x8d, (byte)0x8e, (byte)0x8f, 0x0a };
            byte[] bytes9 = new byte[] { (byte)0x90, (byte)0x91, (byte)0x92, (byte)0x93, (byte)0x94, (byte)0x95, (byte)0x96, (byte)0x97, (byte)0x98, (byte)0x99, (byte)0x9a, (byte)0x9b, (byte)0x9c, (byte)0x9d, (byte)0x9e, (byte)0x9f, 0x0a };
            byte[] bytesA = new byte[] { (byte)0xa0, (byte)0xa1, (byte)0xa2, (byte)0xa3, (byte)0xa4, (byte)0xa5, (byte)0xa6, (byte)0xa7, (byte)0xa8, (byte)0xa9, (byte)0xaa, (byte)0xab, (byte)0xac, (byte)0xad, (byte)0xae, (byte)0xaf, 0x0a };
            byte[] bytesB = new byte[] { (byte)0xb0, (byte)0xb1, (byte)0xb2, (byte)0xb3, (byte)0xb4, (byte)0xb5, (byte)0xb6, (byte)0xb7, (byte)0xb8, (byte)0xb9, (byte)0xba, (byte)0xbb, (byte)0xbc, (byte)0xbd, (byte)0xbe, (byte)0xbf, 0x0a };
            byte[] bytesC = new byte[] { (byte)0xc0, (byte)0xc1, (byte)0xc2, (byte)0xc3, (byte)0xc4, (byte)0xc5, (byte)0xc6, (byte)0xc7, (byte)0xc8, (byte)0xc9, (byte)0xca, (byte)0xcb, (byte)0xcc, (byte)0xcd, (byte)0xce, (byte)0xcf, 0x0a };
            byte[] bytesD = new byte[] { (byte)0xd0, (byte)0xd1, (byte)0xd2, (byte)0xd3, (byte)0xd4, (byte)0xd5, (byte)0xd6, (byte)0xd7, (byte)0xd8, (byte)0xd9, (byte)0xda, (byte)0xdb, (byte)0xdc, (byte)0xdd, (byte)0xde, (byte)0xdf, 0x0a };
            byte[] bytesE = new byte[] { (byte)0xe0, (byte)0xe1, (byte)0xe2, (byte)0xe3, (byte)0xe4, (byte)0xe5, (byte)0xe6, (byte)0xe7, (byte)0xe8, (byte)0xe9, (byte)0xea, (byte)0xeb, (byte)0xec, (byte)0xed, (byte)0xee, (byte)0xef, 0x0a };
            byte[] bytesF = new byte[] { (byte)0xf0, (byte)0xf1, (byte)0xf2, (byte)0xf3, (byte)0xf4, (byte)0xf5, (byte)0xf6, (byte)0xf7, (byte)0xf8, (byte)0xf9, (byte)0xfa, (byte)0xfb, (byte)0xfc, (byte)0xfd, (byte)0xfe, (byte)0xff, 0x0a };

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendCodePage(CodePageType.CP998);  builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP998*\n").AsBuffer());

            builder.AppendBytes(bytes2, bytes2.Length);
            builder.AppendBytes(bytes3, bytes3.Length);
            builder.AppendBytes(bytes4, bytes4.Length);
            builder.AppendBytes(bytes5, bytes5.Length);
            builder.AppendBytes(bytes6, bytes6.Length);
            builder.AppendBytes(bytes7, bytes7.Length);
            builder.AppendBytes(bytes8, bytes8.Length);
            builder.AppendBytes(bytes9, bytes9.Length);
            builder.AppendBytes(bytesA, bytesA.Length);
            builder.AppendBytes(bytesB, bytesB.Length);
            builder.AppendBytes(bytesC, bytesC.Length);
            builder.AppendBytes(bytesD, bytesD.Length);
            builder.AppendBytes(bytesE, bytesE.Length);
            builder.AppendBytes(bytesF, bytesF.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n").AsBuffer());

            //builder.AppendCodePage(CodePageType.CP437); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP437*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP737); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP737*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP772); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP772*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP774); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP774*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP851); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP851*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP852); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP852*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP855); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP855*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP857); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP857*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP858); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP858*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP860); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP860*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP861); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP861*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP862); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP862*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP863); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP863*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP864); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP864*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP865); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP865*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP866); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP866*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP869); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP869*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP874); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP874*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP928); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP928*\n").AsBuffer());
            builder.AppendCodePage(CodePageType.CP932); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP932*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP998); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP998*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP999); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP999*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP1001); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP1001*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP1250); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP1250*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP1251); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP1251*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP1252); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP1252*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP2001); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP2001*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3001); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3001*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3002); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3002*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3011); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3011*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3012); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3012*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3021); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3021*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3041); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3041*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3840); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3840*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3841); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3841*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3843); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3843*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3844); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3844*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3845); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3845*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3846); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3846*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3847); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3847*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.CP3848); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*CP3848*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.UTF8); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*UTF8*\n").AsBuffer());
            //builder.AppendCodePage(CodePageType.Blank); builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Blank*\n").AsBuffer());

            builder.AppendBytes(bytes2, bytes2.Length);
            builder.AppendBytes(bytes3, bytes3.Length);
            builder.AppendBytes(bytes4, bytes4.Length);
            builder.AppendBytes(bytes5, bytes5.Length);
            builder.AppendBytes(bytes6, bytes6.Length);
            builder.AppendBytes(bytes7, bytes7.Length);
            builder.AppendBytes(bytes8, bytes8.Length);
            builder.AppendBytes(bytes9, bytes9.Length);
            builder.AppendBytes(bytesA, bytesA.Length);
            builder.AppendBytes(bytesB, bytesB.Length);
            builder.AppendBytes(bytesC, bytesC.Length);
            builder.AppendBytes(bytesD, bytesD.Length);
            builder.AppendBytes(bytesE, bytesE.Length);
            builder.AppendBytes(bytesF, bytesF.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateInternationalData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            byte[] bytes = { 0x23, 0x24, 0x40, 0x58, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x60, 0x7b, 0x7c, 0x7d, 0x7e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*USA*\n").AsBuffer());
            builder.AppendInternational(InternationalType.USA);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*France*\n").AsBuffer());
            builder.AppendInternational(InternationalType.France);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Germany*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Germany);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*UK*\n").AsBuffer());
            builder.AppendInternational(InternationalType.UK);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Denmark*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Denmark);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Sweden*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Sweden);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Italy*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Italy);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Spain*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Spain);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Japan*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Japan);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Norway*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Norway);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Denmark2*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Denmark2);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Spain2*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Spain2);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*LatinAmerica*\n").AsBuffer());
            builder.AppendInternational(InternationalType.LatinAmerica);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Korea*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Korea);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Ireland*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Ireland);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Legal*\n").AsBuffer());
            builder.AppendInternational(InternationalType.Legal);
            builder.AppendBytes(bytes, bytes.Length);


            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateFeedData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData       = System.Text.Encoding.UTF8.GetBytes("Hello World.").AsBuffer();
            IBuffer otherDataWithLF = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e };

            builder.BeginDocument();

            builder.AppendData(otherData);
            builder.AppendLineFeed();
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendBytesWithLineFeed(bytes, bytes.Length);

            builder.AppendData(otherData);
            builder.AppendLineFeed(2);

            builder.AppendDataWithLineFeed(otherData, 2);

            builder.AppendBytesWithLineFeed(bytes, bytes.Length, 2);

            builder.AppendData(otherData);
            builder.AppendUnitFeed(64);

            builder.AppendDataWithUnitFeed(otherData, 64);

            builder.AppendBytesWithUnitFeed(bytes, bytes.Length, 64);

            builder.AppendData(otherDataWithLF);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateCharacterSpaceData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.").AsBuffer();

            builder.BeginDocument();

            builder.AppendCharacterSpace(0);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(1);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(2);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(3);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(4);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(5);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(6);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCharacterSpace(7);
            builder.AppendDataWithLineFeed(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateLineSpaceData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.").AsBuffer();

            builder.BeginDocument();

            builder.AppendLineSpace(32);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendLineSpace(24);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendLineSpace(32);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendLineSpace(24);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateTopMarginData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.").AsBuffer();

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Top margin:2mm*\n").AsBuffer());
            builder.AppendTopMargin(2);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Top margin:6mm*\n").AsBuffer());
            builder.AppendTopMargin(6);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Top margin:11mm*\n").AsBuffer());
            builder.AppendTopMargin(11);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendDataWithLineFeed(otherData);
            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateEmphasisData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData      = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();
            IBuffer otherDataHalf0 = System.Text.Encoding.UTF8.GetBytes("Hello "        ).AsBuffer();
            IBuffer otherDataHalf1 = System.Text.Encoding.UTF8.GetBytes(      "World.\n").AsBuffer();

            byte[] bytes      = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };
            byte[] bytesHalf0 = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20 };
            byte[] bytesHalf1 =                                     { 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendEmphasis(true);
            builder.AppendData(otherData);
            builder.AppendEmphasis(false);
            builder.AppendData(otherData);

            builder.AppendDataWithEmphasis(otherData);
            builder.AppendData(otherData);

            builder.AppendBytesWithEmphasis(bytes, bytes.Length);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendDataWithEmphasis(otherDataHalf0);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithEmphasis(bytesHalf1, bytesHalf1.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }


        public static IBuffer CreateInvertData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData      = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();
            IBuffer otherDataHalf0 = System.Text.Encoding.UTF8.GetBytes("Hello ").AsBuffer();
            IBuffer otherDataHalf1 = System.Text.Encoding.UTF8.GetBytes(      "World.\n").AsBuffer();

            byte[] bytes      = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };
            byte[] bytesHalf0 = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20 };
            byte[] bytesHalf1 =                                     { 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendInvert(true);
            builder.AppendData(otherData);
            builder.AppendInvert(false);
            builder.AppendData(otherData);

            builder.AppendDataWithInvert(otherData);
            builder.AppendData(otherData);

            builder.AppendBytesWithInvert(bytes, bytes.Length);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendDataWithInvert(otherDataHalf0);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithInvert(bytesHalf1, bytesHalf1.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }


        public static IBuffer CreateUnderLineData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData      = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();
            IBuffer otherDataHalf0 = System.Text.Encoding.UTF8.GetBytes("Hello ").AsBuffer();
            IBuffer otherDataHalf1 = System.Text.Encoding.UTF8.GetBytes(      "World.\n").AsBuffer();

            byte[] bytes      = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };
            byte[] bytesHalf0 = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20 };
            byte[] bytesHalf1 =                                     { 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendUnderLine(true);
            builder.AppendData(otherData);
            builder.AppendUnderLine(false);
            builder.AppendData(otherData);

            builder.AppendDataWithUnderLine(otherData);
            builder.AppendData(otherData);

            builder.AppendBytesWithUnderLine(bytes, bytes.Length);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendDataWithUnderLine(otherDataHalf0);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithUnderLine(bytesHalf1, bytesHalf1.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateMultipleData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData      = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();
            IBuffer otherDataHalf0 = System.Text.Encoding.UTF8.GetBytes("Hello ").AsBuffer();
            IBuffer otherDataHalf1 = System.Text.Encoding.UTF8.GetBytes(      "World.\n").AsBuffer();

            byte[] bytes      = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };
            byte[] bytesHalf0 = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20 };
            byte[] bytesHalf1 =                                     { 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendMultiple(2, 2);
            builder.AppendData(otherData);
            builder.AppendMultiple(1, 1);
            builder.AppendData(otherData);

            builder.AppendDataWithMultiple(otherData, 2, 2);
            builder.AppendData(otherData);

            builder.AppendBytesWithMultiple(bytes, bytes.Length, 2, 2);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendDataWithMultiple(otherDataHalf0, 2, 2);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithMultiple(bytesHalf1, bytesHalf1.Length, 2, 2);

            builder.AppendMultipleHeight(2);
            builder.AppendData(otherData);
            builder.AppendMultipleHeight(1);
            builder.AppendData(otherData);

            builder.AppendDataWithMultipleHeight(otherDataHalf0, 2);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithMultipleHeight(bytesHalf1, bytesHalf1.Length, 2);

            builder.AppendMultipleWidth(2);
            builder.AppendData(otherData);
            builder.AppendMultipleWidth(1);
            builder.AppendData(otherData);

            builder.AppendDataWithMultipleWidth(otherDataHalf0, 2);
            builder.AppendData(otherDataHalf1);

            builder.AppendBytes(bytesHalf0, bytesHalf0.Length);
            builder.AppendBytesWithMultipleWidth(bytesHalf1, bytesHalf1.Length, 2);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateAbsolutePositionData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendAbsolutePosition(40);
            builder.AppendData(otherData);
            builder.AppendData(otherData);

            builder.AppendDataWithAbsolutePosition(otherData, 40);
            builder.AppendData(otherData);

            builder.AppendBytesWithAbsolutePosition(bytes, bytes.Length, 40);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateAlignmentData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            builder.BeginDocument();

            builder.AppendData(otherData);

            builder.AppendAlignment(AlignmentPosition.Center);
            builder.AppendData(otherData);
            builder.AppendAlignment(AlignmentPosition.Right);
            builder.AppendData(otherData);
            builder.AppendAlignment(AlignmentPosition.Left);
            builder.AppendData(otherData);

            builder.AppendDataWithAlignment(otherData, AlignmentPosition.Center);
            builder.AppendDataWithAlignment(otherData, AlignmentPosition.Right);
            builder.AppendData(otherData);

            builder.AppendBytesWithAlignment(bytes, bytes.Length, AlignmentPosition.Center);
            builder.AppendBytesWithAlignment(bytes, bytes.Length, AlignmentPosition.Right);
            builder.AppendBytes(bytes, bytes.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateHorizontalTabData(Emulation emulation)
        {
            byte[] data1      = System.Text.Encoding.UTF8.GetBytes("QTY\tITEM\tTOTAL\n");
            byte[] data2      = System.Text.Encoding.UTF8.GetBytes("1\tApple\t1.50\n");
            byte[] data3      = System.Text.Encoding.UTF8.GetBytes("2\tOrange\t2.00\n");
            byte[] data4      = System.Text.Encoding.UTF8.GetBytes("3\tBanana\t3.00\n");

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendHorizontalTabPosition(new int[] {5,24});
        
            builder.AppendData(Encoding.UTF8.GetBytes("*Tab Position:5,24*\n").AsBuffer());
            builder.AppendBytes(data1, data1.Length);
            builder.AppendBytes(data2, data2.Length);
            builder.AppendBytes(data3, data3.Length);
            builder.AppendBytes(data4, data4.Length);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }
        public static IBuffer CreateLogoData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Normal*\n").AsBuffer());
            builder.AppendLogo(Logosize.Normal, 1);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Double Width*\n").AsBuffer());
            builder.AppendLogo(Logosize.DoubleWidth, 1);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Double Height*\n").AsBuffer());
            builder.AppendLogo(Logosize.DoubleHeight, 1);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Double Width and Double Height*\n").AsBuffer());
            builder.AppendLogo(Logosize.DoubleWidthDoubleHeight, 1);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateCutPaperData(Emulation emulation)
        {
            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.FullCut);

            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCut);

            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.FullCutWithFeed);

            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);
            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreatePeripheralData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendPeripheral(PeripheralChannel.No1);
            builder.AppendPeripheral(PeripheralChannel.No2);
            builder.AppendPeripheral(PeripheralChannel.No1, 2000);
            builder.AppendPeripheral(PeripheralChannel.No2, 2000);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateSoundData(Emulation emulation)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendSound(SoundChannel.No1);
            builder.AppendSound(SoundChannel.No2);
            builder.AppendSound(SoundChannel.No1, 3);
            builder.AppendSound(SoundChannel.No2, 3);
            builder.AppendSound(SoundChannel.No1, 1, 1000, 1000);
            builder.AppendSound(SoundChannel.No2, 1, 1000, 1000);

            builder.EndDocument();

            return builder.GetCommands();
        }

        private static async Task<BitmapDecoder> CreateCouponImageAsync(string resouceName)
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

        public static async Task<IBuffer> CreateBitmapDataAsync(Emulation emulation, uint width)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            BitmapDecoder sphereBitmapDecoder   = await CreateCouponImageAsync("sphere_image.png");
            BitmapDecoder starLogoBitmapDecoder = await CreateCouponImageAsync("star_logo_image.png");

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*diffusion:true*\n").AsBuffer());
            await builder.AppendBitmapAsync(sphereBitmapDecoder, true);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*diffusion:false*\n").AsBuffer());
            await builder.AppendBitmapAsync(sphereBitmapDecoder, false);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Normal*\n").AsBuffer());
            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*width:Full, bothScale:true*\n").AsBuffer());
            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true, width, true);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*width:Full, bothScale:false*\n").AsBuffer());
            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true, width, false);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right90*\n").AsBuffer());
            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Right90);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right180*\n").AsBuffer());
            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Rotate180);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Left90*\n").AsBuffer());
            //await builder.AppendBitmapAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Left90);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Normal,    AbsolutePosition:40*\n").AsBuffer());
            await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, 40);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right90,   AbsolutePosition:40*\n").AsBuffer());
            //await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Right90, 40);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Rotate180, AbsolutePosition:40*\n").AsBuffer());
            //await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Rotate180, 40);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Left90,    AbsolutePosition:40*\n").AsBuffer());
            //await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Left90, 40);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Normal,    Alignment:Center*\n").AsBuffer());
            await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, AlignmentPosition.Center);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right90,   Alignment:Center*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Right90, AlignmentPosition.Center);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Rotate180, Alignment:Center*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Rotate180, AlignmentPosition.Center);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Left90,    Alignment:Center*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, SCBBitmapConverterRotation.Left90, SCBAlignmentPosition.Center);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Normal,    Alignment:Right*\n").AsBuffer());
            await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, AlignmentPosition.Right);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right90,   Alignment:Right*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Right90, AlignmentPosition.Right);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Rotate180, Alignment:Right*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Rotate180, AlignmentPosition.Right);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Left90,    Alignment:Right*\n").AsBuffer());
            //await builder.AppendBitmapWithAlignmentAsync(starLogoBitmapDecoder, true, BitmapConverterRotation.Left90, AlignmentPosition.Right);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

//            builder.EndDocument();


            return builder.GetCommands();
        }

        public static IBuffer CreateBarcodeData(Emulation emulation)
        {
            IBuffer otherDataUpcE     = System.Text.Encoding.UTF8.GetBytes("01234500006" ).AsBuffer();
            IBuffer otherDataUpcA     = System.Text.Encoding.UTF8.GetBytes("01234567890" ).AsBuffer();
            IBuffer otherDataJan8     = System.Text.Encoding.UTF8.GetBytes("0123456"     ).AsBuffer();
            IBuffer otherDataJan13    = System.Text.Encoding.UTF8.GetBytes("012345678901").AsBuffer();
            IBuffer otherDataCode39   = System.Text.Encoding.UTF8.GetBytes("0123456789"  ).AsBuffer();
            IBuffer otherDataItf      = System.Text.Encoding.UTF8.GetBytes("0123456789"  ).AsBuffer();
            IBuffer otherDataCode128  = System.Text.Encoding.UTF8.GetBytes("{B0123456789").AsBuffer();
            IBuffer otherDataCode93   = System.Text.Encoding.UTF8.GetBytes("0123456789"  ).AsBuffer();
            IBuffer otherDataNw7      = System.Text.Encoding.UTF8.GetBytes("A0123456789B").AsBuffer();


            byte[] bytesUpcE = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x30, 0x30, 0x30, 0x30, 0x36 };
            byte[] bytesUpcA = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };
            byte[] bytesJan8 = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
            byte[] bytesJan13 = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31 };
            byte[] bytesCode39 = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            byte[] bytesItf = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            byte[] bytesCode128 = { 0x7b, 0x42, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            byte[] bytesCode93 = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
            byte[] bytesNw7 = { 0x41, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x42 };


            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*UPCE*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*UPCA*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcA, BarcodeSymbology.UPCA, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*JAN8*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataJan8, BarcodeSymbology.JAN8, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*JAN13*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataJan13, BarcodeSymbology.JAN13, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code39*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataCode39, BarcodeSymbology.Code39, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*ITF*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataItf, BarcodeSymbology.ITF, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code128*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataCode128, BarcodeSymbology.Code128, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code93*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataCode93, BarcodeSymbology.Code93, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*NW7*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataNw7, BarcodeSymbology.NW7, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);


            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*UPCE*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*UPCA*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcA, bytesUpcA.Length, BarcodeSymbology.UPCA, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*JAN8*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesJan8, bytesJan8.Length, BarcodeSymbology.JAN8, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*JAN13*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesJan13, bytesJan13.Length, BarcodeSymbology.JAN13, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code39*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesCode39, bytesCode39.Length, BarcodeSymbology.Code39, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*ITF*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesItf, bytesItf.Length, BarcodeSymbology.ITF, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code128*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesCode128, bytesCode128.Length, BarcodeSymbology.Code128, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Code93*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesCode93, bytesCode93.Length, BarcodeSymbology.Code93, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*NW7*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesNw7, bytesNw7.Length, BarcodeSymbology.NW7, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);


            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*HRI:false*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, false);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:1*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:2*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode2, 40, true);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:3*\n").AsBuffer());
            builder.AppendBarcodeData(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode3, 40, true);
            builder.AppendUnitFeed(32);


            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*HRI:false*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, false);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:1*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:2*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode2, 40, true);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Mode:3*\n").AsBuffer());
            //builder.AppendBarcodeBytes(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode3, 40, true);
            //builder.AppendUnitFeed(32);



            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            builder.AppendBarcodeDataWithAbsolutePosition(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, 40);
            builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            //builder.AppendBarcodeBytesWithAbsolutePosition(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, 40);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            builder.AppendBarcodeDataWithAlignment(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, AlignmentPosition.Center);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            builder.AppendBarcodeDataWithAlignment(otherDataUpcE, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, AlignmentPosition.Right);
            builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            //builder.AppendBarcodeBytesWithAlignment(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, AlignmentPosition.Center);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            //builder.AppendBarcodeBytesWithAlignment(bytesUpcE, bytesUpcE.Length, BarcodeSymbology.UPCE, BarcodeWidth.Mode1, 40, true, AlignmentPosition.Right);
            //builder.AppendUnitFeed(32);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreatePdf417Data(Emulation emulation)
        {
            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Module:2*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 1, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Module:4*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 1, Pdf417Level.ECC0, 4, 2);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Module:2*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 1, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Module:4*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 1, Pdf417Level.ECC0, 4, 2);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Column:2*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 2, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Column:4*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 4, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Column:2*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 2, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Column:4*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 4, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);


            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Line:10*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 10, 0, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Line:40*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 40, 0, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);


            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Line:10*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 10, 0, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Line:40*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 40, 0, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);


            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Level:ECC0*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 7, Pdf417Level.ECC0, 2, 2);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Level:ECC8*\n").AsBuffer());
            builder.AppendPdf417Data(otherData, 0, 7, Pdf417Level.ECC8, 2, 2);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Level:ECC0*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 7, Pdf417Level.ECC0, 2, 2);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Level:ECC8*\n").AsBuffer());
            //builder.AppendPdf417Bytes(bytes, bytes.Length, 0, 7, Pdf417Level.ECC8, 2, 2);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            builder.AppendPdf417DataWithAbsolutePosition(otherData, 0, 1, Pdf417Level.ECC0, 2, 2, 40);
            builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            //builder.AppendPdf417BytesWithAbsolutePosition(bytes, bytes.Length, 0, 1, Pdf417Level.ECC0, 2, 2, 40);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            builder.AppendPdf417DataWithAlignment(otherData, 0, 1, Pdf417Level.ECC0, 2, 2, AlignmentPosition.Center);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            builder.AppendPdf417DataWithAlignment(otherData, 0, 1, Pdf417Level.ECC0, 2, 2, AlignmentPosition.Right);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            //builder.AppendPdf417BytesWithAlignment(bytes, bytes.Length, 0, 1, Pdf417Level.ECC0, 2, 2, AlignmentPosition.Center);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            //builder.AppendPdf417BytesWithAlignment(bytes, bytes.Length, 0, 1, Pdf417Level.ECC0, 2, 2, AlignmentPosition.Right);
            //builder.AppendUnitFeed(32);


            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }


        public static IBuffer CreateQrCodeData(Emulation emulation)
        {
            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            byte[] bytes = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x0a };

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Cell:2*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.L, 2);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Cell:8*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.L, 8);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Cell:2*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 2);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Cell:8*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 8);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:L*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.L, 4);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:M*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.M, 4);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:Q*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.Q, 4);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:H*\n").AsBuffer());
            builder.AppendQrCodeData(otherData, QrCodeModel.No2, QrCodeLevel.H, 4);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:L*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 4);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:M*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.M, 4);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:Q*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.Q, 4);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Level:H*\n").AsBuffer());
            //builder.AppendQrCodeBytes(bytes, bytes.Length, QrCodeModel.No2,QrCodeLevel.H, 4);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            builder.AppendQrCodeDataWithAbsolutePosition(otherData, QrCodeModel.No2, QrCodeLevel.L, 4, 40);
            builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*AbsolutePosition:40*\n").AsBuffer());
            //builder.AppendQrCodeBytesWithAbsolutePosition(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 4, 40);
            //builder.AppendUnitFeed(32);

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            builder.AppendQrCodeDataWithAlignment(otherData, QrCodeModel.No2, QrCodeLevel.L, 4, AlignmentPosition.Center);
            builder.AppendUnitFeed(32);
            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            builder.AppendQrCodeDataWithAlignment(otherData, QrCodeModel.No2, QrCodeLevel.L, 4, AlignmentPosition.Right);
            builder.AppendUnitFeed(32);

            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Center*\n").AsBuffer());
            //builder.AppendQrCodeBytesWithAlignment(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 4, AlignmentPosition.Center);
            //builder.AppendUnitFeed(32);
            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Alignment:Right*\n").AsBuffer());
            //builder.AppendQrCodeBytesWithAlignment(bytes, bytes.Length, QrCodeModel.No2, QrCodeLevel.L, 4, AlignmentPosition.Right);
            //builder.AppendUnitFeed(32);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static IBuffer CreateBlackMarkData(Emulation emulation, BlackMarkType type)
        {
            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendBlackMark(type);

            builder.AppendData(otherData);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            //builder.AppendBlackMark(BlackMarkType.Invalid);

            builder.EndDocument();

            return builder.GetCommands();
        }

        public static async Task<IBuffer> CreatePageModeData(Emulation emulation, int width)
        {
            IBuffer otherData = System.Text.Encoding.UTF8.GetBytes("Hello World.\n").AsBuffer();

            BitmapDecoder starLogoBitmapDecoder = await CreateCouponImageAsync("star_logo_image.png");

            int height = 30 * 8; //30mm

            PageModePrintRegion printRegion = null;

            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            builder.BeginDocument();

            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Normal*\n").AsBuffer());

            printRegion = new PageModePrintRegion(0, 0, width, height);

            builder.BeginPageMode(printRegion, BitmapConverterRotation.Normal);

            builder.AppendData(otherData);

            builder.AppendPageModeVerticalAbsolutePosition(160);

            builder.AppendData(otherData);

            builder.AppendPageModeVerticalAbsolutePosition(80);

            builder.AppendDataWithAbsolutePosition(otherData, 40);

            builder.EndPageMode();


            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Right90*\n").AsBuffer());

            ////printRegion = new PageModePrintRegion(0, 0, width, height);
            //printRegion = new PageModePrintRegion(0, 0, height, width);
            printRegion = new PageModePrintRegion(0, 0, width, width);

            builder.BeginPageMode(printRegion, BitmapConverterRotation.Right90);

            builder.AppendData(otherData);

            builder.AppendPageModeVerticalAbsolutePosition(160);

            builder.AppendData(otherData);

            builder.AppendPageModeVerticalAbsolutePosition(80);

            builder.AppendDataWithAbsolutePosition(otherData, 40);

            builder.EndPageMode();


            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Rotate180*\n").AsBuffer());

            //printRegion = new PageModePrintRegion(0, 0, width, height);

            //builder.BeginPageMode(printRegion, BitmapConverterRotation.Rotate180);

            //builder.AppendData(otherData);

            //builder.AppendPageModeVerticalAbsolutePosition(160);

            //builder.AppendData(otherData);

            //builder.AppendPageModeVerticalAbsolutePosition(80);

            //builder.AppendDataWithAbsolutePosition(otherData, 40);

            //builder.EndPageMode();



            //builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Left90*\n").AsBuffer());

            ////printRegion = new PageModePrintRegion(0, 0, width, height);
            //printRegion = new PageModePrintRegion(0, 0, height, width);

            //builder.BeginPageMode(printRegion, BitmapConverterRotation.Left90);

            //builder.AppendData(otherData);

            //builder.AppendPageModeVerticalAbsolutePosition(160);

            //builder.AppendData(otherData);

            //builder.AppendPageModeVerticalAbsolutePosition(80);

            //builder.AppendDataWithAbsolutePosition(otherData, 40);

            //builder.EndPageMode();



            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Mixed Text*\n").AsBuffer());

            //printRegion = new PageModePrintRegion(0, 0, width, height);
            printRegion = new PageModePrintRegion(0, 0, width, width);

            builder.BeginPageMode(printRegion, BitmapConverterRotation.Normal);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            builder.AppendDataWithAbsolutePosition(otherData, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Right90);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            builder.AppendDataWithAbsolutePosition(otherData, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Rotate180);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            builder.AppendDataWithAbsolutePosition(otherData, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Left90);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            builder.AppendDataWithAbsolutePosition(otherData, width / 2);

            builder.EndPageMode();



            builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("\n*Mixed Bitmap*\n").AsBuffer());

            //printRegion = new PageModePrintRegion(0, 0, width, height);
            printRegion = new PageModePrintRegion(0, 0, width, width);

            builder.BeginPageMode(printRegion, BitmapConverterRotation.Normal);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Right90);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Rotate180);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, width / 2);

            builder.AppendPageModeRotation(BitmapConverterRotation.Left90);

            builder.AppendPageModeVerticalAbsolutePosition(width / 2);

            await builder.AppendBitmapWithAbsolutePositionAsync(starLogoBitmapDecoder, true, width / 2);

            builder.EndPageMode();

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);


            builder.EndDocument();

            return builder.GetCommands();
        }

        public static async Task<IBuffer> CreatePrintableAreaData(Emulation emulation, PrintableAreaType type)
        {
            ICommandBuilder builder = StarIO_Extension.StarIoExt.CreateCommandBuilder(emulation);

            BitmapDecoder starLogoBitmapDecoder = await CreateCouponImageAsync("PrintableArea_image.png");

            IBuffer otherData1 = System.Text.Encoding.UTF8.GetBytes("123456789").AsBuffer();
            IBuffer otherData2 = System.Text.Encoding.UTF8.GetBytes("0").AsBuffer();

            builder.BeginDocument();

            builder.AppendPrintableArea(type);

            switch (type)
            {
                default:
//              case PrintableAreaType.Standard:
                    builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Standard*\n").AsBuffer());
                    break;
                case PrintableAreaType.Type1:
                    builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Type1*\n").AsBuffer());
                    break;
                case PrintableAreaType.Type2:
                    builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Type2*\n").AsBuffer());
                    break;
                case PrintableAreaType.Type3:
                    builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Type3*\n").AsBuffer());
                    break;
                case PrintableAreaType.Type4:
                    builder.AppendData(Encoding.GetEncoding("ASCII").GetBytes("*Type4*\n").AsBuffer());
                    break;
            }

            await builder.AppendBitmapAsync(starLogoBitmapDecoder, true);

            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);
            builder.AppendData(otherData1);
            builder.AppendDataWithInvert(otherData2);

            builder.AppendCutPaper(CutPaperAction.PartialCutWithFeed);

            builder.EndDocument();

            return builder.GetCommands();
        }
    }
}
