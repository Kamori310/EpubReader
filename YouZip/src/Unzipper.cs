using System.Text;
using Microsoft.Extensions.Logging;
using YouZip.data;

namespace YouZip;

public class Unzipper(ILogger<Unzipper> logger)
{
        
    public void Unzip(string filename)
    {
        var allBytes = File.ReadAllBytes(filename);
        logger.LogDebug($"File length: {allBytes.Length.ToString()}");
        // logger.LogDebug($"Base 16: {Convert.ToString(allBytes[0], 16)}");
        // logger.LogDebug($"Base  2: {Convert.ToString(allBytes[0], 2)}");
        // logger.LogDebug($"Base 10: {Convert.ToString(allBytes[0], 10)}");
        
        // Console.WriteLine(allBytes.FormatString());

        GetLocalFileHeaders(allBytes);

        Console.WriteLine("Local File Headers");
        foreach (var startingPosition in FindSignatures(
                     allBytes, LocalFileHeader.LocalFileHeaderSignature))
        {
            Console.WriteLine($"Starting position: {startingPosition}");
        }
        
        Console.WriteLine();
        Console.WriteLine("Central File Headers");
        foreach (var startingPosition in FindSignatures(
                     allBytes, CentralFileHeader.CentralFileHeaderSignature))
        {
            Console.WriteLine($"Starting position: {startingPosition}");
            var header = GetCentralFileHeader(allBytes, startingPosition);
            Console.WriteLine(Encoding.UTF8.GetString(header.FileName));
        }
        
        
    }

    private IEnumerable<int> FindSignatures(byte[] mainSequence, byte[] subsequence)
    {
        return Enumerable
            .Range(0, mainSequence.Length - subsequence.Length)
            .Where(
                it => mainSequence
                    .Skip(it)
                    .Take(subsequence.Length)
                    .SequenceEqual(subsequence));
    }
    
    private List<LocalFileHeader> GetLocalFileHeaders(byte[] input)
    {
        List<LocalFileHeader> result = [];
        
        for (var i = 0; i < input.Length - LocalFileHeader.LocalFileHeaderSignatureLength; )
        {
            if (input
                .Skip(i)
                .Take(LocalFileHeader.LocalFileHeaderSignatureLength)
                .SequenceEqual(LocalFileHeader.LocalFileHeaderSignature))
            {
                i += 4; // Skip signature
                
                var versionNeededToExtract = input.Skip(i).Take(2).ToArray();
                // Console.WriteLine(versionNeededToExtract.FormatString());
                i += 2; 
                
                var generalPurposeBitFlag = input.Skip(i).Take(2).ToArray();
                // Console.WriteLine(generalPurposeBitFlag.FormatString());
                i += 2;
                
                var compressionMethod = input.Skip(i).Take(2).ToArray();
                // Console.WriteLine(compressionMethod.FormatString());
                i += 2;
                
                var lastModFileTime = input.Skip(i).Take(2).ToArray();
                // Console.WriteLine(lastModFileTime.FormatString());
                i += 2;
                
                var lastModFileDate = input.Skip(i).Take(2).ToArray();
                // Console.WriteLine(lastModFileDate.FormatString());
                i += 2;
                
                var crc32 = input.Skip(i).Take(4).ToArray();
                // Console.WriteLine(crc32.FormatString());
                i += 4;
                
                var compressedSize = input.Skip(i).Take(4).ToArray();
                // Console.WriteLine(compressedSize.FormatString());
                i += 4;
                
                var uncompressedSize = input.Skip(i).Take(4).ToArray();
                // Console.WriteLine(uncompressedSize.FormatString());
                i += 4;
                
                var fileNameLength = input.Skip(i).Take(2).ToArray().ToArray();
                Console.WriteLine($"File name length: {fileNameLength.ToUShort()}");
                i += 2;
                
                var extraFieldLength = input.Skip(i).Take(2).ToArray();
                Console.WriteLine($"Extra field length: {extraFieldLength.ToUShort()}");
                i += 2;
                
                var fileName = input.Skip(i).Take(fileNameLength.ToUShort()).ToArray();
                Console.WriteLine($"{Encoding.UTF8.GetString(fileName)}");
                i += fileNameLength.ToUShort();
                
                var extraField = input.Skip(i).Take(extraFieldLength.ToUShort()).ToArray();
                // Console.WriteLine(extraField.FormatString());
                
                // Set i to last position in current header
                i += extraFieldLength.ToUShort();

                // Try reading the data as well
                var fileData = input.Skip(i).Take((int)compressedSize.ToUInt()).ToArray();
                i += (int)compressedSize.ToUInt();
                
                var test =
                    versionNeededToExtract
                        .Concat(generalPurposeBitFlag)
                        .Concat(compressionMethod)
                        .Concat(lastModFileTime)
                        .Concat(lastModFileDate)
                        .Concat(crc32)
                        .Concat(compressedSize)
                        .Concat(uncompressedSize)
                        .Concat(fileNameLength)
                        .Concat(extraFieldLength)
                        .Concat(fileName)
                        .Concat(extraField)
                        .Concat(fileData)
                        .ToArray();
                
                Console.WriteLine(test.FormatString());
                
                Console.WriteLine($"'i' is: {i}");
            }
            else
            {
                i++;
            }
        }

        return result;
    }

    private LocalFileEntry GetLocalFileEntry(byte[] input, int startingPosition)
    {
        var header = GetLocalFileHeader(input, startingPosition);
        var fileData = GetPropertyFromBytes(
            input,
            startingPosition + header.GetHeaderLength(),
            (int)header.CompressedSize.ToUInt());

        return new LocalFileEntry(header, fileData);
    }

    private LocalFileHeader GetLocalFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += LocalFileHeader.LocalFileHeaderSignatureLength;

        var versionNeededToExtract = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.VersionNeedToExtractLength);
        startingPosition += LocalFileHeader.VersionNeedToExtractLength;

        var generalPurposeBitFlag = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.GeneralPurposeBitFlagLength);
        startingPosition += LocalFileHeader.GeneralPurposeBitFlagLength;

        var compressionMethod = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.CompressionMethodLength);
        startingPosition += LocalFileHeader.CompressionMethodLength;

        var lastModFileTime = GetPropertyFromBytes(
            input,
            startingPosition,
            LocalFileHeader.LastModFileTimeLength);
        startingPosition += LocalFileHeader.LastModFileTimeLength;

        var lastModFileDate = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.LastModFileDateLength);
        startingPosition += LocalFileHeader.LastModFileDateLength;

        var crc32 = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.Crc32Length);
        startingPosition += LocalFileHeader.Crc32Length;

        var compressedSize = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.CompressedSizeLength);
        startingPosition += LocalFileHeader.CompressedSizeLength;

        var uncompressedSize = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.UncompressedSizeLength);
        startingPosition += LocalFileHeader.UncompressedSizeLength;

        var fileNameLength = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.FileNameLengthLength);
        startingPosition += LocalFileHeader.FileNameLengthLength;
        Console.WriteLine($"File name length: {fileNameLength.ToUShort()}");

        var extraFieldLength = GetPropertyFromBytes(
            input, 
            startingPosition, 
            LocalFileHeader.ExtraFieldLengthLength);
        startingPosition += LocalFileHeader.ExtraFieldLengthLength;
        Console.WriteLine($"Extra field length: {extraFieldLength.ToUShort()}");

        var fileName = GetPropertyFromBytes(
            input, 
            startingPosition, 
            fileNameLength.ToUShort());
        startingPosition += fileNameLength.ToUShort();
        Console.WriteLine($"{Encoding.UTF8.GetString(fileName)}");

        var extraField = GetPropertyFromBytes(
            input, startingPosition, extraFieldLength.ToUShort());

        return new LocalFileHeader(
            versionNeededToExtract,
            generalPurposeBitFlag,
            compressionMethod, 
            lastModFileTime, 
            lastModFileDate, 
            crc32, 
            compressedSize, 
            uncompressedSize, 
            fileNameLength, 
            extraFieldLength, 
            fileName, 
            extraField
        );
    }

    private CentralFileHeader GetCentralFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += CentralFileHeader.CentralFileHeaderSignatureLength;

        var versionMadeBy = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.VersionMadeByLength);
        startingPosition += CentralFileHeader.VersionMadeByLength;

        var versionNeededToExtract = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.VersionNeededToExtractLength);
        startingPosition += CentralFileHeader.VersionNeededToExtractLength;

        var generalPurposeBitFlag = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.GeneralPurposeBitFlagLength);
        startingPosition += CentralFileHeader.GeneralPurposeBitFlagLength;

        var compressionMethod = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.CompressionMethodLength);
        startingPosition += CentralFileHeader.CompressionMethodLength;

        var lastModFileTime = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.LastModFileTimeLength);
        startingPosition += CentralFileHeader.LastModFileTimeLength;

        var lastModFileDate = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.LastModFileDateLength);
        startingPosition += CentralFileHeader.LastModFileDateLength;

        var crc32 = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.Crc32Length);
        startingPosition += CentralFileHeader.Crc32Length;

        var compressedSize = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.CompressedSizeLength);
        startingPosition += CentralFileHeader.CompressedSizeLength;
        
        var uncompressedSize = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.UncompressedSizeLength);
        startingPosition += CentralFileHeader.UncompressedSizeLength;

        var fileNameLength = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.FileNameLengthLength);
        startingPosition += CentralFileHeader.FileNameLengthLength;

        var extraFieldLength = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.ExtraFieldLengthLength);
        startingPosition += CentralFileHeader.ExtraFieldLengthLength;

        var fileCommentLength = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.FileCommentLengthLength);
        startingPosition += CentralFileHeader.FileCommentLengthLength;

        var diskNumberStart = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.DiskNumberStartLength);
        startingPosition += CentralFileHeader.DiskNumberStartLength;

        var internalFileAttributes = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.InternalFileAttributesLength);
        startingPosition += CentralFileHeader.InternalFileAttributesLength;

        var externalFileAttribute = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.ExternalFileAttributesLength);
        startingPosition += CentralFileHeader.ExternalFileAttributesLength;

        var relativeOffsetOfLocalHeader = GetPropertyFromBytes(
            input, startingPosition, CentralFileHeader.RelativeOffsetOfLocalHeaderLength);
        startingPosition += CentralFileHeader.RelativeOffsetOfLocalHeaderLength;

        var fileName = GetPropertyFromBytes(
            input, startingPosition, fileNameLength.ToUShort());
        startingPosition += fileNameLength.ToUShort();

        var extraField = GetPropertyFromBytes(
            input, startingPosition, extraFieldLength.ToUShort());
        startingPosition += extraFieldLength.ToUShort();

        var fileComment = GetPropertyFromBytes(
            input, startingPosition, fileCommentLength.ToUShort());

        return new CentralFileHeader(
            versionMadeBy,
            versionNeededToExtract,
            generalPurposeBitFlag,
            compressionMethod,
            lastModFileTime,
            lastModFileDate,
            crc32,
            compressedSize,
            uncompressedSize,
            fileNameLength,
            extraFieldLength,
            fileCommentLength,
            diskNumberStart,
            internalFileAttributes,
            externalFileAttribute,
            relativeOffsetOfLocalHeader,
            fileName,
            extraField,
            fileComment);
    }

    private static byte[] GetPropertyFromBytes(
        byte[] input, 
        int startingPosition, 
        int propertyLength)
    {
        return input
            .Skip(startingPosition)
            .Take(propertyLength)
            .ToArray();
    }
}