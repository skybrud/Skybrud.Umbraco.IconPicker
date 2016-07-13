angular.module("umbraco").controller("Skybrud.IconDialog.Controller", function ($scope, $http) {
    $scope.icons = [];
    $scope.config = $scope.dialogOptions.config;
    
    $http.get('/Umbraco/backoffice/Api/Icon/GetIcons/?path=' + $scope.config.iconpath).success(function (r) {
        $scope.icons = r;
         
    });

    $scope.selectIcon = function(icon) {
        $scope.submit(icon);
    }
});