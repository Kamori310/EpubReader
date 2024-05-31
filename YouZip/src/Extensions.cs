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

    public static TimeSpan TimeFromMsDosFormat(this byte[] input)
    {
        int[] bitValues = new[] { 1, 2, 4, 8, 16, 32, 64 };
        
        var inputAsReversedString = input
            .Reverse()
            .Select(it => Convert.ToString(it, 2).PadLeft(8).Replace(' ', '0'))
            .ToArray()
            .Aggregate("", (acc, curr) => acc + curr);
        
        var hours = inputAsReversedString
            .Take(5)
            .Reverse()
            .Select((character, index) =>
            {
                if (character == '0')
                    return 0;
                else 
                    return bitValues[index];
            })
            .Aggregate(0, (acc, curr) => acc + curr);

        var minutes = inputAsReversedString
            .Skip(5)
            .Take(6)
            .Reverse()
            .Select((character, index) =>
            {
                if (character == '0')
                    return 0;
                else 
                    return bitValues[index];
            })
            .Aggregate(0, (acc, curr) => acc + curr);

        var seconds = inputAsReversedString
            .Skip(5 + 6)
            .Take(5)
            .Reverse()
            .Select((character, index) =>
            {
                if (character == '0')
                    return 0;
                else 
                    return bitValues[index];
            })
            .Aggregate(0, (acc, curr) => acc + curr * 2); // * 2 because number of seconds is halved are saved in the format
        
        return new TimeSpan(hours,minutes,seconds);
    }

    public static DateTime DateFromMsDosFormat(this byte[] input)
    {
        // 10110001                         01011000
        // 01011000                         10110001
        
        // Actual Date: 2024-05-17
        // 17      1  0  0  0  1
        //        16  8  4  2  1
        
        // 05      0  1  0  1
        //         8  4  2  1
        
        // 44      0  1  0  1  1  0  0
        //        64 32 16  8  4  2  1
        
        // Year offset since 1980
        //  64 32 16  8  4  2  1
        //   0  1  0  1  1  0  0
        
        // Month
        //  8  4  2  1
        //  0  1  0  1
        
        // Day of Month
        // 16  8  4  2  1
        //  1  0  0  0  1
        int[] bitValues = new[] { 1, 2, 4, 8, 16, 32, 64 };

        var inputAsReversedString = input
            .Reverse()
            .Select(it => Convert.ToString(it, 2).PadLeft(8).Replace(' ', '0'))
            .ToArray()
            .Aggregate("", (acc, curr) => acc + curr);

        var year = inputAsReversedString
            .Take(7)
            .Reverse()
            .Select((character, index) =>
            {
                if (character == '0')
                    return 0;
                else 
                    return bitValues[index];
            })
            .Aggregate(1980, (acc, curr) => acc + curr);

        var month = inputAsReversedString
            .Skip(7)
            .Take(4)
            .Reverse()
            .Select(
                (character, index) =>
                {
                    if (character == '0')
                        return 0;
                    else
                        return bitValues[index];
                })
            .Aggregate(0, (acc, curr) => acc + curr);
        
        var dayOfMonth = inputAsReversedString
            .Skip(7 + 4)
            .Take(5)
            .Reverse()
            .Select(
                (character, index) =>
                {
                    if (character == '0')
                        return 0;
                    else
                        return bitValues[index];
                })
            .Aggregate(0, (acc, curr) => acc + curr);

        return new DateTime(year, month, dayOfMonth);
    }

}