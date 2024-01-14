function AddTwoMonthes(date, numMonths)
{
    var months = date.getMonth();
    var milliSec = new Date(date).setMonth(months + numMonths);
    return new Date(milliSec);
}