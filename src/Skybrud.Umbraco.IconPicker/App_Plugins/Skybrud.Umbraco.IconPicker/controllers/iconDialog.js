angular.module("umbraco").controller("Skybrud.IconDialog.Controller", function ($scope, $http) {
    $scope.icons = [];
       
    $http.get('/Umbraco/backoffice/Api/Icon/GetIcons/?path=/svgs/icons/').success(function (r) {
        $scope.icons = r;
         
    });

    $scope.selectIcon = function(icon) {
        $scope.submit(icon);
    }
});