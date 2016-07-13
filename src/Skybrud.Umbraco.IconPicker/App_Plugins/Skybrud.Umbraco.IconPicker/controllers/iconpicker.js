angular.module("umbraco").controller("Skybrud.IconPicker", function ($scope, $http, dialogService) {
   
    $scope.addIcons = function () {
           
        dialogService.open({
            template: '/App_Plugins/Skybrud.Umbraco.IconPicker/Dialogs/iconDialog.html',
            dialogData: $scope.icons,
            config: $scope.model.config,
            callback: function (data) {
                $scope.model.value = data;
            }
        });
    };

    $scope.removeIcon = function() {
        $scope.model.value = '';
    }
});