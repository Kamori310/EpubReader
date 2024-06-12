using System.Text;

namespace YouZip.data;

public struct EndOfCentralDirectoryRecord
{
    public static readonly byte[] EndOfCentralDirSignature = [0x50, 0x4b, 0x05, 0x06];

    public const int EndOfCentralDirSignatureLength = 4;
    public const int NumberOfThisDiskLength = 2;
    public const int NumberOfTheDiskWithTheStartOfCentralDirectoryLength = 2;
    public const int TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength = 2;
    public const int TotalNumberOfEntriesInTheCentralDirectoryLength = 2;
    public const int SizeOfTheCentralDirectoryLength = 4;
    public const int OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumberLength = 4;
    public const int DotZipFileCommentLengthLength = 2;

    public ushort NumberOfThisDisk { get; }
    public ushort NumberOfTheDiskWithTheStartOfCentralDirectory { get; }
    public ushort TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk { get; }
    public ushort TotalNumberOfEntriesInTheCentralDirectory { get; }
    public uint SizeOfTheCentralDirectory { get; }
    public uint OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumber { get; }
    public ushort DotZipFileCommentLength { get; }
    public string DotZipFileComment { get; }

    public EndOfCentralDirectoryRecord(byte[] input, int startingPosition)
    {
        startingPosition += EndOfCentralDirSignatureLength;

        NumberOfThisDisk = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += NumberOfThisDiskLength;

        NumberOfTheDiskWithTheStartOfCentralDirectory = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += NumberOfTheDiskWithTheStartOfCentralDirectoryLength;

        TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength;

        TotalNumberOfEntriesInTheCentralDirectory = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += TotalNumberOfEntriesInTheCentralDirectoryLength;

        SizeOfTheCentralDirectory = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += SizeOfTheCentralDirectoryLength;

        OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumber = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumberLength;

        DotZipFileCommentLength = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += DotZipFileCommentLengthLength;

        DotZipFileComment = Encoding.UTF8.GetString(
            input, 
            startingPosition, 
            DotZipFileCommentLength);
    }

    internal void PrintRecord()
    {
        string output =
            $"Number of disk: {NumberOfThisDisk}\n" +
            $"Number of disk with the start of central directory: {NumberOfTheDiskWithTheStartOfCentralDirectory}\n" +
            $"Total number of entries in the central directory on this disk: {TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk}\n" +
            $"Total number of entries in the central directory: {TotalNumberOfEntriesInTheCentralDirectory}\n" +
            $"Size of the central directory: {SizeOfTheCentralDirectory}\n" +
            $"Offset of the start of central directory with respect to the starting disk number: {OffSetOfTheStartOfCentralDirWrtTheStartingDiskNumber}\n" +
            $"Dot zip file comment: {DotZipFileComment}\n\n";
        Console.Write(output);
    }
}