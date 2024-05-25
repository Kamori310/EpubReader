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
        
        List<byte[]> input = new List<byte[]>()
        {
            new byte[] { 0b1000_0000, 0b0000_0000 },
            new byte[] { 0b0100_0000, 0b0000_0000 },
            new byte[] { 0b0010_0000, 0b0000_0000 },
            new byte[] { 0b0001_0000, 0b0000_0000 },
            new byte[] { 0b0000_1000, 0b0000_0000 },
            new byte[] { 0b0000_0100, 0b0000_0000 },
            new byte[] { 0b0000_0010, 0b0000_0000 },
            new byte[] { 0b0000_0001, 0b0000_0000 },
            new byte[] { 0b0000_0000, 0b1000_0000 },
            new byte[] { 0b0000_0000, 0b0100_0000 },
            new byte[] { 0b0000_0000, 0b0010_0000 },
            new byte[] { 0b0000_0000, 0b0001_0000 },
            new byte[] { 0b0000_0000, 0b0000_1000 },
            new byte[] { 0b0000_0000, 0b0000_0100 },
            new byte[] { 0b0000_0000, 0b0000_0010 },
            new byte[] { 0b0000_0000, 0b0000_0001 },
        };
        
        List<bool[]> desiredOutpout = new List<bool[]>()
        {
            new bool[] {true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
            new bool[] {false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false}, 
            new bool[] {false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false},
            new bool[] {false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false},
            new bool[] {false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false},
            new bool[] {false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false},
            new bool[] {false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false},
            new bool[] {false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false},
            
            new bool[] {false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false},
            new bool[] {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true},
        };

        for (var i = 0; i < 16; i++)
        {
            localFileHeader.GeneralPurposeBitFlag = input[i];
            Assert.That(localFileHeader.GetGeneralPurposeBitFlags(), Is.EqualTo(desiredOutpout[i]));
        }
        
    }
}