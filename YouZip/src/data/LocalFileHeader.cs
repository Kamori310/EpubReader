namespace YouZip.data;

public struct LocalFileHeader
{
    public static readonly byte[] LocalFileHeaderSignature = [0x50, 0x4b, 0x03, 0x04];
    
    public const int LocalFileHeaderSignatureLength = 4;
    public const int VersionNeedToExtractLength = 2;
    public const int GeneralPurposeBitFlagLength = 2;
    public const int CompressionMethodLength = 2;
    public const int LastModFileTimeLength = 2;
    public const int LastModFileDateLength = 2;
    public const int Crc32Length = 4;
    public const int CompressedSizeLength = 4;
    public const int UncompressedSizeLength = 4;
    public const int FileNameLengthLength = 2;
    public const int ExtraFieldLengthLength = 2;
    
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
    public byte[] FileName { get; set; }
    public byte[] ExtraField { get; set; }

    public LocalFileHeader(
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
        byte[] fileName,
        byte[] extraField)
    {
        // NOTE: Investigate whether this length check is necessary
        // Should consider other exception type
        
        if (versionNeededToExtract.Length != VersionNeedToExtractLength)
            throw new ArgumentException($"{nameof(versionNeededToExtract)} is out of bounds.");
        if (generalPurposeBitFlag.Length != GeneralPurposeBitFlagLength)
            throw new ArgumentException($"{nameof(generalPurposeBitFlag)} is out of bounds.");
        if (compressionMethod.Length != CompressionMethodLength)
            throw new ArgumentException($"{nameof(compressionMethod)} is out of bounds.");
        if (lastModFileTime.Length != LastModFileTimeLength)
            throw new ArgumentException($"{nameof(lastModFileTime)} is out of bounds.");
        if (lastModFileDate.Length != LastModFileDateLength)
            throw new ArgumentException($"{nameof(lastModFileDate)} is out of bounds.");
        if (crc32.Length != Crc32Length)
            throw new ArgumentException($"{nameof(crc32)} is out of bounds.");
        if (compressedSize.Length != CompressedSizeLength)
            throw new ArgumentException($"{nameof(compressedSize)} is out of bounds.");
        if (uncompressedSize.Length != UncompressedSizeLength)
            throw new ArgumentException($"{nameof(uncompressedSize)} is out of bounds.");
        if (fileNameLength.Length != FileNameLengthLength)
            throw new ArgumentException($"{nameof(fileNameLength)} is out of bounds.");
        if (extraFieldLength.Length != ExtraFieldLengthLength)
            throw new ArgumentException($"{nameof(extraFieldLength)} is out of bounds.");
        if (fileName.Length != fileNameLength.ToUShortBigEndian())
            throw new ArgumentException($"{nameof(fileName)} is out of bounds.");
        if (extraField.Length != extraFieldLength.ToUShortBigEndian())
            throw new ArgumentException($"{nameof(extraField)} is out of bounds.");
        
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
        FileName = fileName;
        ExtraField = extraField;
    }
}