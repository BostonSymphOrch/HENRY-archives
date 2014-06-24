var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var WebServices = BSO.Model.extend({
        initialize: function () {
            this.pendingServiceCalls = [];
        },

        baseAjaxPost: function (url, data, callback, context) {
            BSO.Dispatch.trigger('didStartNetworkCalls');

            this.pendingServiceCalls.push($.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                retryLimit: 5,
                tryCount: 0,
                url: url,
                data: data,
                success: _.bind(function (response) {
                    this.baseSuccessCallback(response, callback, context);
                }, this),
                error: function (xhr, textStatus, errorThrown) {
                    this.tryCount++;
                    if (this.tryCount <= this.retryLimit) {
                        //try again
                        console.log("Network call ", this, "failed " + this.tryCount + " times. Trying again.");
                        $.ajax(this);
                        return;
                    }

                    console.log("Network call failed after 5 tries... Not trying again.", this);
                    this.pendingServiceCalls.pop();
                    return;
                }
            }));
        },

        baseSuccessCallback: function (response, callback, context) {
            this.pendingServiceCalls.pop();

            _(callback).bind(context)(response);

            if (this.pendingServiceCalls.length == 0)
                BSO.Dispatch.trigger('didFinishNetworkCalls');
        },

        networkCallFailed: function (err) {
            this.pendingServiceCalls.pop();

            console.log("Network call failed: ", err);
        },

        loadPerformances: function (eventDetailIdList, callback, context) {
            this.baseAjaxPost("ArchiveWebService.asmx/GetEventDetails", JSON.stringify({ eventDetailIds: eventDetailIdList }), callback, context);
        },


        loadArtists: function (artistIdList, callback, context) {
            this.baseAjaxPost("ArchiveWebService.asmx/GetArtistDetails", JSON.stringify({ artistDetailIds: artistIdList }), callback, context);
        },

        loadRepertoires: function (repList, callback, context) {
            this.baseAjaxPost("ArchiveWebService.asmx/GetRepertoires", JSON.stringify({ repertoireIds: repList }), callback, context);
        },

        loadPerformance: function (eventId, callback, context) {
            this.baseAjaxPost("ArchiveWebService.asmx/GetEventDetail", JSON.stringify({ eventId: eventId }), callback, context);
        },

        SaveExportDataToSession: function (htmlData) {
            //$.ajax({
            //    type: "POST",
            //    contentType: 'application/json; charset=utf-8',
            //    dataType: "json",
            //    url: "ArchiveWebService.asmx/SaveExportDataToSession",
            //    data: JSON.stringify({
            //        exportData: htmlData
            //    }),
            //    success: _.bind(this.success, this),
            //    error: _.bind(this.networkCallFailed, this)
            //});

            //Convert <div><ul><li> to <table><tr><td>

            //data = data.Replace("<div>", "<table><tr>");
            htmlData = htmlData.replace(/<div>/ig, "<table><tr>");
            
            //Replace("<ul", "<td rowspan='2'").
            htmlData = htmlData.replace(/<ul/ig, "<td rowspan='2'");

            //Replace("<li>", String.Empty)
            htmlData = htmlData.replace(/<li>/ig, "");

            //Replace("</li>", String.Empty).
            htmlData = htmlData.replace(/<ul/ig, "");

            //Replace("</ul>", "</td>").
            htmlData = htmlData.replace(/<\/ul>/ig, "</td>");

            //Replace("</div>", "</tr></table>");
            htmlData = htmlData.replace(/<\/div>/ig, "</tr></table>");


            // Look for invalid HTML (td inside td renders as td after td)
            //data = data.Replace("<td class=\"tableColumn\">\n\t\t\t\t<td rowspan='2'>", "<td class=\"tableColumn\">");
            htmlData = htmlData.replace(/<td class=\"tableColumn\">\n\t\t\t\t<td rowspan='2'>/ig, "<td class=\"tableColumn\">");

            //data = data.Replace("</td>\n\t\t\t</td>", "</td>");
            htmlData = htmlData.replace(/<\/td>\n\t\t\t<\/td>/ig, "</td>");

            var $htmlData = $(htmlData);

            $($htmlData.find("[class]")).removeAttr("class");

            htmlData = $htmlData[0].outerHTML;

            while (htmlData.indexOf('á') != -1) htmlData = htmlData.replace('á', '&aacute;');
            while (htmlData.indexOf('À') != -1) htmlData = htmlData.replace('À', '&Agrave;');
            while (htmlData.indexOf('é') != -1) htmlData = htmlData.replace('é', '&eacute;');
            while (htmlData.indexOf('í') != -1) htmlData = htmlData.replace('í', '&iacute;');
            while (htmlData.indexOf('ó') != -1) htmlData = htmlData.replace('ó', '&oacute;');
            while (htmlData.indexOf('ú') != -1) htmlData = htmlData.replace('ú', '&uacute;');
            while (htmlData.indexOf('Ó') != -1) htmlData = htmlData.replace('Ó', '&Oacute;');
            while (htmlData.indexOf('Ú') != -1) htmlData = htmlData.replace('Ú', '&Uacute;');
            while (htmlData.indexOf('É') != -1) htmlData = htmlData.replace('É', '&Eacute;');
            while (htmlData.indexOf('È') != -1) htmlData = htmlData.replace('È', '&Egrave;');
            while (htmlData.indexOf('Á') != -1) htmlData = htmlData.replace('Á', '&Aacute');
            while (htmlData.indexOf('Ñ') != -1) htmlData = htmlData.replace('Ñ', '&Ntilde;');
            while (htmlData.indexOf('ñ') != -1) htmlData = htmlData.replace('ñ', '&ntilde;');

            //var output = "<head><style>td{border:1px solid #000;}</style></head>" + data;
            if (!!htmlData && htmlData.length > 0) {
                htmlData = "<head><style>td{border:1px solid #000;}</style></head>" + htmlData;
            }

            //htmlData = $(htmlData).removeAttr("class").removeAttr("style").html();

            var blob = new Blob([htmlData], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-16"
            });

            saveAs(blob, "Archives.xls");


        },

        success: function (data) {
            $('#excelFrame').attr('src', '/handlers/ExportData.ashx');
        }
    });

    return {
        WebServices: WebServices
    };
}(jQuery));