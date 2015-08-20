
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
	                    scope.canDelete = false;
	                    scope.rememberCookie = $ngCookies.rememberMe;

	                    //alert("canDelete" + scope.canDelete);
	                    //alert("cookies" + scope.rememberCookie);


	                    deleteDialog(id, $event, performDelete);

	                //    if (scope.canDelete == true) {
	                //        $event.preventDefault();
	                //        $distributionListService.del(id).then(function () {
	                //            scope.ngModel = _.without(scope.ngModel, _.findWhere(scope.ngModel, { id: id }));
	                //            console.log($stateParams);
	                //            if ($stateParams.listId == id) {
	                //                $state.go('home');
	                //            }
	                //        }, function (error) {
	                //            console.log('has failed... ' + error);
	                //        });
	                //    }
	                //}

	                }

	                function performDelete(id, $event) {
	                    if (scope.canDelete == true) {
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

	                function deleteDialog(id, $event, callback) {
	                    //if (scope.rememberCookie == 'true') {
	                    //    alert("Cookies are here");
	                    //}
	                    $ngDialog.openConfirm({
	                        template: 'deleteDialog',
	                        closeByEscape: true,
	                        closeByDocument: false
	                    }).then(function (value) {
	                        scope.canDelete = true;	                      
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