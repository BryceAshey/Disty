
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
		'disty.lists.service'
	]);

	//Course Template Options Directive
	(function ($ng, $module) {

		function Directive() {

			return {
				restrict: 'EA',
				template: '<li>{{column.name}}</li>',
				scope: {
				    item: "=distributionListsUl"
				},			    
			};
		}

		$module.directive('distributionListsUl', [Directive]);


	})(ng, module);
	
})(angular);