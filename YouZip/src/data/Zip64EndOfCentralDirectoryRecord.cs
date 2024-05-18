namespace YouZip.data;

public struct Zip64EndOfCentralDirectoryRecord
{
    public static readonly byte[] Zip64EndOfCentralDirSignature = [0x50, 0x4b, 0x06, 0x06];

    public const int Zip64EndOfCentralDirSignatureLength = 4;
    public const int SizeOfZip64EndOfCentralDirectoryRecordLength = 8;
    public const int VersionMadeByLength = 2;
    public const int VersionNeededToExtractLength = 2;
    public const int NumberOfThisDiskLength = 4;
    public const int NumberOfTheDiskWithTheStartOfTheCentralDirectoryLength = 4;
    public const int TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength = 8;
    public const int TotalNumberOfEntriesInTheCentralDirectoryLength = 8;
    public const int SizeOfTheCentralDirectoryLength = 8;
    public const int OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumberLength = 8;

    public byte[] SizeOfZip64EndOfCentralDirectoryRecord { get; set; }
    public byte[] VersionMadeBy { get; set; }
    public byte[] VersionNeededToExtract { get; set; }
    public byte[] NumberOfThisDisk { get; set; }
    public byte[] NumberOfTheDiskWithTheStartOfTheCentralDirectory { get; set; }
    public byte[] TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk { get; set; }
    public byte[] TotalNumberOfEntriesInTheCentralDirectory { get; set; }
    public byte[] SizeOfTheCentralDirectory { get; set; }
    public byte[] OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber { get; set; }

    public Zip64EndOfCentralDirectoryRecord(
        byte[] sizeOfZip64EndOfCentralDirectoryRecord, 
        byte[] versionMadeBy,
        byte[] versionNeededToExtract, 
        byte[] numberOfThisDisk, 
        byte[] numberOfTheDiskWithTheStartOfTheCentralDirectory,
        byte[] totalNumberOfEntriesInTheCentralDirectoryOnThisDisk, 
        byte[] totalNumberOfEntriesInTheCentralDirectory,
        byte[] sizeOfTheCentralDirectory, 
        byte[] offsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber)
    {
        SizeOfZip64EndOfCentralDirectoryRecord = sizeOfZip64EndOfCentralDirectoryRecord;
        VersionMadeBy = versionMadeBy;
        VersionNeededToExtract = versionNeededToExtract;
        NumberOfThisDisk = numberOfThisDisk;
        NumberOfTheDiskWithTheStartOfTheCentralDirectory = numberOfTheDiskWithTheStartOfTheCentralDirectory;
        TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk = totalNumberOfEntriesInTheCentralDirectoryOnThisDisk;
        TotalNumberOfEntriesInTheCentralDirectory = totalNumberOfEntriesInTheCentralDirectory;
        SizeOfTheCentralDirectory = sizeOfTheCentralDirectory;
        OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber =
            offsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber;
    }
}