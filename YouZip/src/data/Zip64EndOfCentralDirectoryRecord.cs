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

    public ulong SizeOfZip64EndOfCentralDirectoryRecord { get; }
    public ushort VersionMadeBy { get; }
    public ushort VersionNeededToExtract { get; }
    public uint NumberOfThisDisk { get; }
    public uint NumberOfTheDiskWithTheStartOfTheCentralDirectory { get; }
    public ulong TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk { get; }
    public ulong TotalNumberOfEntriesInTheCentralDirectory { get; }
    public ulong SizeOfTheCentralDirectory { get; }
    public ulong OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber { get; }
    public byte[] Zip64ExtensibleDataSector { get; }

    private const ulong SizeOfFixedFields = 
        VersionMadeByLength + 
        VersionNeededToExtractLength + 
        NumberOfThisDiskLength + 
        NumberOfTheDiskWithTheStartOfTheCentralDirectoryLength + 
        TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength + 
        TotalNumberOfEntriesInTheCentralDirectoryLength + 
        SizeOfTheCentralDirectoryLength + 
        OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumberLength;


    public Zip64EndOfCentralDirectoryRecord(byte[] input, int startingPosition)
    {
        startingPosition += Zip64EndOfCentralDirSignatureLength;
        
        SizeOfZip64EndOfCentralDirectoryRecord = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += SizeOfZip64EndOfCentralDirectoryRecordLength;
        
        VersionMadeBy = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += VersionMadeByLength;
        
        VersionNeededToExtract = BitConverter.ToUInt16(input, startingPosition);
        startingPosition += VersionNeededToExtractLength;
        
        NumberOfThisDisk = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += NumberOfThisDiskLength;
        
        NumberOfTheDiskWithTheStartOfTheCentralDirectory = BitConverter.ToUInt32(input, startingPosition);
        startingPosition += NumberOfTheDiskWithTheStartOfTheCentralDirectoryLength;
        
        TotalNumberOfEntriesInTheCentralDirectoryOnThisDisk = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += TotalNumberOfEntriesInTheCentralDirectoryOnThisDiskLength;
        
        TotalNumberOfEntriesInTheCentralDirectory = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += TotalNumberOfEntriesInTheCentralDirectoryLength;
        
        SizeOfTheCentralDirectory = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += SizeOfTheCentralDirectoryLength;
        
        OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumber = BitConverter.ToUInt64(input, startingPosition);
        startingPosition += OffsetOfStartOfCentralDirectoryWrtTheStartingDiskNumberLength;

        var sizeVariableData = SizeOfTheCentralDirectory + 12 - SizeOfFixedFields;
        // TODO
        // Zip64ExtensibleDataSector = input.Skip(startingPosition).Take(sizeVariableData);
    }
}