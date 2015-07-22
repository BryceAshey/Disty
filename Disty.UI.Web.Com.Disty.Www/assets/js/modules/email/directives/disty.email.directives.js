
(function (ng) {
	'use strict';

	var module = ng.module('disty.email.directives', [
		'disty.email.service'
	]);

	(function ($ng, $module) {

		function Directive($emailService) {

		    return {
		        restrict: 'EA',
		        templateUrl: window.distyConfig.baseUrl + '/assets/html/partials/email/addEmail.html',
		        scope: {
		            ngModel: '='
		        },
		        link: function(scope, element, attributes) {
		            scope.email = { name: '', address: '' };
		            
		            scope.save = function (e) {
		            	e.preventDefault();

		            	$emailService.create(scope.email.name, scope.email.address, scope.ngModel.id, function (result) {
			                scope.ngModel.emails.push({
		                        id: result.id,
		                        listId: scope.ngModel.id,
		                        name: scope.email.name,
		                        address: scope.email.address
		                    });

		                    scope.email = { name: '', address: '' };
		                });
		            };
		        }
		    };
		}

		$module.directive('addEmail', ['$emailService', Directive]);

	})(ng, module);

})(angular);