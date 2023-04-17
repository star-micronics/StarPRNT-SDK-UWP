************************************************************
      StarPRNT SDK Ver 5.16.0 20230331
         Readme_Jp.txt                  スター精密（株）
************************************************************

    1. 概要
    2. 変更点
    3. 内容
    4. 適用
    5. 注意事項
    6. 著作権

=============
 1. 概要
=============

    本パッケージはStarプリンタを使用するアプリケーションの開発を容易にするためのSDKです。
    詳細な説明は、ドキュメント(documents/UsersManual.url)を参照ください。

==================================
 2. 変更点
==================================

    [StarIOPort]
        - 機能追加
            * mC-Label3に対応
        - SM-S210i, SM-T300i, SM-T300, SM-T400iのBluetoothモジュール情報を新規追加

    [StarIO_Extension]
        - 機能追加
            * mC-Label3に対応

=============
 3. 内容
=============

    StarPRNT_UWP_SDK_V5.16.0_20230331
    |- Readme_En.txt                          // リリースノート (英語)
    |- Readme_Jp.txt                          // リリースノート (日本語)
    |- Changelog_En.txt                       // 変更履歴 (英語)
    |- Changelog_Jp.txt                       // 変更履歴 (日本語)
    |- SoftwareLicenseAgreement.pdf           // ソフトウエア使用許諾書 (英語)
    |- SoftwareLicenseAgreement_Jp.pdf        // ソフトウエア使用許諾書 (日本語)
    |- SoftwareLicenseAgreementAppendix.pdf   // ソフトウエア使用許諾書付録 (英語)
    |
    +- documents
    |  +- UsersManual.url                     // StarPRNT SDK ドキュメントへのリンク
    |
    +- software
    |  |- examples
    |  |   |- 0_StarPRNT                      // 印刷やプリンターの検索などのサンプルプログラム (Ver 5.16.0)
    |  |   |- 1_StarIODeviceSetting           // SteadyLAN設定サンプルプログラム (Ver 1.0.0)
    |  |   |
    |  +- libs
    |      |- StarIOPort.winmd                // StarIOPort.winmd (Ver 1.8.0)
    |      |- StarIO_Extension.winmd          // StarIO_Extension.winmd (Ver 1.11.0)
    |      +- StarIODeviceSetting.dll         // StarIODeviceSetting.dll (Ver 1.0.0)
    |
    +- tools
        +- StarSoundConverter                 // メロディスピーカー向け音声変換ツール (Ver 1.0.0)

=============
 4. 適用
=============

    対応OS・開発環境・対応プリンターについては、
    ドキュメント(documents/UsersManual.url)を参照ください。

=============
 5. 注意事項
=============

    1. プリンターの電源をONした後にプリンターにmC-Soundを接続した場合、メロディスピーカーのAPIは正常に動作しません。
        プリンターにmC-Soundを接続した後にプリンターの電源をONしてください。

=============
 6. 著作権
=============

    スター精密（株）Copyright 2016-2022

