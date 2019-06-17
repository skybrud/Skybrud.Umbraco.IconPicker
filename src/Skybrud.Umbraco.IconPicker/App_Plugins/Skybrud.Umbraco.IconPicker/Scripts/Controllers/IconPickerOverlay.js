angular.module("umbraco").controller("Skybrud.IconPickerOverlay.Controller", function ($scope, $element, $http) {

    // Get the configuration
    $scope.config = $scope.model.config;

    // Append the class name from the prevalues
    if ($scope.config.className) $element[0].classList.add($scope.config.className);

    $scope.icons = [];
    $http.get("/umbraco/backoffice/Skybrud/IconPicker/GetIcons/?contentTypeAlias=" + $scope.config.contentTypeAlias + "&propertyAlias=" + $scope.config.propertyAlias).success(function (r) {
        $scope.icons = r.icons;
    });

    $scope.selectIcon = function (icon) {
        $scope.model.submit(icon);
    };

});