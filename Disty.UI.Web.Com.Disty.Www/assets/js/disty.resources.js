(function(ng) {
    'use strict';

    var baseUrl = distyConfig.baseUrl + '/api';

    function distributionListResource($resource) {
        var resource = $resource(baseUrl + '/distributionList/:listId',
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
        .factory('$distributionListResource', distributionListResource);


    //$distyResourceService
    (function ($ng, $module) {

        $module.factory('$distyResourceService',
            [
                '$distributionListResource',

                function (
                    $distributionListResource
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
                            moduleMemoizer('$distributionList', $distributionListResource);

                            return moduleMemoizer(resource);
                        }
                    };

                }]);

    })(ng, module);
    
})(angular)

