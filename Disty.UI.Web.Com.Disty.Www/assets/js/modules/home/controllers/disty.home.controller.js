﻿/*global angular*/
(function ($ng) {
    'use strict';

    var module = $ng.module('disty.home.controller', [
        'ngDialog',
        'disty.common.lib.services',        
        'disty.lists.controller'
    ]);

    //home.controller 
    (function () {

        function Controller($scope, $ngDialog, $distributionListService) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;
            this.$ngDialog = $ngDialog;

            // TODO Add code here

            $scope.addList = function () {
                $ngDialog.open({
                    template: '/assets/html/partials/lists/addList.html',
                    controller: 'addList.controller'
                });
            }

            $scope.deleteList = function () {
                alert('Delete List');
            }

            $distributionListService.getAll().then(function (data) {
                $scope.lists = data;
            }, function (error) {
                console.log('has failed... ' + error);
            });
            
            return this;

        }

        Controller.prototype = {

        };

        module.controller('home.controller',
            ['$scope', 'ngDialog', '$distributionListService', Controller]);

    })();


})(angular);