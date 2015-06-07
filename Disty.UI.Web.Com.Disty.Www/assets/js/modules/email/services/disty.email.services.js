/*global angular*/
(function (ng) {
    'use strict';

    var module = ng.module('disty.email.service', [
        'disty.common.api.services'
    ]);

    //lists.service 
    (function ($ng, $module) {

        function Service($q, $compose, $emailResource) {
            var $this = this;

            return {

                getAll: function () {

                    var deferredObject = $q.defer();

                    // retrieve the information...
                    // no caching here. but can easily be added.
                    $emailResource
                        .query()
                        .$promise
                        .then(function (result) {
                            deferredObject.resolve(result);
                        }, function (errorMsg) {
                            deferredObject.reject(errorMsg);
                        });

                    return deferredObject.promise;
                },

                get: function (listId, emailId) {

                    var deferredObject = $q.defer();

                    // retrieve the information...
                    // no caching here. but can easily be added.
                    $emailResource
                        .get({ listId: listId, emailId: emailId })
                        .$promise
                        .then(function (result) {
                            deferredObject.resolve(result);
                        }, function (errorMsg) {
                            deferredObject.reject(errorMsg);
                        });

                    return deferredObject.promise;
                },

                create: function (name, email, callback) {

                    var createResource = new $emailResource({ name: name, address: email });
                    createResource.$create(function (object, responseHeaders) {
                        $compose.sanitizeCallback(callback)($compose.apiLocation(responseHeaders));
                    });

                },
                // Add other methods here...
            }
        }

        $module.factory('$emailService', ['$q', '$compose', '$emailResource', Service]);

    })(ng, module);

})(angular);