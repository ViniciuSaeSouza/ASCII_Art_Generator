# Struct Rgba32

Packed pixel type containing four 8-bit unsigned normalized values ranging from 0 to 255. The color components are stored in red, green, blue, and alpha order (least significant to most significant byte).

Ranges from [0, 0, 0, 0] to [1, 1, 1, 1] in vector form.
##### Implements

[IPixel](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.IPixel-1.html)<[==Rgba32==](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html)>

[IEquatable](https://learn.microsoft.com/dotnet/api/system.iequatable-1)<[==Rgba32==](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html)>

[IPackedVector](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.IPackedVector-1.html)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)>

[IPixel](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.IPixel.html)

##### Inherited Members

[object.Equals(object, object)](https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals\(system-object-system-object\))

[object.GetType()](https://learn.microsoft.com/dotnet/api/system.object.gettype)

[object.ReferenceEquals(object, object)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals)

###### **Namespace**: [SixLabors](https://docs.sixlabors.com/api/ImageSharp/SixLabors.html).[ImageSharp](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.html).[PixelFormats](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.html)

###### **Assembly**: SixLabors.ImageSharp.dll

##### Syntax

```csharp
public struct Rgba32 : IPixel<Rgba32>, IEquatable<Rgba32>, IPackedVector<uint>, IPixel
```

##### **Remarks**

This struct is fully mutable. This is done (against the guidelines) for the sake of performance, as it avoids the need to create new values for modification operations.

### [](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html?q=rgba32#constructors)Constructors

#### [](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html?q=rgba32#SixLabors_ImageSharp_PixelFormats_Rgba32__ctor_System_Byte_System_Byte_System_Byte_)==Rgba32==(byte, byte, byte)

Initializes a new instance of the [==Rgba32==](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html) struct.

##### Declaration

```csharp
public Rgba32(byte r, byte g, byte b)
```

##### Parameters

|Type|Name|Description|
|---|---|---|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|r|The red component.|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|g|The green component.|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|b|The blue component.|

#### [](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html?q=rgba32#SixLabors_ImageSharp_PixelFormats_Rgba32__ctor_System_Byte_System_Byte_System_Byte_System_Byte_)==Rgba32==(byte, byte, byte, byte)

Initializes a new instance of the [==Rgba32==](https://docs.sixlabors.com/api/ImageSharp/SixLabors.ImageSharp.PixelFormats.Rgba32.html) struct.

##### Declaration

```csharp
public Rgba32(byte r, byte g, byte b, byte a)
```

##### Parameters

|Type|Name|Description|
|---|---|---|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|r|The red component.|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|g|The green component.|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|b|The blue component.|
|[byte](https://learn.microsoft.com/dotnet/api/system.byte)|a|The alpha component.|