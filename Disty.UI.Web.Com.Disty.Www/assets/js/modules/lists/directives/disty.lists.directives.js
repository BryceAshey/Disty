
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
		'disty.lists.service'
	]);

	(function ($ng, $module) {

	    function Directive() {
	        
	        return {
	            restrict: 'EA',
	            templateUrl: '/assets/html/partials/lists/list.html',
	            scope: {
	                ngModel: '='
	            }
	        };
		}

		$module.directive('distributionListsUl', [Directive]);

	})(ng, module);
	
})(angular);