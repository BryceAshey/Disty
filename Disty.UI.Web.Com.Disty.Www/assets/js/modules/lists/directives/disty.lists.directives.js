
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
        'ngCookies',
        'disty.lists.service'
	]);

	(function ($ng, $module) {

	    function Directive($stateParams, $state, $ngDialog, $distributionListService, $ngCookies) {
	        
	        return {
	            restrict: 'EA',
	            templateUrl: window.distyConfig.baseUrl + '/assets/html/partials/lists/list.html',
	            scope: {
	                ngModel: '='
	            },
	            link: function (scope) {
	                scope.deleteList = function (id, $event) {
	                    scope.hideDialog = $ngCookies.rememberMe;
	                    if (scope.hideDialog == 'true') {
	                        performDelete(id, $event);
	                    } else {
	                        deleteDialog(id, $event, performDelete);
	                    }	                    
	                }

	                function performDelete(id, $event) {
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

	                function deleteDialog(id, $event, callback) {
	                    $ngDialog.openConfirm({
	                        template: 'deleteDialog',
	                        closeByEscape: true,
	                        closeByDocument: false
	                    }).then(function (value) {                      
	                        callback(id, $event);                    
	                        if (document.getElementById('hideBox').checked) {
	                            $ngCookies.rememberMe = 'true';
	                        }

	                    }, function (value) {
	                        if (document.getElementById('hideBox').checked) {
	                            $ngCookies.rememberMe = 'true';
	                        }
	                    });
	                }
                }
	        };
		}

	    $module.directive('distributionListsUl',
        ['$stateParams', '$state', 'ngDialog', '$distributionListService', '$cookies', Directive]);

	})(ng, module);
	
})(angular);