using YouZip;

namespace TestExtensions;

public class TestExtensions
{
    [SetUp]
    public void Setup()
    {
    }


    public class Test_Byte_Array_Extensions
    {
        [Test]
        public void Byte_Array_To_UShort_Big_Endian()
        {
            byte[] valueToBeConverted = [0x0f, 0xac];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Big_Endian_Max()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Big_Endian_Min()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShortBigEndian(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Byte_Array_To_UShort_Little_Endian()
        {
            byte[] valueToBeConverted = [0xac, 0x0f];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Little_Endian_Max()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Little_Endian_Min()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShortLittleEndian(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UInt_Little_Endian_Min()
        {
            byte[] valueToBeConverted = [0x00, 0x00, 0x00, 0x00];
            const uint expectedValue = 0;
            Assert.That(valueToBeConverted.ToUInt(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Byte_Array_To_UInt_Little_Endian()
        {
            byte[] valueToBeConverted = [0x01, 0x00, 0x00, 0x00];
            const uint expectedValue = 1;
            Assert.That(valueToBeConverted.ToUInt(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Byte_Array_To_UInt_Little_Endian_Test_2()
        {
            byte[] valueToBeConverted = [0x0b, 0x00, 0x00, 0x00];
            const uint expectedValue = 11;
            Assert.That(valueToBeConverted.ToUInt(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UInt_Little_Endian_Max()
        {
            byte[] valueToBeConverted = [0xff, 0xff, 0xff, 0xff];
            const uint expectedValue = 4294967295;
            Assert.That(valueToBeConverted.ToUInt(), Is.EqualTo(expectedValue));
        }
    }
}