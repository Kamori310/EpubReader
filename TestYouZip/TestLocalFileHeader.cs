using YouZip.data;

namespace TestYouZip;

public class TestLocalFileHeader
{
        // var localFileHeader = new LocalFileHeader(
        // [
        //     0x50, 0x4b, 0x03, 0x04, // Signature
        //     0x00, 0x00, // Version needed to extract
        //     0x00, 0x00, // General purpose bit flag
        //     0x00, 0x00, // Compression method
        //     0x00, 0x00, // Last mod file time
        //     0x00, 0x00, // Last mod file date
        //     0x00, 0x00, 0x00, 0x00, // Crc-32
        //     0x00, 0x00, 0x00, 0x00, // Compressed size
        //     0x00, 0x00, 0x00, 0x00, // Uncompressed size
        //     0x0a, 0x00, // File name length
        //     0x00, 0x00, // Extra field length
        //     0x43, 0x68, 0x65, 0x63, 0x6b, 0x32, 0x2e, 0x74, 0x78, 0x74, // File name (length from file name length)
        //     // Extra field
        // ], 0);
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Get_General_Purpose_Bit_Flag_0()
    {
        var localFileHeader = new LocalFileHeader(
        [
            0x50, 0x4b, 0x03, 0x04, // Signature
            0x00, 0x00, // Version needed to extract
            0b1000_0000, 0b0000_0000, // General purpose bit flag
            0x00, 0x00, // Compression method
            0x00, 0x00, // Last mod file time
            0x00, 0x00, // Last mod file date
            0x00, 0x00, 0x00, 0x00, // Crc-32
            0x00, 0x00, 0x00, 0x00, // Compressed size
            0x00, 0x00, 0x00, 0x00, // Uncompressed size
            0x0a, 0x00, // File name length
            0x00, 0x00, // Extra field length
            0x43, 0x68, 0x65, 0x63, 0x6b, 0x32, 0x2e, 0x74, 0x78, 0x74, // File name (length from file name length)
            // Extra field
        ], 0);
        
        // List<byte[]> input =
        // [
        //     [0b1000_0000, 0b0000_0000],
        //     [0b0100_0000, 0b0000_0000],
        //     [0b0010_0000, 0b0000_0000],
        //     [0b0001_0000, 0b0000_0000],
        //     [0b0000_1000, 0b0000_0000],
        //     [0b0000_0100, 0b0000_0000],
        //     [0b0000_0010, 0b0000_0000],
        //     [0b0000_0001, 0b0000_0000],
        //     [0b0000_0000, 0b1000_0000],
        //     [0b0000_0000, 0b0100_0000],
        //     [0b0000_0000, 0b0010_0000],
        //     [0b0000_0000, 0b0001_0000],
        //     [0b0000_0000, 0b0000_1000],
        //     [0b0000_0000, 0b0000_0100],
        //     [0b0000_0000, 0b0000_0010],
        //     [0b0000_0000, 0b0000_0001]
        // ];
        
        // List<bool[]> desiredOutput =
        // [
        //     [true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false],
        //     [false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false],
        //     [false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false],
        //     [false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false],
        //     [false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false],
        //     [false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false],
        //     [false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false],
        //     [false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false],
        //
        //     [false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false],
        //     [false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false],
        //     [false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false],
        //     [false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false],
        //     [false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false],
        //     [false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false],
        //     [false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false],
        //     [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true]
        // ];
        //
        // for (var i = 0; i < 16; i++)
        // {
        //     localFileHeader.GeneralPurposeBitFlag = input[i];
        //     Assert.That(localFileHeader.GetGeneralPurposeBitFlags(), Is.EqualTo(desiredOutput[i]));
        // }

        bool[] desiredOutput =
        [
            true, false, false, false, false, false, false, false, 
            false, false, false, false, false, false, false, false
        ];
        
        Assert.That(localFileHeader.GetGeneralPurposeBitFlags(), Is.EqualTo(desiredOutput));
    }

    [Test]
    public void Get_General_Purpose_Bit_Flag_15()
    {
        var localFileHeader = new LocalFileHeader(
        [
            0x50, 0x4b, 0x03, 0x04, // Signature
            0x00, 0x00, // Version needed to extract
            0b0000_0000, 0b0000_0001, // General purpose bit flag
            0x00, 0x00, // Compression method
            0x00, 0x00, // Last mod file time
            0x00, 0x00, // Last mod file date
            0x00, 0x00, 0x00, 0x00, // Crc-32
            0x00, 0x00, 0x00, 0x00, // Compressed size
            0x00, 0x00, 0x00, 0x00, // Uncompressed size
            0x0a, 0x00, // File name length
            0x00, 0x00, // Extra field length
            0x43, 0x68, 0x65, 0x63, 0x6b, 0x32, 0x2e, 0x74, 0x78, 0x74, // File name (length from file name length)
            // Extra field
        ], 0);
        
        bool[] desiredOutput =
        [
            false, false, false, false, false, false, false, false, 
            false, false, false, false, false, false, false, true
        ];
        
        Assert.That(localFileHeader.GetGeneralPurposeBitFlags(), Is.EqualTo(desiredOutput));
    }

    [Test]
    public void Get_Last_Modified_Date_Time()
    {
        var localFileHeader = new LocalFileHeader(
        [
            0x50, 0x4b, 0x03, 0x04, // Signature
            0x00, 0x00, // Version needed to extract
            0x00, 0x00, // General purpose bit flag
            0x00, 0x00, // Compression method
            0b0100_0100, 0b0101_1111, // Last mod file time CHANGE
            0b1011_0001, 0b0101_1000, // Last mod file date CHANGE
            0x00, 0x00, 0x00, 0x00, // Crc-32
            0x00, 0x00, 0x00, 0x00, // Compressed size
            0x00, 0x00, 0x00, 0x00, // Uncompressed size
            0x0a, 0x00, // File name length
            0x00, 0x00, // Extra field length
            0x43, 0x68, 0x65, 0x63, 0x6b, 0x32, 0x2e, 0x74, 0x78, 0x74, // File name "Check2.txt"(length from file name length)
            // Extra field
        ], 0);

        var expected = new DateTime(2024, 5, 17, 11, 58, 8);
        Assert.That(localFileHeader.GetLastModifiedDateTime(), Is.EqualTo(expected));
    }
}