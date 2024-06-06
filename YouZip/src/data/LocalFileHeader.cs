using System.Text;

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
    private readonly byte[] _versionNeededToExtract;
    private readonly byte[] _generalPurposeBitFlag;
    private readonly byte[] _compressionMethod;
    private readonly byte[] _lastModFileTime;
    private readonly byte[] _lastModFileDate;
    /// <summary>
    /// Contains the crc-32 hash of the file data
    /// Use System.IO.Hashing
    /// </summary>
    private readonly byte[] _crc32;
    /// <summary>
    /// Size of file data. Contains size of decryption header if present.
    /// If the archive is in ZIP64 format and value of this field is 0xff_ff_ff_ff
    /// the size will be in the corresponding 8 byte ZIP64 extended information
    /// extra field.  
    /// </summary>
    private readonly byte[] _compressedSize;
    private readonly byte[] _uncompressedSize;
    private readonly byte[] _fileNameLength;
    private readonly byte[] _extraFieldLength;
    private readonly byte[] _fileName;
    private readonly byte[] _extraField;
    
    public LocalFileHeader (byte[] input, int startingPosition)
    {
        startingPosition += LocalFileHeaderSignatureLength;

        _versionNeededToExtract = input.Subarray(startingPosition, VersionNeedToExtractLength);
        startingPosition += VersionNeedToExtractLength;
        
        _generalPurposeBitFlag = input.Subarray(startingPosition, GeneralPurposeBitFlagLength);
        startingPosition += GeneralPurposeBitFlagLength;

        _compressionMethod = input.Subarray(startingPosition, CompressionMethodLength);
        startingPosition += CompressionMethodLength;

        _lastModFileTime = input.Subarray(startingPosition, LastModFileTimeLength);
        startingPosition += LastModFileTimeLength;

        _lastModFileDate = input.Subarray(startingPosition, LastModFileDateLength);
        startingPosition += LastModFileDateLength;

        _crc32 = input.Subarray(startingPosition, Crc32Length);
        startingPosition += Crc32Length;

        _compressedSize = input.Subarray(startingPosition, CompressedSizeLength);
        startingPosition += CompressedSizeLength;

        _uncompressedSize = input.Subarray(startingPosition, UncompressedSizeLength);
        startingPosition += UncompressedSizeLength;

        _fileNameLength = input.Subarray(startingPosition, FileNameLengthLength);
        startingPosition += FileNameLengthLength;
        // Console.WriteLine($"File name length: {FileNameLength.ToUShort()}");

        _extraFieldLength = input.Subarray(startingPosition, ExtraFieldLengthLength);
        startingPosition += ExtraFieldLengthLength;
        // Console.WriteLine($"Extra field length: {ExtraFieldLength.ToUShort()}");

        _fileName = input.Subarray(startingPosition, _fileNameLength.ToUShort());
        startingPosition += _fileNameLength.ToUShort();
        // Console.WriteLine($"{Encoding.UTF8.GetString(FileName)}");

        _extraField = input.Subarray(startingPosition, _extraFieldLength.ToUShort());
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
            _fileNameLength.ToUShort() +
            _extraFieldLength.ToUShort();
    }

    public ushort GetVersionNeededToExtract() => 
        _versionNeededToExtract.ToUShort();
    
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
        
        return _generalPurposeBitFlag
            .SelectMany(it =>
                bits.Select(bit =>
                    it.GetBit(bit)))
            .ToArray();
    }

    public ushort GetCompressionMethod() => 
        _compressionMethod.ToUShort();

    public DateTime GetLastModifiedDateTime() =>
        _lastModFileDate.DateFromMsDosFormat() + _lastModFileTime.TimeFromMsDosFormat();

    public uint GetCompressedSize() =>
        _compressedSize.ToUInt();

    public uint GetUncompressedSize() => 
        _uncompressedSize.ToUInt();

    public ushort GetFileNameLength() =>
        _fileNameLength.ToUShort();

    public ushort GetExtraFieldLength() =>
        _extraFieldLength.ToUShort();
    
    public string GetFileName() => 
        Encoding.UTF8.GetString(_fileName);

    public string GetExtraField() =>
        Encoding.UTF8.GetString(_extraField);
}