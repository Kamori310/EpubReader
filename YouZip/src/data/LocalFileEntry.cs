namespace YouZip.data;

public class LocalFileEntry
{
    private LocalFileHeader _localFileHeader;
    private byte[] _localFileData;

    public LocalFileEntry (byte[] input, int startingPosition)
    {
        _localFileHeader = new LocalFileHeader(input, startingPosition);
        // TODO: Add class for localFileData and construct with info from header
        _localFileData = input.Subarray(
            startingPosition + _localFileHeader.HeaderLength(), 
            (int)_localFileHeader.CompressedSize.ToUInt());
    }

}