# Z85 Encoder/Decoder

This C# library provides an implementation of the Z85 encoding and decoding scheme. Z85 is a binary-to-text encoding system that was designed to be efficient both in terms of space and width of the output, while also being human-friendly.
## Installation
```bash
dotnet add package Z85Net
```
## Usage

```csharp
byte[] data = [ 0x86, 0x4F, 0xD2, 0x6F, 0xB5, 0x59, 0xf7, 0x5b ];
string encoded = Z85Encoder.GetString(data);
byte[] decoded = Z85Encoder.GetBytes(encoded);
```

Please note that the input length for the `GetString` method must be a multiple of 4 and for the `GetBytes` method must be a multiple of 5. If it is not, an `ArgumentException` will be thrown.
