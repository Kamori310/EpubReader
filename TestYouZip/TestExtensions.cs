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
        public void Byte_Array_To_UShort_Big_Endian()
        {
            byte[] valueToBeConverted = [0x0f, 0xac];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShort(false), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Big_Endian_Max()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShort(false), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Big_Endian_Min()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShort(false), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Byte_Array_To_UShort_Little_Endian()
        {
            byte[] valueToBeConverted = [0xac, 0x0f];
            const ushort expectedValue = 4012;
            Assert.That(valueToBeConverted.ToUShort(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Little_Endian_Max()
        {
            byte[] valueToBeConverted = [0xff, 0xff];
            const ushort expectedValue = 65535;
            Assert.That(valueToBeConverted.ToUShort(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_UShort_Little_Endian_Min()
        {
            byte[] valueToBeConverted = [0x00, 0x00];
            const ushort expectedValue = 0;
            Assert.That(valueToBeConverted.ToUShort(), Is.EqualTo(expectedValue));
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

        [Test]
        public void Byte_Array_To_DateTime()
        {
            byte[] valueToBeConverted = [0b1011_0001, 0b0101_1000];
            var expectedValue = new DateTime(2024, 5, 17);
            Assert.That(valueToBeConverted.DateFromMsDosFormat(), Is.EqualTo(expectedValue));
        }

        [Test]
        public void Byte_Array_To_TimeSpan()
        {
            byte[] valueToBeConverted = [0b0100_0100, 0b0101_1111];
            var expectedValue = new TimeSpan(11, 58, 8);
            Assert.That(valueToBeConverted.TimeFromMsDosFormat(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Byte_Array_To_TimeSpan_2()
        {
            byte[] valueToBeConverted = [0b1101_0010, 0b0111_1110];
            var expectedValue = new TimeSpan(15, 54, 36);
            Assert.That(valueToBeConverted.TimeFromMsDosFormat(), Is.EqualTo(expectedValue));
        }
    }
}