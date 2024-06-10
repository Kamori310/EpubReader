namespace YouZip.Extensions;

public static class BooleanArrayExtensions
{
    public static string FormatString(this bool[] input)
    {
        return input
            .Select(
                item =>
                    item switch
                    {
                        true => "True",
                        false => "False"
                    })
            .Aggregate("", (acc, curr) => acc + curr + " ");
    }
}