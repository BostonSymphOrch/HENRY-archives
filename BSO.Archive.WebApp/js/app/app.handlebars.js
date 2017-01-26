/*
 *
 *  BSO Media Center: Handlebars File
 *
 */


(function () {
    Handlebars.registerHelper('eachWithLimit', function (arrayToLoop, maximumResults, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (!arrayToLoop || arrayToLoop.length == 0)
            return options.inverse(this);

        var optionArray = [];

        for (var i = 0 ; i < arrayToLoop.length && i < maximumResults ; i++)
            optionArray.push(fnTrue(arrayToLoop[i]));

        return optionArray.join('');
    });

    Handlebars.registerHelper('greaterThan', function (lvalue, rvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        lvalue = parseFloat(lvalue);
        rvalue = parseFloat(rvalue);

        if (lvalue > rvalue)
            return fnTrue(this);

        return fnFalse(this);
    });

    Handlebars.registerHelper('ifEqual', function (lvalue, rvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        lvalue = parseFloat(lvalue);
        rvalue = parseFloat(rvalue);

        if (lvalue == rvalue)
            return fnTrue(this);

        return fnFalse(this);
    });

    Handlebars.registerHelper('ifNotEqual', function (lvalue, rvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        lvalue = parseFloat(lvalue);
        rvalue = parseFloat(rvalue);

        if (lvalue != rvalue)
            return fnTrue(this);

        return fnFalse(this);
    });
    

    Handlebars.registerHelper('ifShouldCreateFilterGroup', function (lvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (lvalue.getActiveFilters().length > 0 || lvalue.length > 0)
            return fnTrue(this);

        return fnFalse(this);
    });

    Handlebars.registerHelper('hasAvailableFilters', function (lvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (lvalue.length)
            return fnTrue(this);

        return fnFalse(this);
    });

    Handlebars.registerHelper('hasActiveFilters', function (lvalue, options) {
        var fnTrue = options.fn, fnFalse = options.inverse;

        if (lvalue.getActiveFilters().length > 0)
            return fnTrue(this);

        return fnFalse(this);
    });
}());
