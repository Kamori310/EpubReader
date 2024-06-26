﻿using System.Text;
using Microsoft.Extensions.Logging;
using YouZip.data;
using YouZip.Extensions;

namespace YouZip;

public class Unzipper(ILogger<Unzipper> logger)
{
        
    public void Unzip(string filename)
    {
        var allBytes = File.ReadAllBytes(filename);
        logger.LogDebug($"File length: {allBytes.Length.ToString()}");
        
        Console.WriteLine("Local File Headers");
        // foreach (
        //     var startingPosition 
        //     in FindSignatures(allBytes, LocalFileHeader.LocalFileHeaderSignature))
        // {
        //     Console.WriteLine($"Starting position: {startingPosition}");
        //     var localFileEntry = new LocalFileEntry(allBytes, startingPosition);
        //     Console.WriteLine($"Length: {localFileEntry.Header.HeaderLength() + localFileEntry.Data.Length} \tHeader length: {localFileEntry.Header.HeaderLength()} \tData length: {localFileEntry.Data.Length}");
        //     Console.WriteLine($"File name length: {localFileEntry.Header.FileNameLength}");
        //     Console.WriteLine($"File name: {localFileEntry.Header.FileName}");
        //     
        //     Console.WriteLine($"Version Needed To Extract: {localFileEntry.Header.VersionNeededToExtract}");
        //     Console.WriteLine($"General Purpose Bit Flag: {localFileEntry.Header.GeneralPurposeBitFlag.FormatString()}");
        //     Console.WriteLine($"Compression Method: {localFileEntry.Header.CompressionMethod}");
        //     Console.WriteLine($"Compressed Size: {localFileEntry.Header.CompressedSize}");
        //     Console.WriteLine($"Uncompressed Size: {localFileEntry.Header.UncompressedSize}");
        //     
        //     Console.WriteLine();
        //     Console.WriteLine($"Data: ");
        //     Console.WriteLine($"{localFileEntry.Data.Take(157).ToArray().FormatStringAsBits()}");
        // }

        // var startingPositions = FindSignatures(allBytes, LocalFileHeader.LocalFileHeaderSignature).ToArray();
        // for (var i = 0; i < 4; i++)
        // {
        //     var startingPosition = startingPositions[i];
        //     
        //     Console.WriteLine($"Starting position: {startingPosition}");
        //     var localFileEntry = new LocalFileEntry(allBytes, startingPosition);
        //     Console.WriteLine($"Length: {localFileEntry.Header.HeaderLength() + localFileEntry.Data.Length} \tHeader length: {localFileEntry.Header.HeaderLength()} \tData length: {localFileEntry.Data.Length}");
        //     Console.WriteLine($"File name length: {localFileEntry.Header.FileNameLength}");
        //     Console.WriteLine($"File name: {localFileEntry.Header.FileName}");
        //     
        //     Console.WriteLine($"Version Needed To Extract: {localFileEntry.Header.VersionNeededToExtract}");
        //     Console.WriteLine($"General Purpose Bit Flag: {localFileEntry.Header.GeneralPurposeBitFlag.FormatString()}");
        //     Console.WriteLine($"Compression Method: {localFileEntry.Header.CompressionMethod}");
        //     Console.WriteLine($"Compressed Size: {localFileEntry.Header.CompressedSize}");
        //     Console.WriteLine($"Uncompressed Size: {localFileEntry.Header.UncompressedSize}");
        //     
        //     Console.WriteLine();
        //     Console.WriteLine($"Data: ");
        //     Console.WriteLine($"{localFileEntry.Data.Take(157).ToArray().FormatStringAsBits()}");
        // }
        
        Console.WriteLine();
        Console.WriteLine("Central File Headers");
        foreach (
            var startingPosition 
            in FindSignatures(allBytes, CentralFileHeader.CentralFileHeaderSignature))
        {
            // Console.WriteLine($"Starting position: {startingPosition}");
            var header = new CentralFileHeader(allBytes, startingPosition);
            header.PrintHeader();
            // Console.WriteLine(header.FileName);
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
    
    // TODO: Refactor this
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
}