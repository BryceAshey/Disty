/*global angular*/
(function ($ng) {
    'use strict';

    var module = $ng.module('disty.email.controller', [
        'ngDialog',
        'disty.common.lib.services'
    ]);

    //email.controller 
    (function () {

        function Controller($scope, $ngDialog) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;
            
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

        module.controller('addEmail.controller',
            ['$scope', 'ngDialog', Controller]);

    })();


})(angular);