namespace YouZip.data;

public class LocalFileEntry(LocalFileHeader localFileHeader, byte[] localFileData)
{
    private LocalFileHeader _localFileHeader = localFileHeader;
    private byte[] _localFileData = localFileData;
}