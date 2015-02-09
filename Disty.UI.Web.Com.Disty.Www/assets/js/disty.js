
var disty = {};
distyConfig.modules = (distyConfig && distyConfig.modules) ? distyConfig.modules : [];

(function (ng) {
    'use strict';

    disty = ng.module('disty', distyConfig.modules);

    (function () {

        function Config($routeProvider) {
            distyConfig.setRoutes($routeProvider);
        }

        disty.config(['$routeProvider', Config]);

    })();

    (function () {

        function Config($httpProvider) {
            //$httpProvider.interceptors.push('disty.httpErrorInterceptor');
            //$httpProvider.interceptors.push('loggingHttpInterceptor');
            //$httpProvider.interceptors.push('disty.common.routing.defaultHeaders');
        }

        disty.config(['$httpProvider', Config]);

    })();

})(angular);