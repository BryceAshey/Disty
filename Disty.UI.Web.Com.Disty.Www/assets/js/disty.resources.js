(function(ng) {
    'use strict';

    var baseUrl = distyConfig.baseUrl;

    function ListResource($resource) {
        var resource = $resource(baseUrl + "/distributionList/:listId",
            { listId: '@listId' },
            {
                create: { method: 'POST' },
                update: { method: 'PUT' },
                get: { method: 'GET', isArray: true },
                del: { method: 'DELETE' }
            });

        return resource;
    }


    var module = ng.module('disty.resources', ['ngResource'])
        .factory('$distributionListService', ListResource);

    //$distyResourceService
    (function ($ng, $module) {

        $module.factory('$distyResourceService',
            [
                '$distributionListService',

                function (
                    $distributionListService
                ) {

                    return {
                        factory: function (resource) {

                            var moduleMemoizer = function () {
                                var modules = {};

                                // Create a new module reference scaffold or load an existing module.
                                return function (name, setup) {
                                    if (modules[name]) {
                                        return modules[name];
                                    }
                                    return modules[name] = setup || {};
                                };
                            }();

                            //TODO Figure out a better way to do this that doesn't require updating for new resources
                            moduleMemoizer('$distributionListService', $distributionListService);

                            return moduleMemoizer(resource);
                        }
                    };

                }]);

    })(ng, module);
    
})(angular)

