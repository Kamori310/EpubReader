namespace YouZip.data;

public struct DigitalSignature
{
    public readonly byte[] HeaderSignature = [0x50, 0x4b, 0x05, 0x05];
    
    public const int HeaderSignatureLength = 4;
    public const int SizeOfDataLength = 2;

    public byte[] SizeOfData { get; set; }
    public byte[] SignatureData { get; set; }

    public DigitalSignature(byte[] sizeOfData, byte[] signatureData)
    {
        SizeOfData = sizeOfData;
        SignatureData = signatureData;
    }
}