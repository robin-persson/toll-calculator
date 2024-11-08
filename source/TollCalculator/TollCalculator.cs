using Nager.Date.HolidayProviders;

namespace TollFeeCalculator;

public class TollCalculator(IHolidayProvider holidayProvider)
{
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0)
                    totalFee -= tempFee;
                if (nextFee >= tempFee)
                    tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60)
            totalFee = 60;
        return totalFee;
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

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            return 0;

        switch (date.Hour)
        {
            case 6:
                return date.Minute < 30 ? 8 : 13;
            case 7:
                return 18;
            case 8:
                return date.Minute < 30 ? 13 : 8;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return 8;
            case 15:
                return date.Minute < 30 ? 13 : 18;
            case 16:
                return 18;
            case 17:
                return 13;
            case 18:
                return date.Minute < 30 ? 8 : 0;
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
