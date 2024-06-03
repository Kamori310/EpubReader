namespace YouZip.data;

public class LocalFileHeader
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

    /// <summary>
    /// Saved in ushort format
    /// </summary>
    public byte[] VersionNeededToExtract;
    public byte[] GeneralPurposeBitFlag;
    public byte[] CompressionMethod;
    public byte[] LastModFileTime;
    public byte[] LastModFileDate;
    public byte[] Crc32;
    /// <summary>
    /// Includes the size of the optional data descriptor 
    /// </summary>
    public byte[] CompressedSize;
    public byte[] UncompressedSize;
    public byte[] FileNameLength;
    public byte[] ExtraFieldLength;
    public byte[] FileName;
    public byte[] ExtraField;
    
    public LocalFileHeader (byte[] input, int startingPosition)
    {
        startingPosition += LocalFileHeaderSignatureLength;

        VersionNeededToExtract = input.Subarray(startingPosition, VersionNeedToExtractLength);
        startingPosition += VersionNeedToExtractLength;
        
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
        // Console.WriteLine($"File name length: {FileNameLength.ToUShort()}");

        ExtraFieldLength = input.Subarray(startingPosition, ExtraFieldLengthLength);
        startingPosition += ExtraFieldLengthLength;
        // Console.WriteLine($"Extra field length: {ExtraFieldLength.ToUShort()}");

        FileName = input.Subarray(startingPosition, FileNameLength.ToUShort());
        startingPosition += FileNameLength.ToUShort();
        // Console.WriteLine($"{Encoding.UTF8.GetString(FileName)}");

        ExtraField = input.Subarray(startingPosition, ExtraFieldLength.ToUShort());
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
            FileNameLength.ToUShort() +
            ExtraFieldLength.ToUShort();
    }

    public ushort GetVersionNeededToExtract() => 
        VersionNeededToExtract.ToUShort();
    

    public bool[] GetGeneralPurposeBitFlags()
    {
        int[] bits = 
            [
                7, // 0b1000_0000 
                6, // 0b0100_0000
                5, // 0b0010_0000
                4, // 0b0001_0000
                3, // 0b0000_1000
                2, // 0b0000_0100
                1, // 0b0000_0010
                0  // 0b0000_0001
            ];
        
        return GeneralPurposeBitFlag
            .SelectMany(it =>
                bits.Select(bit =>
                    it.GetBit(bit)))
            .ToArray();
    }

    public ushort GetCompressionMethod() => 
        CompressionMethod.ToUShort();

    public DateTime GetLastModifiedDateTime() =>
        LastModFileDate.DateFromMsDosFormat() + LastModFileTime.TimeFromMsDosFormat();
}