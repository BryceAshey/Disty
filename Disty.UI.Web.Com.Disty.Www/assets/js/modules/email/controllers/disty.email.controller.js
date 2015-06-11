/*global angular*/
(function ($ng) {
    'use strict';

    var module = $ng.module('disty.email.controller', [
        'disty.common.lib.services'
    ]);

    //email.controller 
    (function () {

        function Controller($scope, $ngDialog) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;
            
            

            return this;

        }

        Controller.prototype = {

        };

        module.controller('addEmail.controller',
            ['$scope', Controller]);

    })();


})(angular);