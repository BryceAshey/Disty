/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.lists.controller', [
        'disty.lists.service',
        'disty.email.controller',
        'disty.email.service'
    ]);

    //list.controller 
    (function() {

        function Controller($scope, $stateParams, $distributionListService, $emailService) {
            var $this = this;
            //Make services and models available to object
            $this.$scope = $scope;
            $this.$stateParams = $stateParams;
            $this.$distributionListService = $distributionListService;
            $this.$emailService = $emailService;

            $distributionListService.get($stateParams.listId).then(function(data) {
                $scope.list = data;
            }, function(error) {
                console.log('has failed... ' + error);
            });
            
            $scope.deleteEmail = function (id) {
                $emailService.del($scope.list.id, id).then(function () {
                    $scope.list.emails = _.without($scope.list.emails, _.findWhere($scope.list.emails, { id: id }));
                }, function (error) {
                    console.log('has failed... ' + error);
                });
            }

            return this;
        }

        Controller.prototype = {

        };

        module.controller('list.controller',
        ['$scope', '$stateParams', '$distributionListService', '$emailService', Controller]);

    })();
    
})(angular);