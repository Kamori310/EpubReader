using YouZip.Extensions;

namespace TestYouZip;

public class TestBooleanArrayExtensions
{
    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void TearDown()
    {
    }

    [TestCase(new bool[] { }, "")]
    [TestCase(new[] { true, false, true }, "True False True ")]
    public void Format_String(bool[] input, string expectedOutput)
    {
        Assert.That(input.FormatString(), Is.EqualTo(expectedOutput));
    }
}