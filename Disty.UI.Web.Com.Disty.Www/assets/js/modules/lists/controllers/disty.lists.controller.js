/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.lists.controller', [
        'ngDialog',
        'disty.lists.service',
        'disty.email.controller'
    ]);

    //list.controller 
    (function() {

        function Controller($scope, $stateParams, $ngDialog, $distributionListService) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;
            this.$stateParams = $stateParams;
            this.$ngDialog = $ngDialog;
            this.$distributionListService = $distributionListService;

            $distributionListService.get($stateParams.listId).then(function(data) {
                $scope.list = data;
            }, function(error) {
                console.log('has failed... ' + error);
            });

            $scope.addEmail = function() {
                $ngDialog.open({
                    template: '/assets/html/partials/email/addEmail.html',
                    controller: 'addEmail.controller'
                });
            }

            $scope.deleteEmail = function() {
                alert('Delete Email');
            }

            return this;

        }

        Controller.prototype = {

        };

        module.controller('list.controller',
        ['$scope', '$stateParams', 'ngDialog', '$distributionListService', Controller]);

    })();

    //addList.controller 
    (function () {

        function Controller($scope, $ngDialog, $distributionListService) {
            var $this = this;
            //Make services and models available to object
            this.$scope = $scope;

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