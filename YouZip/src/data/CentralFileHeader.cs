namespace YouZip.data;

public struct CentralFileHeader
{
    public static readonly byte[] CentralFileHeaderSignature = [0x50, 0x4b, 0x01, 0x02];
    
    public const int CentralFileHeaderSignatureLength = 4;
    public const int VersionMadeByLength = 2;
    public const int VersionNeededToExtractLength = 2;
    public const int GeneralPurposeBitFlagLength = 2;
    public const int CompressionMethodLength = 2;
    public const int LastModFileTimeLength = 2;
    public const int LastModFileDateLength = 2;
    public const int Crc32Length = 4;
    public const int CompressedSizeLength = 4;
    public const int UncompressedSizeLength = 4;
    public const int FileNameLengthLength = 2;
    public const int ExtraFieldLengthLength = 2;
    public const int FileCommentLengthLength = 2;
    public const int DiskNumberStartLength = 2;
    public const int InternalFileAttributesLength = 2;
    public const int ExternalFileAttributesLength = 4;
    public const int RelativeOffsetOfLocalHeaderLength = 4;

    public byte[] VersionMadeBy { get; set; }
    public byte[] VersionNeededToExtract { get; set; }
    public byte[] GeneralPurposeBitFlag { get; set; }
    public byte[] CompressionMethod { get; set; }
    public byte[] LastModFileTime { get; set; }
    public byte[] LastModFileDate { get; set; }
    public byte[] Crc32 { get; set; }
    public byte[] CompressedSize { get; set; }
    public byte[] UncompressedSize { get; set; }
    public byte[] FileNameLength { get; set; }
    public byte[] ExtraFieldLength { get; set; }
    public byte[] FileCommentLength { get; set; }
    public byte[] DiskNumberStart { get; set; }
    public byte[] InternalFileAttributes { get; set; }
    public byte[] ExternalFileAttributes { get; set; }
    public byte[] RelativeOffsetOfLocalHeader { get; set; }
    public byte[] FileName { get; set; }
    public byte[] ExtraField { get; set; }
    public byte[] FileComment { get; set; }

    public CentralFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += CentralFileHeaderSignatureLength;

        VersionMadeBy = input.Subarray(startingPosition, VersionMadeByLength);
        startingPosition += VersionMadeByLength;

        VersionNeededToExtract = input.Subarray(startingPosition, VersionNeededToExtractLength);
        startingPosition += VersionNeededToExtractLength;

        GeneralPurposeBitFlag = input.Subarray(startingPosition, GeneralPurposeBitFlagLength);
        startingPosition += GeneralPurposeBitFlagLength;

        CompressionMethod = input.Subarray(startingPosition, CompressionMethodLength);
        startingPosition += CompressionMethodLength;

        LastModFileTime = input.Subarray(startingPosition, LastModFileTimeLength);
        startingPosition += LastModFileTimeLength;

        LastModFileDate = input.Subarray(startingPosition, LastModFileDateLength);
        startingPosition += LastModFileDateLength;

        Crc32 = input.Subarray(startingPosition, Crc32Length);
        startingPosition += Crc32Length;

        CompressedSize = input.Subarray(startingPosition, CompressedSizeLength);
        startingPosition += CompressedSizeLength;

        UncompressedSize = input.Subarray(startingPosition, UncompressedSizeLength);
        startingPosition += UncompressedSizeLength;

        FileNameLength = input.Subarray(startingPosition, FileNameLengthLength);
        startingPosition += FileNameLengthLength;

        ExtraFieldLength = input.Subarray(startingPosition, ExtraFieldLengthLength);
        startingPosition += ExtraFieldLengthLength;

        FileCommentLength = input.Subarray(startingPosition, FileCommentLengthLength);
        startingPosition += FileCommentLengthLength;

        DiskNumberStart = input.Subarray(startingPosition, DiskNumberStartLength);
        startingPosition += DiskNumberStartLength;

        InternalFileAttributes = input.Subarray(startingPosition, InternalFileAttributesLength);
        startingPosition += InternalFileAttributesLength;

        ExternalFileAttributes = input.Subarray(startingPosition, ExternalFileAttributesLength);
        startingPosition += ExternalFileAttributesLength;

        RelativeOffsetOfLocalHeader = input.Subarray(startingPosition, RelativeOffsetOfLocalHeaderLength);
        startingPosition += RelativeOffsetOfLocalHeaderLength;

        FileName = input.Subarray(startingPosition, FileNameLength.ToUShort());
        startingPosition += FileNameLength.ToUShort();

        ExtraField = input.Subarray(startingPosition, ExtraFieldLength.ToUShort());
        startingPosition += ExtraFieldLength.ToUShort();

        FileComment = input.Subarray(startingPosition, FileCommentLength.ToUShort());
    }
}