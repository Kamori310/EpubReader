namespace YouZip.data;

public struct EndOfCentralDirectoryRecord
{
    public readonly byte[] EndOfCentralDirSignature = [0x50, 0x4b, 0x05, 0x06];

    public const int EndOfCentralDirSignatureLength = 4;
    public const int NumberOfThisDiskLength = 2;
    public const int NumberOfTheDiskWithTheStartOfCentralDirectoryLength = 2;
    public const int TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength = 2;
    public const int TotalNumberOfEntriesInTheCentralDirectoryLength = 2;
    public const int SizeOfTheCentralDirectoryLength = 4;
    public const int OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumberLength = 4;
    public const int DotZipFileCommentLengthLength = 2;

    public byte[] NumberOfThisDisk { get; set; }
    public byte[] NumberOfTheDiskWithTheStartOfCentralDirectory { get; set; }
    public byte[] TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk { get; set; }
    public byte[] TotalNumberOfEntriesInTheCentralDirectory { get; set; }
    public byte[] SizeOfTheCentralDirectory { get; set; }
    public byte[] OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumber { get; set; }
    public byte[] DotZipFileCommentLength { get; set; }

    public EndOfCentralDirectoryRecord(
        byte[] numberOfThisDisk, 
        byte[] numberOfTheDiskWithTheStartOfCentralDirectory,
        byte[] totalNumberOfEntriesInTheCentralDirectoryOnThisDisk, 
        byte[] totalNumberOfEntriesInTheCentralDirectory,
        byte[] sizeOfTheCentralDirectory, 
        byte[] offSetOfTheStartOfCentralDirWrtTheStartingDiskNumber,
        byte[] dotZipFileCommentLength)
    {
        NumberOfThisDisk = numberOfThisDisk;
        NumberOfTheDiskWithTheStartOfCentralDirectory = numberOfTheDiskWithTheStartOfCentralDirectory;
        TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk = totalNumberOfEntriesInTheCentralDirectoryOnThisDisk;
        TotalNumberOfEntriesInTheCentralDirectory = totalNumberOfEntriesInTheCentralDirectory;
        SizeOfTheCentralDirectory = sizeOfTheCentralDirectory;
        OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumber = offSetOfTheStartOfCentralDirWrtTheStartingDiskNumber;
        DotZipFileCommentLength = dotZipFileCommentLength;
    }
}