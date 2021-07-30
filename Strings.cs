using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnlaunchEditor
{
    class Strings
    {
        #region Common Text
        static public byte[] TopScreen_Title;         // 0xE5FE - 'Nocash Unlaunch.dsi v' + version
        static public byte[] BottomScreen_URL;        // 0xE802 - 'http://problemkaputt.de'
        static public byte[] BottomScreen_CustomText; // 'No one is illegal'
        #endregion

        #region Unknown Use Text
        static public byte[] UNK_Error_LoadWifiFirmware; // 'Cannot load Wifi Firmware' - prolly is Wifiboot-related
        #endregion

        #region Main Menu text
        static public byte[] MM_Size_PUB;  // 'PUB Size: '
        static public byte[] MM_Size_PRV;  // 'PRV Size: '
        static public byte[] MM_Cart_None; // 'NO CARTRIDGE'
        static public byte[] MM_Wifiboot;  // 'WIFIBOOT'
        static public byte[] MM_Options;   // 'OPTIONS'
        #endregion

        #region Options text
        static public byte[] OP_Title;     // 'CHANGE BOOT DEFAULT/HOTKEYS:'
        static public byte[] OP_BTN_None;  // 'NO BUTTON'
        static public byte[] OP_BTN_A;     // 'BUTTON A'
        static public byte[] OP_BTN_B;     // 'BUTTON B'
        static public byte[] OP_BTN_X;     // 'BUTTON X'
        static public byte[] OP_BTN_Y;     // 'BUTTON Y'
        static public byte[] OP_BTN_COMBO; // 'BUTTON A+B'
        static public byte[] OP_LoadError; // 'LOAD ERROR'
        static public byte[] OP_ExitSave;  // 'Save & Exit'
        #endregion

        #region WifiBoot text
        // text is made upper when running
        static public byte[] WB_Title;                 // 'nocash wifiboot' + version
        static public byte[] WB_Connecting;            // 'connecting'
        static public byte[] WB_Read_ARM7BIN;          // 'reading arm7 binary'
        static public byte[] WB_Read_ARM9BIN;          // 'reading arm9 binary'
        static public byte[] WB_Read_ARM7iBIN;         // 'reading arm7i binary'
        static public byte[] WB_Read_ARM9iBIN;         // 'reading arm9i binary'
        static public byte[] WB_KBs_Rate;              // 'Kbyte/s'
        static public byte[] WB_Error_Connect;         // 'failed to connect'
        static public byte[] WB_Error_UDPSocket;       // 'upd socket error'
        static public byte[] WB_Error_HeaderRead;      // 'error reading header'
        static public byte[] WB_Error_Received;        // 'receive error'
        static public byte[] WB_Error_BlowfishKey;     // 'Cannot load blowfish key'
        static public byte[] WB_DataSource;            // 'from rom/itcm'
        static public byte[] WB_Connect_IPText;        // 'WifiReconnectDtaLOCAL IP  : ' / 'IP  : ' (unsure)
        static public byte[] WB_Connect_RemoteIPText;  // 'REMOTE IP : '
        static public byte[] WB_Connect_SubnetText;    // 'SUBNET    : '
        static public byte[] WB_Connect_GatewayText;   // 'GATEWAY   : '
        static public byte[] WB_Connect_DNSText;       // 'DNS       : '
        static public byte[] WB_Connect_NYPText;       // 'NTP       : '
        static public byte[] WB_Connect_MACText;       // 'MAC:   '
        static public byte[] WB_Connect_BSSIDText;     // 'BSSID: '
        static public byte[] WB_Connect_SSIDText;      // 'SSID:  '
        static public byte[] WB_Connect_ChannelText;   // 'CHANNEL:'
        static public byte[] WB_Connect_RSSIText;      // 'RSSI:   '
        static public byte[] WB_Connect_CryptText;     // 'CRYPT:  '
        static public byte[] WB_Connect_DomainText;    // 'DOMAIN: '
        static public byte[] WB_Connect_InterfaceText; // 'INTERFACE: .'
        static public byte[] WB_Connect_Console_NDS;   // 'NDS'
        static public byte[] WB_Connect_Console_DSI;   // 'DSi'
        static public byte[] WB_Connect_Console_UNK;   // '???'
        static public byte[] WB_Connect_PortOpenText;  // 'Open' ( i assume its about if a port is open or not)
        static public byte[] WB_ConnectionType_WEP;    // 'WEP'
        static public byte[] WB_ConnectionType_TKIP;   // 'TKIP'
        static public byte[] WB_ConnectionType_AES;    // 'AES' (prolly to encrypt packets or sum idk)
        static public byte[] WB_ConnectionType_WPA;    // 'WPA-'
        static public byte[] WB_ConnectionType_WPA2;   // 'WPA-2'
        static public byte[] WB_Rate_UNK;              // ' BB/RF:'
        static public byte[] WB_DHCP_Discover;         // 'DHCP Discover'
        static public byte[] WB_DHCP_Request;          // 'DHCP Request'
        static public byte[] WB_DHCP_Retry;            // 'DHCP Retry'
        static public byte[] WB_DHCP_Release;          // 'DHCP Release'
        static public byte[] WB_PMK_ExpandText;        // 'Pairwise key expansion'
        static public byte[] WB_PMK_Name;              // 'PMK Name'
        #endregion

        #region Installation Menu text
        static public byte[] Installation_Selection1; // 'How to install'
        static public byte[] Installation_Selection2; // 'How to use'
        static public byte[] Installation_Selection3; // 'How to works'
        static public byte[] Installation_Selection4; // 'Install now'
        static public byte[] Installation_Selection5; // 'Power down'
        static public byte[] Installation_Selection6; // 'Uninstall'
        #endregion

        #region "How to use" text
        static public byte[] HTU_WhenInsatlled; // 'When installed. unlaunch takes control almost immediately after power-up. before even executing the boot menu (aka launcher). Unlaunch can start titles from: - SD Card. internal eMMC memory - DS Cart slot. and Wifiboot. Title loading features are: - Unlicensed/homebreaw titles - Full SCFG access rights - Region free. no healthsafety'
        static public byte[] HTU_OptionsDesc;   // 'Options allow to select the default boot title. and to set hotkeys for commonly used files. Button A+B forces filemenu (and allows to change options).'
        #endregion

        #region "How it works" text
        static public byte[] HIW_Section1; // 'Bootstage 2 is loading the launcher's TITLE.TMD file to memory. that's done without any FILESIZE>LIMIT check (it's only checking FILESIZE>FILESIZE).'
        static public byte[] HIW_Section2; // 'That is allowing to load about 80Kbytes of usefull code. and to overwrite a task switching structure. causing ARM9 to execute the loaded code. which can then tweak ARM7 to execute custom code by remapping some potions of shared VRAM.'
        static public byte[] HIW_Section3; // 'Yup. it's actually that simple. The bigger problem has been to find this exploits within the 400.000 lines of code that bootstages 2 and 3 consist of.'
        #endregion

        #region "How to install" text
        static public byte[] HTI_Title;                // 0xE899 - 'There are 2 installation methods'
        static public byte[] HTI_AutomaticMethodTitle; // 'AUTOMATIC via DSIware exploits'
        static public byte[] HTI_AutomaticMethodDesc;  // 'Simply start unlaunch.dsi and select Install now in the menu.'
        static public byte[] HTI_ManualMethodTitle;    // 'MANUAL INSTALL via hardmods'
        static public byte[] HTI_ManualMethodDesc;     // 'locate 520-byte title.tmd file in the following folder: title\00030017\484E41xx\content (the xx varies per region) append the unlaunch.dsi file at the end of the tmd file. Important: .Set Read-Only attr for all files in above folder.'
        static public byte[] HTI_BothMethods;          // 'Both methods'
        static public byte[] HTI_BothMethodsDesc;      // 'are working on all known DSi models. regardless of region or firmware version. For uninstallation truncate the tmd file back to 520-byte size.'
        #endregion

        #region "Install now / Uninstall" text
        static public byte[] ErrorText;    // 'Error:'
        static public byte[] ErrorDSIMode; // 'Requires DSi (in DSi mode)'
        static public byte[] ErrorMBR;     // 'Bad MBR'

        // 'Install now' specific text
        static public byte[] IN_WriteProtectingFiles; // 'Write-Protecting files'
        static public byte[] IN_WriteTMD;             // 'Writing launcher tmd'
        static public byte[] IN_Finished;             // 'Installation complete'

        // used in both 'Install now' & "Uninstall"
        static public byte[] INUN_MountingEMMC; // 'Mounting eMMC drive'
        static public byte[] INUN_LoadBootInfo; // 'Loading boot info'
        static public byte[] INUN_LoadFAT;      // 'Loading FAT'
        static public byte[] INUN_LoadHWInfo;   // 'Loading hwinfo_s'
        static public byte[] INUN_LoadTMD;      // 'Loading launcher tmd'
        static public byte[] INUN_AdjFilesize;  // 'Adjusting filesize'
        static public byte[] INUN_AdjClChain;   // 'Adjusting cluster chain'
        static public byte[] INUN_FATWriting;   // 'Writing FAT'

        // 'Uninstall' specific text
        static public byte[] UN_Warning_Section1; // 'Warning: Uninstalling unlaunch will render your console mostly useless.'
        static public byte[] UN_Warning_Section2; // 'Press X+Y to confirm that you want to continue. other buttons to cancel.'
        static public byte[] UN_Warning_Section3; // 'You can reinstall unlaunch via flipnote or hardmod (if you have either one).'
        static public byte[] UN_UnprotectFiles;   // 'Unprotecting files'
        static public byte[] UN_Finished;         // 'Uninstall complete'
        #endregion
    }
}
