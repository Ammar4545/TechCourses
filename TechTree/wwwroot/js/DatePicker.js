$(function () {
    $('.datepicker').datepicker(
        {
            dateFormat: 'yy-mm-dd',
            minDate: new Date(),
            maxDate: AddTwoMonthes(new Date(),2)

        }
    );
    function AddTwoMonthes(date, numMonths) {
        var months = date.getMonth();
        var milliSec = new Date(date).setMonth(months + numMonths);
        return new Date(milliSec);
    }
});