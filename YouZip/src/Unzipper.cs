using System.Text;
using System.Xml;
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

    private CentralFileHeader GetCentralFileHeader(byte[] input, int startingPosition)
    {
        startingPosition += CentralFileHeader.CentralFileHeaderSignatureLength;

        var versionMadeBy = GetProperty(
            input, startingPosition, CentralFileHeader.VersionMadeByLength);
        startingPosition += CentralFileHeader.VersionMadeByLength;

        var versionNeededToExtract = GetProperty(
            input, startingPosition, CentralFileHeader.VersionNeededToExtractLength);
        startingPosition += CentralFileHeader.VersionNeededToExtractLength;

        var generalPurposeBitFlag = GetProperty(
            input, startingPosition, CentralFileHeader.GeneralPurposeBitFlagLength);
        startingPosition += CentralFileHeader.GeneralPurposeBitFlagLength;

        var compressionMethod = GetProperty(
            input, startingPosition, CentralFileHeader.CompressionMethodLength);
        startingPosition += CentralFileHeader.CompressionMethodLength;

        var lastModFileTime = GetProperty(
            input, startingPosition, CentralFileHeader.LastModFileTimeLength);
        startingPosition += CentralFileHeader.LastModFileTimeLength;

        var lastModFileDate = GetProperty(
            input, startingPosition, CentralFileHeader.LastModFileDateLength);
        startingPosition += CentralFileHeader.LastModFileDateLength;

        var crc32 = GetProperty(
            input, startingPosition, CentralFileHeader.Crc32Length);
        startingPosition += CentralFileHeader.Crc32Length;

        var compressedSize = GetProperty(
            input, startingPosition, CentralFileHeader.CompressedSizeLength);
        startingPosition += CentralFileHeader.CompressedSizeLength;
        
        var uncompressedSize = GetProperty(
            input, startingPosition, CentralFileHeader.UncompressedSizeLength);
        startingPosition += CentralFileHeader.UncompressedSizeLength;

        var fileNameLength = GetProperty(
            input, startingPosition, CentralFileHeader.FileNameLengthLength);
        startingPosition += CentralFileHeader.FileNameLengthLength;

        var extraFieldLength = GetProperty(
            input, startingPosition, CentralFileHeader.ExtraFieldLengthLength);
        startingPosition += CentralFileHeader.ExtraFieldLengthLength;

        var fileCommentLength = GetProperty(
            input, startingPosition, CentralFileHeader.FileCommentLengthLength);
        startingPosition += CentralFileHeader.FileCommentLengthLength;

        var diskNumberStart = GetProperty(
            input, startingPosition, CentralFileHeader.DiskNumberStartLength);
        startingPosition += CentralFileHeader.DiskNumberStartLength;

        var internalFileAttributes = GetProperty(
            input, startingPosition, CentralFileHeader.InternalFileAttributesLength);
        startingPosition += CentralFileHeader.InternalFileAttributesLength;

        var externalFileAttribute = GetProperty(
            input, startingPosition, CentralFileHeader.ExternalFileAttributesLength);
        startingPosition += CentralFileHeader.ExternalFileAttributesLength;

        var relativeOffsetOfLocalHeader = GetProperty(
            input, startingPosition, CentralFileHeader.RelativeOffsetOfLocalHeaderLength);
        startingPosition += CentralFileHeader.RelativeOffsetOfLocalHeaderLength;

        var fileName = GetProperty(
            input, startingPosition, fileNameLength.ToUShort());
        startingPosition += fileNameLength.ToUShort();

        var extraField = GetProperty(
            input, startingPosition, extraFieldLength.ToUShort());
        startingPosition += extraFieldLength.ToUShort();

        var fileComment = GetProperty(
            input, startingPosition, fileCommentLength.ToUShort());
        startingPosition += fileCommentLength.ToUShort();

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

    private static byte[] GetProperty(
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