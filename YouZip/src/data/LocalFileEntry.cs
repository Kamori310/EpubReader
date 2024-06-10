using YouZip.Extensions;

namespace YouZip.data;

public class LocalFileEntry
{
    public readonly LocalFileHeader Header;
    public readonly byte[] Data;

    public LocalFileEntry (byte[] input, int startingPosition)
    {
        Header = new LocalFileHeader(input, startingPosition);
        // TODO: Add class for localFileData and construct with info from header
        //       Look for encryption header 
        Data = input.Subarray(
            startingPosition + Header.HeaderLength(), 
            (int)Header.CompressedSize);
    }

}