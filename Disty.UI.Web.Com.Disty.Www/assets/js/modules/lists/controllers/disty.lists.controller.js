/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.lists.controller', [
        'disty.lists.service'
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

    //addList.controller 
    (function () {

        function Controller($scope, $ngDialog, $distributionListService) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;

            $scope.list = {};

            // TODO Add code here

            $scope.save = function () {

                $distributionListService.create($scope.list.name, function (data) {
                    console.log(data);
                });
                
                $ngDialog.closeAll();
            }

            return this;

        }

        Controller.prototype = {

        };

        module.controller('addList.controller',
            ['$scope', 'ngDialog', '$distributionListService', Controller]);

    })();

})(angular);