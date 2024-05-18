namespace YouZip.data;

public struct ArchiveExtraDataRecord
{
    public static readonly byte[] ArchiveExtraDataSignature = [0x50, 0x4b, 0x64, 0x80];
    
    public const int ArchiveExtraDataSignatureLength = 4;
    public const int ExtraFieldLengthLength = 4;

    public byte[] ExtraFieldLength { get; set; }
    public byte[] ExtraFieldData { get; set; }

    public ArchiveExtraDataRecord(byte[] extraFieldLength, byte[] extraFieldData)
    {
        ExtraFieldLength = extraFieldLength;
        ExtraFieldData = extraFieldData;
    }
}