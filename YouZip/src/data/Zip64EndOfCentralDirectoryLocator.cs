namespace YouZip.data;

public struct Zip64EndOfCentralDirectoryLocator
{
    public static readonly byte[] Zip64EndOfCentralDirLocatorSignature = [0x50, 0x4b, 0x06, 0x07];

    public const int Zip64EndOfCentralDirLocatorSignatureLength = 4;
    public const int NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirLength = 4;
    public const int RelativeOffsetOfTheZip64EndOfCentralDirectoryRecordLength = 8;
    public const int TotalNumberOfDisksLength = 4;

    public uint NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir { get; }
    public ulong RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord { get; }
    public uint TotalNumberOfDisks { get; }

    public Zip64EndOfCentralDirectoryLocator(byte[] input, int startingPosition)
    {
        startingPosition += Zip64EndOfCentralDirLocatorSignatureLength;

        NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirLength;

        RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += RelativeOffsetOfTheZip64EndOfCentralDirectoryRecordLength;
        
        TotalNumberOfDisks = BitConverter.ToUInt32(input, startingPosition);
    }
}