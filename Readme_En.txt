************************************************************
      StarPRNT SDK Ver 5.16.0 20230331
         Readme_En.txt             Star Micronics Co., Ltd.
************************************************************

    1. Overview
    2. Changes
    3. Contents
    4. Scope
    5. Note
    6. Copyright

===========================
 1. Overview
===========================

    This package contains StarPRNT SDK for supporting to develop applications for Star printers.
    Please refer to the document(documents/UsersManual.url) for details.

==========================
 2. Changes
==========================

    [StarIOPort]
        - Added features
            * Added mC-Label3.
        - Added new Bluetooth module information for SM-S230i, SM-T300i, SM-T300 and SM-T400i.

    [StarIOExtension]
        - Added features
            * Added mC-Label3.

===========================
 3. Contents
===========================

    StarPRNT_UWP_SDK_V5.16.0_20230331
    |- Readme_En.txt                          // Release Notes (English)
    |- Readme_Jp.txt                          // Release Notes (Japanese)
    |- Changelog_En.txt                       // Changelog (English)
    |- Changelog_Jp.txt                       // Changelog (Japanese)
    |- SoftwareLicenseAgreement.pdf           // Software License Agreement (English)
    |- SoftwareLicenseAgreement_Jp.pdf        // Software License Agreement (Japanese)
    |- SoftwareLicenseAgreementAppendix.pdf   // Software License Agreement Appendix (English)
    |
    +- documents
    |  +- UsersManual.url                     // Link to StarPRNT SDK document
    |
    +- software
    |  |- examples
    |  |  | - 0_StarPRNT                      // Sample program for printing and searching function (Ver 5.16.0)
    |  |  | - 1_StarIODeviceSetting           // Sample program for SteadyLAN settings (Ver 1.0.0)
    |  |
    |  +- libs
    |      |- StarIOPort.winmd                // StarIOPort.winmd (Ver 1.8.0)
    |      |- StarIO_Extension.winmd          // StarIO_Extension.winmd (Ver 1.11.0)
    |      +- StarIODeviceSetting.dll         // StarIODeviceSetting.dll (Ver 1.0.0)
    |
    +- tools
        +- StarSoundConverter               // Tools for converting sound format for melody speaker (Ver 1.0.0)

===========================
 4. Scope
===========================

    Please refer to UsersManual.url for supported OS, development environment, and supported printers.

===========================
 5. Note
===========================

    1. If mC-Sound was connected after the printer power was turned ON, melody speaker API does not work properly.
        Please turn on the printer after connecting mC-Sound to it.

===========================
 6. Copyright
===========================

    Copyright 2016-2022 Star Micronics Co., Ltd. All rights reserved.

