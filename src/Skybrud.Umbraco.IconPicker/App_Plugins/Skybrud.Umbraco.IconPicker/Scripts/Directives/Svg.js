angular.module("umbraco").directive("skybrudSvg", function ($http) {
    return {
        scope: {
            value: "=",
            config: "="
        },
        restrict: "E",
        replace: true,
        template: "<div></div>",
        link: function (scope, element) {

            scope.$watch("value", function () {

                if (!scope.value) {
                    element[0].innerHTML = "";
                    return;
                }

                $http.get("/umbraco/backoffice/Skybrud/IconPicker/GetSvg/?contentTypeAlias=" + scope.config.contentTypeAlias + "&propertyAlias=" + scope.config.propertyAlias + "&icon=" + scope.value).success(function (r) {
                    element[0].innerHTML = r;
                });

            });

        }
    };
});