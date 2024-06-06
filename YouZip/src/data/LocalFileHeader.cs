using System.Text;

namespace YouZip.data;

public class LocalFileHeader
{
    public static readonly byte[] LocalFileHeaderSignature = [0x50, 0x4b, 0x03, 0x04];
    
    public const int LocalFileHeaderSignatureLength = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int VersionNeedToExtractLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int GeneralPurposeBitFlagLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int CompressionMethodLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int LastModFileTimeLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int LastModFileDateLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int Crc32Length = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int CompressedSizeLength = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int UncompressedSizeLength = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int FileNameLengthLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int ExtraFieldLengthLength = 2;

    private readonly byte[] _lastModFileTime;
    private readonly byte[] _lastModFileDate;
    
    /// <summary>
    /// Saved in ushort format
    /// </summary>
    public ushort VersionNeededToExtract { get; }
    public bool[] GeneralPurposeBitFlag { get; }
    public ushort CompressionMethod { get; }

    public DateTime LastModFileDateTime => CalculateLastModifiedDateTime();

    /// <summary>
    /// Contains the crc-32 hash of the file data
    /// Use System.IO.Hashing
    /// </summary>
    public byte[] Crc32 { get; }
    /// <summary>
    /// Size of file data. Contains size of decryption header if present.
    /// If the archive is in ZIP64 format and value of this field is 0xff_ff_ff_ff
    /// the size will be in the corresponding 8 byte ZIP64 extended information
    /// extra field.  
    /// </summary>
    public uint CompressedSize { get; }
    public uint UncompressedSize { get; }
    public ushort FileNameLength { get; }
    public ushort ExtraFieldLength { get; }
    public string FileName { get; }
    public string ExtraField { get; }
    
    public LocalFileHeader (byte[] input, int startingPosition)
    {
        startingPosition += LocalFileHeaderSignatureLength;
        
        VersionNeededToExtract = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += VersionNeedToExtractLength;
        
        GeneralPurposeBitFlag = CalculateGeneralPurposeBitFlag(input, startingPosition);
        startingPosition += GeneralPurposeBitFlagLength;

        CompressionMethod = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += CompressionMethodLength;

        _lastModFileTime = input.Subarray(startingPosition, LastModFileTimeLength);
        startingPosition += LastModFileTimeLength;

        _lastModFileDate = input.Subarray(startingPosition, LastModFileDateLength);
        startingPosition += LastModFileDateLength;

        Crc32 = input.Subarray(startingPosition, Crc32Length);
        startingPosition += Crc32Length;

        CompressedSize = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += CompressedSizeLength;

        UncompressedSize = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += UncompressedSizeLength;

        FileNameLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += FileNameLengthLength;

        ExtraFieldLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += ExtraFieldLengthLength;

        FileName = Encoding.UTF8.GetString(input.Subarray(startingPosition, FileNameLength));
        startingPosition += FileNameLength;

        ExtraField = Encoding.UTF8.GetString(input.Subarray(startingPosition, ExtraFieldLength));
    }

    public int HeaderLength()
    {
        return
            LocalFileHeaderSignatureLength +
            VersionNeedToExtractLength +
            GeneralPurposeBitFlagLength +
            CompressionMethodLength +
            LastModFileTimeLength +
            LastModFileDateLength +
            Crc32Length +
            CompressedSizeLength +
            UncompressedSizeLength +
            FileNameLengthLength +
            ExtraFieldLengthLength +
            FileNameLength +
            ExtraFieldLength;
    }
    
    private static bool[] CalculateGeneralPurposeBitFlag(byte[] input, int startingPosition)
    { 
        return input
            .Skip(startingPosition)
            .Take(GeneralPurposeBitFlagLength)
            .SelectMany(
                it =>
                    Enumerable
                        .Range(0, 8)
                        .Reverse()
                        .Select(
                            bit => (it & (1 << bit)) != 0))
            .ToArray();
    }

    private DateTime CalculateLastModifiedDateTime() =>
        _lastModFileDate.DateFromMsDosFormat() + _lastModFileTime.TimeFromMsDosFormat();
}