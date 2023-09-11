$(function () {
    var l = abp.localization.getResource("WebPush");
	
	var webPushSubscriptionService = window.decisionTree.abp.notification.webPush.webPushSubscriptions.webPushSubscription;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "WebPush/WebPushSubscriptions/CreateModal",
        scriptUrl: "/Pages/WebPush/WebPushSubscriptions/createModal.js",
        modalClass: "webPushSubscriptionCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "WebPush/WebPushSubscriptions/EditModal",
        scriptUrl: "/Pages/WebPush/WebPushSubscriptions/editModal.js",
        modalClass: "webPushSubscriptionEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            endPoint: $("#EndPointFilter").val(),
			p256dh: $("#P256dhFilter").val(),
			auth: $("#AuthFilter").val(),
			userId: $("#UserIdFilter").val(),
			deviceName: $("#DeviceNameFilter").val()
        };
    };

    var dataTable = $("#WebPushSubscriptionsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"],[2, "asc"],[3, "asc"],[4, "asc"],[5, "asc"]],
        ajax: abp.libs.datatables.createAjax(webPushSubscriptionService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('WebPush.WebPushSubscriptions.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('WebPush.WebPushSubscriptions.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    webPushSubscriptionService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "endPoint" },
			{ data: "p256dh" },
			{ data: "auth" },
			{ data: "userId" },
			{ data: "deviceName" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewWebPushSubscriptionButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        webPushSubscriptionService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/web-push/web-push-subscriptions/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'endPoint', value: input.endPoint }, 
                            { name: 'p256dh', value: input.p256dh }, 
                            { name: 'auth', value: input.auth }, 
                            { name: 'userId', value: input.userId }, 
                            { name: 'deviceName', value: input.deviceName }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    
});
