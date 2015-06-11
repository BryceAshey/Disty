
(function (ng) {
	'use strict';

	var module = ng.module('disty.email.directives', [
		'disty.email.service'
	]);

	(function ($ng, $module) {

		function Directive($emailService) {

		    console.log(this);

		    return {
		        restrict: 'EA',
		        templateUrl: '/assets/html/partials/email/addEmail.html',
		        scope: {
		            ngModel: '='
		        },
		        link: function(scope, element, attributes) {
		            console.log(scope.addEmail);

		            scope.email = { name: '', address: '' };
		            
		            scope.save = function (e) {
		            	e.preventDefault();

		                $this.$emailService.create(scope.email.name, scope.email.address, scope.ngModel.id, function(id) {
		                	scope.ngModel.emails.push({
		                		id: id,
								listId: scope.ngModel.id,
		                		name: scope.email.name,
		                		address: scope.email.address
		                	});

		                	scope.email = { name: '', address: '' };

		                    console.log(scope.ngModel.emails);
		                });
		            };
		        }
		    };
		}

		$module.directive('addEmail', ['$emailService', Directive]);

	})(ng, module);

})(angular);