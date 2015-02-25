/*global angular*/
(function ($ng) {
    'use strict';

    var module = $ng.module('disty.lists.controller', [
        
    ]);

    //lists.controller 
    (function () {

        function Controller($scope, requestContext, $route, $routeParams, $loadingService, $timezoneService, $compose) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;

            // TODO Add code here


            return this;

        }

        Controller.prototype = {

        };

        module.controller('lists.controller',
            ['$scope', Controller]);

    })();

})(angular);