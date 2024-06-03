using YouZip.data;

namespace TestExtensions;

public class TestLocalFileHeader
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Get_General_Purpose_Bit_Flag()
    {
        var localFileHeader = new LocalFileHeader(
        [
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00
        ], 0);
        
        List<byte[]> input =
        [
            [0b1000_0000, 0b0000_0000],
            [0b0100_0000, 0b0000_0000],
            [0b0010_0000, 0b0000_0000],
            [0b0001_0000, 0b0000_0000],
            [0b0000_1000, 0b0000_0000],
            [0b0000_0100, 0b0000_0000],
            [0b0000_0010, 0b0000_0000],
            [0b0000_0001, 0b0000_0000],
            [0b0000_0000, 0b1000_0000],
            [0b0000_0000, 0b0100_0000],
            [0b0000_0000, 0b0010_0000],
            [0b0000_0000, 0b0001_0000],
            [0b0000_0000, 0b0000_1000],
            [0b0000_0000, 0b0000_0100],
            [0b0000_0000, 0b0000_0010],
            [0b0000_0000, 0b0000_0001]
        ];
        
        List<bool[]> desiredOutput =
        [
            [true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false],
            [false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false],
            [false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false],
            [false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false],
            [false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false],
            [false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false],
            [false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false],
            [false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false],

            [false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false],
            [false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false],
            [false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false],
            [false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false],
            [false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false],
            [false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false],
            [false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false],
            [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true]
        ];

        for (var i = 0; i < 16; i++)
        {
            localFileHeader.GeneralPurposeBitFlag = input[i];
            Assert.That(localFileHeader.GetGeneralPurposeBitFlags(), Is.EqualTo(desiredOutput[i]));
        }
    }

    [Test]
    public void Get_Last_Modified_Date_Time()
    {
        var localFileHeader = new LocalFileHeader(
        [
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00
        ], 0);

        localFileHeader.LastModFileDate = [0b1011_0001, 0b0101_1000];
        localFileHeader.LastModFileTime = [0b0100_0100, 0b0101_1111];

        var expected = new DateTime(2024, 5, 17, 11, 58, 8);
        Assert.That(localFileHeader.GetLastModifiedDateTime(), Is.EqualTo(expected));
    }
}