public static class MoneyFormatter
{
    public static string FormatMoney(double value)
    {
        if (value >= 1_000_000_000)
            return (value / 1_000_000_000d).ToString("0.##") + "B";
        else if (value >= 1_000_000)
            return (value / 1_000_000d).ToString("0.##") + "M";
        else if (value >= 1_000)
            return (value / 1_000d).ToString("0.##") + "K";
        else
            return value.ToString("0");
    }
}
