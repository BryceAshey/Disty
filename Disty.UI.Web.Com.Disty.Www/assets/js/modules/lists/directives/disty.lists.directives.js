
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
		'disty.lists.service'
	]);

	(function ($ng, $module) {

	    function Directive($distributionListService) {
	        
	        return {
	            restrict: 'EA',
	            templateUrl: '/assets/html/lists/list.html',
	            scope: {
	                ngModel: '='
	            }
	        };
		}

		$module.directive('distributionListsUl', ['$distributionListService', Directive]);

	})(ng, module);
	
})(angular);