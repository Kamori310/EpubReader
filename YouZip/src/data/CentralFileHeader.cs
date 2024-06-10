using System.Text;
using YouZip.Extensions;

namespace YouZip.data;

public struct CentralFileHeader
{
    public static readonly byte[] CentralFileHeaderSignature = [0x50, 0x4b, 0x01, 0x02];
    
    // ReSharper disable once MemberCanBePrivate.Global
    public const int CentralFileHeaderSignatureLength = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int VersionMadeByLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int VersionNeededToExtractLength = 2;
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
    // ReSharper disable once MemberCanBePrivate.Global
    public const int FileCommentLengthLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int DiskNumberStartLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int InternalFileAttributesLength = 2;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int ExternalFileAttributesLength = 4;
    // ReSharper disable once MemberCanBePrivate.Global
    public const int RelativeOffsetOfLocalHeaderLength = 4;

    public ushort VersionMadeBy { get; }
    public ushort VersionNeededToExtract { get; }
    public bool[] GeneralPurposeBitFlag { get; }
    public ushort CompressionMethod { get; }
    public DateTime LastModFileDateTime => CalculateLastModifiedDateTime();
    public byte[] Crc32 { get; }
    public uint CompressedSize { get; }
    public uint UncompressedSize { get; }
    public ushort FileNameLength { get; }
    public ushort ExtraFieldLength { get; }
    public ushort FileCommentLength { get; }
    public ushort DiskNumberStart { get; }
    public byte[] InternalFileAttributes { get; }
    public byte[] ExternalFileAttributes { get; }
    public uint RelativeOffsetOfLocalHeader { get; }
    public string FileName { get; }
    public string ExtraField { get; }
    public string FileComment { get; }
    
    private readonly byte[] _lastModFileTime;
    private readonly byte[] _lastModFileDate;

    public CentralFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += CentralFileHeaderSignatureLength;

        VersionMadeBy = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += VersionMadeByLength;

        VersionNeededToExtract = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += VersionNeededToExtractLength;
        
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

        // _fileNameLength = input.Subarray(startingPosition, FileNameLengthLength);
        FileNameLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += FileNameLengthLength;

        ExtraFieldLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += ExtraFieldLengthLength;

        FileCommentLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += FileCommentLengthLength;

        DiskNumberStart = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += DiskNumberStartLength;

        InternalFileAttributes = input.Subarray(startingPosition, InternalFileAttributesLength);
        startingPosition += InternalFileAttributesLength;

        ExternalFileAttributes = input.Subarray(startingPosition, ExternalFileAttributesLength);
        startingPosition += ExternalFileAttributesLength;
        
        // _relativeOffsetOfLocalHeader = input.Subarray(startingPosition, RelativeOffsetOfLocalHeaderLength);
        RelativeOffsetOfLocalHeader = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += RelativeOffsetOfLocalHeaderLength;

        FileName = Encoding.UTF8.GetString(input.Subarray(startingPosition, FileNameLength));
        startingPosition += FileNameLength;

        ExtraField = Encoding.UTF8.GetString(input.Subarray(startingPosition, ExtraFieldLength));
        startingPosition += ExtraFieldLength;

        FileComment = Encoding.UTF8.GetString(input.Subarray(startingPosition, FileCommentLength));
    }

    public int HeaderLength()
    {
        return
            CentralFileHeaderSignatureLength +
            VersionMadeByLength +
            VersionNeededToExtractLength +
            GeneralPurposeBitFlagLength +
            CompressionMethodLength +
            LastModFileTimeLength +
            LastModFileDateLength +
            Crc32Length +
            CompressedSizeLength +
            UncompressedSizeLength +
            FileNameLengthLength +
            ExtraFieldLengthLength +
            FileCommentLengthLength +
            DiskNumberStartLength +
            InternalFileAttributesLength +
            ExternalFileAttributesLength +
            RelativeOffsetOfLocalHeaderLength +
            FileNameLength +
            ExtraFieldLength +
            FileCommentLength;
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