namespace YouZip.data;

public struct Zip64EndOfCentralDirectoryLocator
{
    public readonly byte[] Zip64EndOfCentralDirLocatorSignature = [0x50, 0x4b, 0x06, 0x07];

    public const int Zip64EndOfCentralDirLocatorSignatureLength = 4;
    public const int NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirLength = 4;
    public const int RelativeOffsetOfTheZip64EndOfCentralDirectoryRecordLength = 8;
    public const int TotalNumberOfDisksLength = 4;

    public byte[] NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir { get; set; }
    public byte[] RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord { get; set; }
    public byte[] TotalNumberOfDisks { get; set; }

    public Zip64EndOfCentralDirectoryLocator(
        byte[] numberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir,
        byte[] relativeOffsetOfTheZip64EndOfCentralDirectoryRecord, 
        byte[] totalNumberOfDisks)
    {
        NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir = numberOfTheDiskWithTheStartOfTheZip64EndOfCentralDir;
        RelativeOffsetOfTheZip64EndOfCentralDirectoryRecord = relativeOffsetOfTheZip64EndOfCentralDirectoryRecord;
        TotalNumberOfDisks = totalNumberOfDisks;
    }
}