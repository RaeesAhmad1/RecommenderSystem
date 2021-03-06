$(function () {
    "use strict";
    $("#fromDate").datepicker({
        defaultDate: "+1w",
        changeMonth: !0,
        numberOfMonths: 3,
        onClose: function (a) {
            $("#toDate").datepicker("option", "minDate", a)
        }
    }), $("#toDate").datepicker({
        defaultDate: "+1w",
        changeMonth: !0,
        numberOfMonths: 3,
        onClose: function (a) {
            $("#fromDate").datepicker("option", "maxDate", a)
        }
    })
}), $(function () {
    "use strict";
    $("#datepicker_multiple_months").datepicker({
        numberOfMonths: 3,
        showButtonPanel: !0
    })
}), $(function () {
    "use strict";
    $(".datepicker").datepicker()
});