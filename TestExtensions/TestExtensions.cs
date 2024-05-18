using YouZip;

namespace TestExtensions;

public class TestExtensions
{
    [SetUp]
    public void Setup()
    {
    }


    public class TestByteArrayExtensions
    {
        [Test]
        public void ByteArrayToUShortBigEndian()
        {
            byte[] valueToBeConverted = [0x0f, 0xac];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ByteArrayToUShortBigEndianMax()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ByteArrayToUShortBigEndianMin()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void ByteArrayToUShortLittleEndian()
        {
            byte[] valueToBeConverted = [0xac, 0x0f];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ByteArrayToUShortLittleEndianMax()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void ByteArrayToUShortLittleEndianMin()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }
    }
}