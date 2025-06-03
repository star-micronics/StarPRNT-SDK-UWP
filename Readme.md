# StarPRNT SDK UWP C#

This package contains StarPRNT SDK for supporting to develop applications for Star printers.

## Scope

Please refer to the [StarPRNT SDK](https://www.star-m.jp/products/s_print/sdk/starprnt_sdk/manual/uwp_csharp/en/index.html) document for supported OS, development environment, and supported printers.

## important

### Considerations when using mC-Label2

| Printer                             | Paper Size            | DPI                   |
| ----------------------------------- | --------------------- | --------------------- |
| mC-Label2                           | 2 inch (576 dots)     | 300dpi                |
| mC-Print2, mPOP, etc                | 2 inch (384 dots)     | 203dpi                |
| mC-Label3, mC-Print3, TSP100IV, etc | 3 inch (576 dots)     | 203dpi                |

Due to the differences in DPI above, the sample code based on a "3-inch (576 dots)" paper size will print on a 2-inch paper size in mC-Label2.

## Note

1.  If mC-Sound was connected after the printer power was turned ON, melody speaker API does not work properly. Please turn on the printer after connecting mC-Sound to it.

## Copyright

Copyright 2016-2025 Star Micronics Co., Ltd. All rights reserved.
