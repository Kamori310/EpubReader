using System.Text;

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

    private readonly byte[] _versionMadeBy;
    private readonly byte[] _versionNeededToExtract;
    private readonly byte[] _generalPurposeBitFlag;
    private readonly byte[] _compressionMethod;
    private readonly byte[] _lastModFileTime;
    private readonly byte[] _lastModFileDate;
    private readonly byte[] _crc32;
    private readonly byte[] _compressedSize;
    private readonly byte[] _uncompressedSize;
    private readonly byte[] _fileNameLength;
    private readonly byte[] _extraFieldLength;
    private readonly byte[] _fileCommentLength;
    private readonly byte[] _diskNumberStart;
    private readonly byte[] _internalFileAttributes;
    private readonly byte[] _externalFileAttributes;
    private readonly byte[] _relativeOffsetOfLocalHeader;
    private readonly byte[] _fileName;
    private readonly byte[] _extraField;
    private readonly byte[] _fileComment;

    public CentralFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += CentralFileHeaderSignatureLength;

        _versionMadeBy = input.Subarray(startingPosition, VersionMadeByLength);
        startingPosition += VersionMadeByLength;

        _versionNeededToExtract = input.Subarray(startingPosition, VersionNeededToExtractLength);
        startingPosition += VersionNeededToExtractLength;

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

        _extraFieldLength = input.Subarray(startingPosition, ExtraFieldLengthLength);
        startingPosition += ExtraFieldLengthLength;

        _fileCommentLength = input.Subarray(startingPosition, FileCommentLengthLength);
        startingPosition += FileCommentLengthLength;

        _diskNumberStart = input.Subarray(startingPosition, DiskNumberStartLength);
        startingPosition += DiskNumberStartLength;

        _internalFileAttributes = input.Subarray(startingPosition, InternalFileAttributesLength);
        startingPosition += InternalFileAttributesLength;

        _externalFileAttributes = input.Subarray(startingPosition, ExternalFileAttributesLength);
        startingPosition += ExternalFileAttributesLength;

        _relativeOffsetOfLocalHeader = input.Subarray(startingPosition, RelativeOffsetOfLocalHeaderLength);
        startingPosition += RelativeOffsetOfLocalHeaderLength;

        _fileName = input.Subarray(startingPosition, _fileNameLength.ToUShort());
        startingPosition += _fileNameLength.ToUShort();

        _extraField = input.Subarray(startingPosition, _extraFieldLength.ToUShort());
        startingPosition += _extraFieldLength.ToUShort();

        _fileComment = input.Subarray(startingPosition, _fileCommentLength.ToUShort());
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
            _fileNameLength.ToUShort() +
            _extraFieldLength.ToUShort() +
            _fileCommentLength.ToUShort();
    }

    public ushort GetVersionMadeBy() =>
        _versionMadeBy.ToUShort();
    
    public ushort GetVersionNeededToExtract() => 
        _versionNeededToExtract.ToUShort();

    public bool[] GetGeneralPurposeBitFlag()
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

    public DateTime GetLastModifiedDateTime =>
        _lastModFileDate.DateFromMsDosFormat() + _lastModFileTime.TimeFromMsDosFormat();

    public uint GetCompressedSize() =>
        _compressedSize.ToUInt();

    public uint GetUncompressedSize() =>
        _uncompressedSize.ToUInt();

    public ushort GetFileNameLength() =>
        _fileNameLength.ToUShort();

    public ushort GetExtraFieldLength() =>
        _extraFieldLength.ToUShort();

    public ushort GetFileCommentLength() =>
        _fileCommentLength.ToUShort();

    public ushort GetDiskNumberStart() =>
        _diskNumberStart.ToUShort();

    public ushort GetInternalFileAttributes() =>
        _internalFileAttributes.ToUShort();

    public uint GetExternalFileAttributes() =>
        _externalFileAttributes.ToUInt();

    public uint GetRelativeOffsetOfLocalHeader() =>
        _relativeOffsetOfLocalHeader.ToUInt();

    public string GetFileName() =>
        Encoding.UTF8.GetString(_fileName);

    public string GetExtraField() =>
        Encoding.UTF8.GetString(_extraField);

    public string GetFileComment() =>
        Encoding.UTF8.GetString(_fileComment);
}