(function (ng) {
    'use strict';

    var module = ng.module('disty.common.api.services', [
        'disty.common.lib.services',
        'disty.resources',

    ]);

    /**
    * $compose
    * functional composition utility library
    * This is designed to be a library for common functional composition behaviors.
    * @module $compose
    */
    (function () {

        function Service() {

            /**
            * Returns original callback if it is a function, otherwise it returns noop
            * @memberof! $compose
            * @funciton sanitizeCallback
            * @param {function} callback
            * @returns {function}
            */
            function sanitizeCallback(callback) {
                return (typeof callback === 'function') ? callback : function () {
                };
            }

            /**
            * Safely performs apply on provided scope
            * @memberof! $compose
            * @function safeApply
            * @param {object} $scope - scope provided from a controller
            */
            function safeApply($scope) {
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            }

            /**
            * Executes a sanitized callback with passed parameters and context
            * @memberof! $compose
            * @function safeExecute
            * @param {function} callback
            * @param {array} params - parameters for function call
            * @param {object} context - optional context, defaults to null if not defined
            */
            function safeExecute(callback, params, context) {
                var sanitizedCallback = sanitizeCallback(callback),
                    sanitizedContext = (context) ? context : null;

                sanitizedCallback.apply(sanitizedContext, params);
            }

            /**
            * Verifies a dot-delimited path is reachable in an object
            * @memberof! $compose
            * @function verifyObjectPath
            * @param {object} rootObject - object to verify path against
            * @param {string} objectPath - path to follow against root object
            * @returns {boolean}
            */
            function verifyObjectPath(rootObject, objectPath) {
                var currentObject = rootObject,
                    currentToken = '',
                    pathTokens = objectPath.split('.');

                for (var key in pathTokens) {
                    currentToken = pathTokens[key];

                    currentObject = (currentObject[currentToken]) ? currentObject[currentToken] : null;

                    if (!currentObject) {
                        break;
                    }
                }

                return (currentObject !== null) ? true : false;
            }


            /**
            * Takes the intersection of two arrays using a comparator function
            * @memberof! $compose
            * @function intersect
            * @param {array} array1
            * @param {array} array2
            * @param {function} comparator
            * @returns {array}
            */
            function intersect(array1, array2, comparator) {
                var finalArray = [],
                    element1,
                    element2;

                for (var i = 0; i < array1.length; i++) {
                    element1 = array1[i];
                    for (var j = 0; j < array2.length; j++) {
                        element2 = array2[j];
                        if (comparator(element1, element2)) {
                            finalArray.push(element1);
                        }
                    }
                }

                return finalArray;
            }

            /**
            * Takes the intersection of two arrays using a comparator function
            * @memberof! $compose
            * @function difference
            * @param {array} checkedArray
            * @param {array} comparisonArray
            * @param {function} comparator - returns true if match is found
            * @returns {array}
            */
            function difference(checkedArray, comparisonArray, comparator) {
                var finalArray = [],
                    element1,
                    element2,
                    matched;

                for (var i = 0; i < checkedArray.length; i++) {
                    matched = false;
                    element1 = checkedArray[i];
                    for (var j = 0; j < comparisonArray.length; j++) {
                        element2 = comparisonArray[j];
                        if (comparator(element1, element2)) {
                            matched = true;
                            break;
                        }
                    }
                    if (!matched) {
                        finalArray.push(element1);
                    }
                }

                return finalArray;
            }

            function apiLocation(headers) {
                var location = '' + headers('Location');
                var lastSlashIndex = location.lastIndexOf('/');
                var id = location.substring(lastSlashIndex + 1, location.length);                    

                return {
                    id: id,
                    location: location
                };
            }

            return {
                safeApply: safeApply,
                safeExecute: safeExecute,
                sanitizeCallback: sanitizeCallback,
                verifyObjectPath: verifyObjectPath,
                intersect: intersect,
                difference: difference,
                apiLocation: apiLocation
            };
        }

        module.factory('$compose', [Service]);

    })();


})(angular);