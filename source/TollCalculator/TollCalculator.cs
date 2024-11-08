using Nager.Date.HolidayProviders;

namespace TollFeeCalculator;

public class TollCalculator(IHolidayProvider holidayProvider)
{
    const int DAILY_MAXIMUM = 60;

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param timestamps  - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(Vehicle vehicle, IEnumerable<DateTime> timestamps)
    {
        if (Days().Count() != 1)
            throw new ExpectedSingleDateException(
                $"Expected single date but got [{string.Join(", ", Days())}]"
            );

        return Math.Min(Fees(timestamps).Sum(), DAILY_MAXIMUM);

        IEnumerable<DateTime> Days()
        {
            var days = from timestamp in timestamps select timestamp.Date;
            return days.Distinct();
        }

        IEnumerable<int> Fees(IEnumerable<DateTime> timestamps)
        {
            foreach (var hourlyTimestamps in timestamps.SplitByHour())
            {
                var hourlyFees =
                    from timestamp in hourlyTimestamps
                    select GetTollFee(vehicle, timestamp);
                yield return hourlyFees.Max();
            }
        }
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        switch (vehicle)
        {
            case Vehicle.Motorbike:
            case Vehicle.Tractor:
            case Vehicle.Emergency:
            case Vehicle.Diplomat:
            case Vehicle.Foreign:
            case Vehicle.Military:
                return true;
            default:
                return false;
        }
    }

    public int GetTollFee(Vehicle vehicle, DateTime timestamp)
    {
        if (IsTollFreeDate(timestamp) || IsTollFreeVehicle(vehicle))
            return 0;

        switch (timestamp.Hour)
        {
            case 6:
                return timestamp.Minute < 30 ? 8 : 13;
            case 7:
                return 18;
            case 8:
                return timestamp.Minute < 30 ? 13 : 8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return 8;
            case 15:
                return timestamp.Minute < 30 ? 13 : 18;
            case 16:
                return 18;
            case 17:
                return 13;
            case 18:
                return timestamp.Minute < 30 ? 8 : 0;
            default:
                return 0;
        }
    }

    private bool IsTollFreeDate(DateTime date)
    {
        return IsWeekend() || IsJuly() || IsHoliday() || IsDayBeforeHoliday();

        bool IsWeekend()
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return true;
                default:
                    return false;
            }
        }

        bool IsJuly()
        {
            return date.Month == 7;
        }

        bool IsHoliday()
        {
            return _IsHoliday(date);
        }

        bool IsDayBeforeHoliday()
        {
            var nextDay = date.AddDays(1);
            return _IsHoliday(nextDay);
        }

        bool _IsHoliday(DateTime date)
        {
            return holidayProvider.GetHolidays(date.Year).Any(holiday => holiday.Date == date);
        }
    }
}
