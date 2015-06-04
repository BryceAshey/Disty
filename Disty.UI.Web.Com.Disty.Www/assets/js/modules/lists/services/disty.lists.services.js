/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.lists.service', [
        'disty.common.api.services'
    ]);

    //lists.service 
    (function ($ng, $module) {

        function Service($q, $compose, $distributionListResource) {
            var $this = this;

            return {

                getAll: function (callback) {

                    var deferredObject = $q.defer();

                    // retrieve the information...
                    // no caching here. but can easily be added.
                    $distributionListResource
                        .query()
                        .$promise
                        .then(function (result) {
                            deferredObject.resolve(result);
                        }, function (errorMsg) {
                            deferredObject.reject(errorMsg);
                        });

                    return deferredObject.promise;
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

        $module.factory('$distributionListService', ['$q', '$compose', '$distributionListResource', Service]);

    })(ng, module);

})(angular);