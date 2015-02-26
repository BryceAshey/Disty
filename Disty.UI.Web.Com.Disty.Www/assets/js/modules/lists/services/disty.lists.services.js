/*global angular*/
(function ($ng) {
    'use strict';

    var module = $ng.module('disty.lists.service', [

    ]);

    //lists.service 
    (function ($ng, $module) {

        function Service($q) {
            var $this = this;
            //$this.$user = $memberService.getUserClaims();

            return {

                create: function (name) {
                    var deferred = $q.defer();

                    var promise = $distyConfig.requestFactory({
                        methodParams: {
                            name: name
                        },
                        distyResource: {
                            methodToCall: 'create',
                            resource: '$distributionList'
                        }
                    })();

                    promise.then(function (response) {
                        deferred.resolve({ data: response.data});
                    }, function (err) {
                        deferred.reject(err);
                    });

                    return deferred.promise;
                },


            };
        }

        $module.factory('$distributionListService', ['$q', Service]);

    })(ng, module);

})(angular);