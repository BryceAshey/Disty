
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
		'disty.lists.service'
	]);

	(function ($ng, $module) {

	    function Directive($stateParams, $state, $distributionListService) {
	        
	        return {
	            restrict: 'EA',
	            templateUrl: '/assets/html/partials/lists/list.html',
	            scope: {
	                ngModel: '='
	            },
                link: function(scope) {
                    scope.deleteList = function (id, $event) {
                        $event.preventDefault();
                        $distributionListService.del(id).then(function () {
                            scope.ngModel = _.without(scope.ngModel, _.findWhere(scope.ngModel, { id: id }));
                            console.log($stateParams);
                            if ($stateParams.listId == id) {
                                $state.go('home');
                            }
                        }, function (error) {
                            console.log('has failed... ' + error);
                        });
                    }
                }
	        };
		}

	    $module.directive('distributionListsUl', ['$stateParams', '$state', '$distributionListService', Directive]);

	})(ng, module);
	
})(angular);