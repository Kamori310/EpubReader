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
    
    public CentralFileHeader(
        byte[] versionMadeBy, 
        byte[] versionNeededToExtract, 
        byte[] generalPurposeBitFlag,
        byte[] compressionMethod, 
        byte[] lastModFileTime, 
        byte[] lastModFileDate, 
        byte[] crc32, 
        byte[] compressedSize,
        byte[] uncompressedSize, 
        byte[] fileNameLength, 
        byte[] extraFieldLength, 
        byte[] fileCommentLength,
        byte[] diskNumberStart, 
        byte[] internalFileAttributes, 
        byte[] externalFileAttributes,
        byte[] relativeOffsetOfLocalHeader, 
        byte[] fileName, 
        byte[] extraField, 
        byte[] fileComment)
    {
        VersionMadeBy = versionMadeBy;
        VersionNeededToExtract = versionNeededToExtract;
        GeneralPurposeBitFlag = generalPurposeBitFlag;
        CompressionMethod = compressionMethod;
        LastModFileTime = lastModFileTime;
        LastModFileDate = lastModFileDate;
        Crc32 = crc32;
        CompressedSize = compressedSize;
        UncompressedSize = uncompressedSize;
        FileNameLength = fileNameLength;
        ExtraFieldLength = extraFieldLength;
        FileCommentLength = fileCommentLength;
        DiskNumberStart = diskNumberStart;
        InternalFileAttributes = internalFileAttributes;
        ExternalFileAttributes = externalFileAttributes;
        RelativeOffsetOfLocalHeader = relativeOffsetOfLocalHeader;
        FileName = fileName;
        ExtraField = extraField;
        FileComment = fileComment;
    }
}