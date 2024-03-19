namespace Z85Net.Tests;

public class Z85DecodeTests
{
    public static IEnumerable<object[]> DecodeTestData()
    {
        yield return ["HelloWorld", new byte[] { 0x86, 0x4F, 0xD2, 0x6F, 0xB5, 0x59, 0xf7, 0x5b }];
        yield return ["Z85Encoder", new byte[] { 0xbe, 0x17, 0x30, 0x11, 0x26, 0x38, 0x92, 0x2a }];
        yield return ["WeLoveYou!", new byte[] { 0xb4, 0xfe, 0x77, 0x9e, 0x2d, 0xc4, 0x30, 0xcc }];
    }

    [Theory]
    [MemberData(nameof(DecodeTestData))]
    public void WhenInputValid_ShouldProduceCorrectByteArray(string data, byte[] expected)
    {
        byte[] result = Z85Encoder.GetBytes(data);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void WhenInputLengthNotMultipleOf5_ShouldThrowArgumentException()
    {
        string data = "HelloWorldX";
        Assert.Throws<ArgumentException>(() => Z85Encoder.GetBytes(data));
    }
}
