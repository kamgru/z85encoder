namespace Z85Net;

public static class Z85Encoder
{
    private const int Base85 = 85;

    private static readonly char[] EncodingTable =
    [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z', '.', '-', ':', '+', '=', '^', '!', '/',
        '*', '?', '&', '<', '>', '(', ')', '[', ']', '{',
        '}', '@', '%', '$', '#'
    ];

    private static readonly uint[] DecodingTable =
    [
        0, 68, 0, 84, 83, 82, 72, 0,
        75, 76, 70, 65, 0, 63, 62, 69,
        0, 1, 2, 3, 4, 5, 6, 7,
        8, 9, 64, 0, 73, 66, 74, 71,
        81, 36, 37, 38, 39, 40, 41, 42,
        43, 44, 45, 46, 47, 48, 49, 50,
        51, 52, 53, 54, 55, 56, 57, 58,
        59, 60, 61, 77, 0, 78, 67, 0,
        0, 10, 11, 12, 13, 14, 15, 16,
        17, 18, 19, 20, 21, 22, 23, 24,
        25, 26, 27, 28, 29, 30, 31, 32,
        33, 34, 35, 79, 0, 80, 0, 0,
    ];

    /// <summary>
    /// Encodes a byte array into a Z85 string. The input length must be a multiple of 4.
    /// </summary>
    /// <param name="data">The input byte array to encode</param>
    /// <returns> The Z85 encoded string</returns>
    /// <exception cref="ArgumentException"> Thrown when the input length is not a multiple of 4</exception>
    public static string GetString(byte[] data)
    {
        int quartet = data.Length / 4;
        if (data.Length % 4 != 0)
        {
            throw new ArgumentException("Input length must be a multiple of 4", nameof(data));
        }

        Span<char> result = stackalloc char[quartet * 5];
        uint value;
        for (int i = 0; i < quartet; i++)
        {
            int offset = i * 4;
            value = (uint)(data[offset] << 24);
            value += (uint)data[offset + 1] << 16;
            value += (uint)data[offset + 2] << 8;
            value += data[offset + 3];

            offset = (i + 1) * 5 - 1;
            result[offset] = EncodingTable[value % Base85];
            value /= Base85;

            result[offset - 1] = EncodingTable[value % Base85];
            value /= Base85;

            result[offset - 2] = EncodingTable[value % Base85];
            value /= Base85;

            result[offset - 3] = EncodingTable[value % Base85];
            value /= Base85;

            result[offset - 4] = EncodingTable[value % Base85];
        }

        return new string(result);
    }

    /// <summary>
    /// Decodes a Z85 string into a byte array. The input length must be a multiple of 5.
    /// </summary>
    /// <param name="data"> The input Z85 string to decode</param>
    /// <returns> The decoded byte array</returns>
    /// <exception cref="ArgumentException"> Thrown when the input length is not a multiple of 5</exception>
    public static byte[] GetBytes(string data)
    {
        if (data.Length % 5 != 0)
        {
            throw new ArgumentException("Input length must be a multiple of 5", nameof(data));
        }

        byte[] output = new byte[data.Length / 5 * 4];
        int outputIndex = 0;
        for (int i = 0; i < data.Length; i += 5)
        {
            uint value = 0;
            value = value * Base85 + DecodingTable[data[i] - 32];
            value = value * Base85 + DecodingTable[data[i + 1] - 32];
            value = value * Base85 + DecodingTable[data[i + 2] - 32];
            value = value * Base85 + DecodingTable[data[i + 3] - 32];
            value = value * Base85 + DecodingTable[data[i + 4] - 32];

            output[outputIndex] = (byte)(value >> 24);
            output[outputIndex + 1] = (byte)(value >> 16);
            output[outputIndex + 2] = (byte)(value >> 8);
            output[outputIndex + 3] = (byte)value;
            outputIndex += 4;
        }

        return output;
    }
}
