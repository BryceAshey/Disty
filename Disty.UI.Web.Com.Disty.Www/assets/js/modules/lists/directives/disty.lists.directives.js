
(function (ng) {
	'use strict';

	var module = ng.module('disty.lists.directives', []);

	//Course Template Options Directive
	(function ($ng, $module) {

		function Directive($compile) {

			return {
				restrict: 'A',
				link: function (scope, element, attrs) {
					//var dataSource = (attrs.data == "") ? "selectedPrograms" : attrs.data;
					//scope.$watch(eval("scope." + dataSource), function () {
						var template = angular.element(
							'<ul class="nav nav-sidebar">' +
								'<li class="active"><a href="#">Overview <span class="sr-only">(current)</span></a></li>' +
								'<li><a href="#">Reports</a></li>' +
								'<li><a href="#">Analytics</a></li>' +
								'<li><a href="#">Export</a></li>' +
							'</ul>');



					//	//'<ul class="course-outcome row" id="program-outcomes-list">' +
					//	//'<li ng-repeat="program in ' + dataSource + '">' +
					//	//'<label for="programs{{program.id}}"><input type="checkbox" name="programlist" id="programs{{program.id}}" ng-model="value[program.id]" value="program.id" ng-click="processSelectedProgram($event, program.id)">' +
					//	//'<span ng-bind="program.name"></span></label>' +
					//	//'</li>' +
					//	//'</ul>');

					//	var linkFunction = $compile(template);
					//	linkFunction(scope);
						element.html(null).append(template);
					//}, true);

				}
			};

		}

		$module.directive('distributionListsUl', ["$compile", Directive]);


	})(ng, module);
	
})(angular);