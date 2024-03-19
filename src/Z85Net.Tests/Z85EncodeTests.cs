namespace Z85Net.Tests;

public class Z85EncodeTests
{
    public static IEnumerable<object[]> EncodeTestData()
    {
        yield return [new byte[] { 0x86, 0x4F, 0xD2, 0x6F, 0xB5, 0x59, 0xf7, 0x5b }, "HelloWorld"];
        yield return [new byte[] { 0xbe, 0x17, 0x30, 0x11, 0x26, 0x38, 0x92, 0x2a }, "Z85Encoder"];
        yield return [new byte[] { 0xb4, 0xfe, 0x77, 0x9e, 0x2d, 0xc4, 0x30, 0xcc }, "WeLoveYou!"];
    }

    [Theory]
    [MemberData(nameof(EncodeTestData))]
    public void WhenInputValid_ShouldProduceCorrectString(byte[] data, string expected)
    {
        string result = Z85Encoder.GetString(data);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void WhenInputLengthNotMultipleOf4_ShouldThrowArgumentException()
    {
        byte[] data = [0x86, 0x4F, 0xD2, 0x6F, 0xB5, 0x59, 0xf7];
        Assert.Throws<ArgumentException>(() => Z85Encoder.GetString(data));
    }
}
