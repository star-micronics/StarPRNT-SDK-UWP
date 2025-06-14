# StarPRNT SDK UWP C#

本パッケージはStarプリンタを使用するアプリケーションの開発を容易にするためのSDKです。

## 適用

対応OS・開発環境・対応プリンターについては、[StarPRNT SDKの開発者向けドキュメント](https://www.star-m.jp/products/s_print/sdk/starprnt_sdk/manual/uwp_csharp/en/index.html)を参照ください。

## 重要事項

### mC-Label2を使用する際の留意点

| プリンタ                              | 用紙サイズ             | DPI                   |
| ------------------------------------- | --------------------- | --------------------- |
| mC-Label2                             | 2インチ (576ドット)    | 300dpi                |
| mC-Print2、mPOP、その他                | 2インチ (384ドット)    | 203dpi                |
| mC-Label3、mC-Print3、TSP100IV、その他 | 3インチ (576ドット)    | 203dpi                |

上記の解像度の違いにより、mC-Label2では「3インチ (576ドット)」の用紙サイズに基づいたサンプルコードを使用すると、2インチの用紙サイズで印刷されます。

## 注意事項

1. プリンターの電源をONした後にプリンターにmC-Soundを接続した場合、メロディスピーカーのAPIは正常に動作しません。プリンターにmC-Soundを接続した後にプリンターの電源をONしてください。

## 著作権

スター精密（株）Copyright 2016-2025
