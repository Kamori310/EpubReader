using System.Globalization;

namespace YouZip;

public static class Extensions
{
    public static ushort ToUShort(this byte[] input, bool littleEndian = true)
    {
        if (littleEndian)
            return UInt16.Parse(
                input
                    .Reverse()
                    .Aggregate(
                        "", (acc, curr) =>
                        {
                            var currentValue = Convert.ToString(curr, 16);
                            if (currentValue.Length == 2)
                                return acc + currentValue;
                            else
                                return acc + "0" + currentValue;
                        }),
                NumberStyles.HexNumber
            );
        else
            return UInt16.Parse(
                input
                    .Aggregate(
                        "", (acc, curr) =>
                        {
                            var currentValue = Convert.ToString(curr, 16);
                            if (currentValue.Length == 2)
                                return acc + currentValue;
                            else
                                return acc + "0" + currentValue;
                        }),
                NumberStyles.HexNumber
            );
    }

    public static uint ToUInt(this byte[] input, bool littleEndian = true)
    {
        if (littleEndian)
            return UInt32.Parse(
                input
                    .Reverse()
                    .Aggregate(
                        "", (acc, curr) =>
                        {
                            var currentValue = Convert.ToString(curr, 16);
                            if (currentValue.Length == 2)
                                return acc + currentValue;
                            else
                                return acc + "0" + currentValue;
                        }),
                NumberStyles.HexNumber);
        else
            return UInt32.Parse(
                input
                    .Aggregate(
                        "", (acc, curr) =>
                        {
                            var currentValue = Convert.ToString(curr, 16);
                            if (currentValue.Length == 2)
                                return acc + currentValue;
                            return acc + "0" + currentValue;
                        }), 
                NumberStyles.HexNumber);
    }

    public static string FormatString(this byte[] input, int bytesPerLine = 8)
    {
        return input
            .Select(it =>
            {
                var asString = Convert.ToString(it, 16);
                if (asString.Length == 1)
                    return "0" + asString;
                else
                    return asString;
            })
            .Chunk(bytesPerLine)
            .Select(it => String.Join(" ", it))
            .Aggregate("", (acc, curr) => acc + curr + "\n");
    }

    public static string FormatStringAsBits(this byte[] input)
    {
        return input
            .Select(
                it =>
                {
                    var asString = Convert.ToString(it, 2);
                    if (asString.Length != 8)
                    {
                        return asString.PadLeft(8).Replace(' ', '0');
                    }
                    else
                    {
                        return asString;
                    }
                })
            .Aggregate("", (acc, curr) => acc + curr + "\n");
    }

    public static byte[] Subarray(
        this byte[] input, 
        int startPosition, 
        int subarrayLength)
    {
        return input
            .Skip(startPosition)
            .Take(subarrayLength)
            .ToArray();
    }

    public static bool GetBit(this byte input, int bitNumber)
    {
        if (bitNumber < 0 || bitNumber >= 8)
        {
            throw new ArgumentOutOfRangeException(nameof(bitNumber), "Bit number must be between 0 and 7");
        }

        byte mask = (byte)(1 << bitNumber);
        byte result = (byte)(input & mask);

        return result > 0;
    }

}