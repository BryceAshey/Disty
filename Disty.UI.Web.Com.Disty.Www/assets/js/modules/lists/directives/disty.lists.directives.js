
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', [
		'disty.lists.service'
	]);

	(function ($ng, $module) {

	    function Directive($distributionListService) {
	        var listData;
	        
	        $distributionListService.getAll().then(function (data) {
	            console.log(data);
	            listData = data;
	        }, function (error) {
	            console.log('has failed... ' + error);
	        });

			return {
				restrict: 'EA',
				templateUrl: '/assets/html/lists/list.html',
				scope: {
				    lists: listData
				},			    
			};
		}

		$module.directive('distributionListsUl', ['$distributionListService', Directive]);

	})(ng, module);
	
})(angular);