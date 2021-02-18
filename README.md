https://github.com/dropoutdude/asn1sharp/workflows/build/badge.svg
# asn1sharp
.Net library for parsing ASN.1 binary code with a simple interface for usage

## Usage

Currently two sources of ASN.1 data are supported.

### Byte array as a source of ASN.1 data
In case you just have binary data you can use the extension Method for `byte[]`.

```C#
byte[] yourData = ....; // You read the bytes yourself

var node = await yourData.ParseBinary()
// var node = await yourData.ParseBinary(CancellationToken.None)

```

### A Stream in PEM format as a source of ASN.1 data
In case you have a stream containing your data in PEM format, you can use the extension to read the stream.

```C#
using var pemStream = ....; // The data from a source in PEM format

var node = pemStream.ParsePem()
// var node = pemStream.ParsePem(CancellationToken.None)
```

### Custom data sources

You can implement a `ReaderSource` and provide your own source of data to the `Asn1Reader` class.
