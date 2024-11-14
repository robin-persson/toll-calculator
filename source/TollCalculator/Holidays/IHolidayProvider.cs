namespace TollFeeCalculator.Holidays;

public interface IHolidayProvider
{
    //
    // Summary:
    //     Get Swedish Holidays of a given year
    //
    // Parameters:
    //   year:
    //     The year for which to get holidays
    //
    // Returns:
    //     Set of DateTime objects whose dates represent holidays
    //     for given year
    IEnumerable<DateTime> GetHolidays(int year);
}
