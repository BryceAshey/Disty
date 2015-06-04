(function(ng) {
    'use strict';

    var baseUrl = distyConfig.baseUrl + '/api';

    function distributionListResource($resource) {
        var resource = $resource(baseUrl + '/distributionList/:listId',
            { listId: '@listId' },
            {
                create: { method: 'POST' },
                update: { method: 'PUT' },
                get: { method: 'GET'},
                $query: { method: 'GET', isArray: true, transformResponse: function (data, headers) { return JSON.parse(data).list; } },
                del: { method: 'DELETE' }
            });

        return resource;
    }


    var module = ng.module('disty.resources', ['ngResource'])
        .factory('$distributionListResource', distributionListResource);

})(angular)

