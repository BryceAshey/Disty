/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.lists.service', [
        'disty.common.api.services'
    ]);

    //lists.service 
    (function ($ng, $module) {

        function Service($compose, $distributionListResource) {
            var $this = this;

            return {

                getAll: function(callback) {
                    var getResource = new $distributionListResource();
                    getResource.$get(function (object, responseHeaders) {
                        console.log('here', object)
                        $compose.sanitizeCallback(callback)(object);
                    });
                },

                create: function (name, callback) {

                    var createResource = new $distributionListResource({ name: name });
                    createResource.$create(function (object, responseHeaders) {
                        $compose.sanitizeCallback(callback)($compose.apiLocation(responseHeaders));
                    });

                },
                // Add other methods here...
            }
        }

        $module.factory('$distributionListService', ['$compose', '$distributionListResource', Service]);

    })(ng, module);

})(angular);