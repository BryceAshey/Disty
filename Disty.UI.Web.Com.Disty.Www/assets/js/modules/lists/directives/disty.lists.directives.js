﻿
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
	                    var favoriteCookie = $ngCookies.myFavorite;
	                    $ngCookies.myFavorite = 'oatmeal';
	                    console.log($ngCookies.myFavorite);
	                    $ngDialog.openConfirm({
	                        template: 'deleteDialog',
	                        closeByEscape: true,
                            closeByDocument: false
	                    }).then(function (value) {
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
	                        if (document.getElementById('hideBox').checked) {
	                            alert("checked");
	                        }
	                    }, function (value) {
	                        if (document.getElementById('hideBox').checked) {
	                            alert("checked");
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