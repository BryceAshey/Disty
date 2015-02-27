(function (ng) {
    'use strict';

    var module = ng.module('disty.common.api.services', [
        'disty.common.lib.services',
        'disty.resources',

    ]);

    //---------------Resource / API service support---------------//
    //$distyConfig
    (function ($ng, $module) {

        $module.factory('$distyConfig', [
            '$rootScope',
            '$http',
            '$q',
            '$window',
            '$distyResourceService',
            '$lodash',
            function ($rootScope, $http, $q, $window, $distyResourceService, $lodash) {

                $http.defaults.useXDomain = true;

                return {
                    requestFactory: function (options) {
                        options || (options = {});

                        //type, url, prms, headers, resource
                        var baseUrl = distyConfig.baseUrl;

                        var defaults = {
                            type: 'GET',
                            url: '',
                            prms: '',
                            timeout: 60000,
                            headers: []
                        };

                        var request = _.extend({}, defaults, options);

                        var requestPromise;

                        var callStrategy = {
                            pngresource: {
                                doIt: function () {

                                    var deferred = $q.defer();


                                    //do something and either resolve or reject the promise
                                    //then return the promise;
                                    var currentResource = (function () {

                                        return $distyResourceService.factory(request.distyResource.resource);

                                    }());

                                    if (typeof currentResource !== "undefined") {

                                        if (ng.isFunction(currentResource[request.distyResource.methodToCall])) {

                                            //what does this mean?
                                            var ajax = currentResource[request.distyResource.methodToCall];
                                            try {

                                                console.log(ajax);

                                                if (request.methodParams) {

                                                    ajax(request.methodParams, function (data, responseHeaderFunc) {
                                                        if (request.distyResource.methodToCall !== "query") {

                                                            data.$ResponseHeaderFunc = function (name) {
                                                                return responseHeaderFunc(name);
                                                            };

                                                            data.$IdFromLocationFunc = function () {
                                                                var location = '' + responseHeaderFunc('Location');
                                                                var lastSlashIndex = location.lastIndexOf('/');
                                                                var id = location.substring(lastSlashIndex + 1, location.length);
                                                                return id;
                                                            };
                                                        }

                                                        deferred.resolve(data);

                                                    }, function (err) {
                                                        deferred.reject({ err: err });
                                                    });

                                                } else {
                                                    ajax(function (data) {
                                                        //do something with this data
                                                        deferred.resolve({ data: data });
                                                    }, function (err) {

                                                        deferred.reject({ err: err });
                                                    });
                                                }
                                            } catch (e) {
                                                //potentially do something here then forward on the error
                                                throw Error(e);
                                            }
                                        } else {

                                            //instance method??
                                            var distyEntity = new currentResource();
                                            if (ng.isFunction(distyEntity[request.distyResource.methodToCall])) {

                                                throw Error('Not yet implmented');
                                            }

                                        }

                                    } else {

                                        throw Error('currentResource is not a function ~ ' + request.distyResource.name);
                                    }


                                    return deferred.promise;

                                }
                            },
                            goodOlAngular$http: {
                                doIt: function () {

                                    function encodeUriSegment(val) {
                                        return encodeUriQuery(val, true).
                                          replace(/%26/gi, '&').
                                          replace(/%3D/gi, '=').
                                          replace(/%2B/gi, '+');
                                    }

                                    function encodeUriQuery(val, pctEncodeSpaces) {
                                        return encodeURIComponent(val).
                                          replace(/%40/gi, '@').
                                          replace(/%3A/gi, ':').
                                          replace(/%24/g, '$').
                                          replace(/%2C/gi, ',').
                                          replace(/%20/g, (pctEncodeSpaces ? '%20' : '+'));
                                    }

                                    function extractParams(data, actionParams) {
                                        var ids = {};
                                        actionParams = extend({}, paramDefaults, actionParams);
                                        $lodash.forEach(actionParams, function (value, key) {
                                            ids[key] = value.charAt && value.charAt(0) == '@' ? getter(data, value.substr(1)) : value;
                                        });
                                        return ids;
                                    }

                                    function Route(template, defaults) {
                                        this.template = template = template + '#';
                                        this.defaults = defaults || {};
                                        var urlParams = this.urlParams = {};

                                        $lodash.forEach(template.split(/\W/), function (param) {
                                            if (param && (new RegExp("(^|[^\\\\]):" + param + "\\W").test(template))) {
                                                urlParams[param] = true;
                                            }
                                        });
                                        this.template = template.replace(/\\:/g, ':');
                                    }

                                    Route.prototype = {
                                        url: function (params) {


                                            var self = this,
                                                url = this.template,
                                                val,
                                                encodedVal;

                                            params = params || {};

                                            $lodash.forEach(this.urlParams, function (_, urlParam) {

                                                val = params.hasOwnProperty(urlParam) ? params[urlParam] : self.defaults[urlParam];
                                                if (angular.isDefined(val) && val !== null) {
                                                    encodedVal = encodeUriSegment(val);
                                                    url = url.replace(new RegExp(":" + urlParam + "(\\W)", "g"), encodedVal + "$1");
                                                } else {
                                                    url = url.replace(new RegExp("(\/?):" + urlParam + "(\\W)", "g"), function (match,
                                                        leadingSlashes, tail) {
                                                        if (tail.charAt(0) == '/') {
                                                            return tail;
                                                        } else {
                                                            return leadingSlashes + tail;
                                                        }
                                                    });
                                                }
                                            });
                                            url = url.replace(/\/?#$/, '');
                                            //var query = [];
                                            //$lodash.forEach(params, function (value, key) {
                                            //    if (!self.urlParams[key]) {
                                            //        query.push(encodeUriQuery(key) + '=' + encodeUriQuery(value));
                                            //    }
                                            //});
                                            //query.sort();
                                            //url = url.replace(/\/*$/, '');
                                            return url; // + (query.length ? '?' + query.join('&') : '');
                                        }

                                    };


                                    var deferred = $q.defer();
                                    if (ng.isFunction($http[request.type.toLowerCase()])) {

                                        var ajax = $http[request.type.toLowerCase()],
                                            _url;

                                        if (request.url.indexOf("/:") > 0) {
                                            _url = new Route(request.url).url(request.methodParams);
                                        } else {
                                            _url = request.url;
                                        }


                                        try {

                                            var promise;
                                            switch (request.type.toLowerCase()) {

                                                case 'post':
                                                case 'put':
                                                    promise = ajax(request.url, request.config.data, request.config);
                                                    break;
                                                default: //get, head, delete, jsonp
                                                    promise = ajax(_url, null, { 'method': 'GET', 'url': _url });
                                                    break;

                                            }

                                            promise.then(function (resp) {


                                                if ($lodash.isObject(resp)) {
                                                    resp.$ResponseHeaderFunc = function (name) {
                                                        return resp.headers(name);
                                                    };
                                                    resp.$IdFromLocationFunc = function () {
                                                        var location = '' + resp.headers('Location');
                                                        var lastSlashIndex = location.lastIndexOf('/');
                                                        var id = location.substring(lastSlashIndex + 1, location.length);
                                                        return id;
                                                    };
                                                }

                                                deferred.resolve(resp);
                                            });


                                            promise.error(function (data, status, headers, config) {
                                                deferred.reject(status);
                                            });

                                        } catch (e) {

                                            //potentially do something here then forward on the error
                                            throw Error(e);
                                        }

                                        return deferred.promise;

                                    } else {

                                        throw Error();
                                    }
                                }
                            },
                            other: {
                                doIt: function () {
                                    throw Error('Not Implemented!');
                                }
                            }
                        };

                        requestPromise = (request.distyResource)
                            ? callStrategy['pngresource']
                            : callStrategy['goodOlAngular$http'];
                        
                        return function () {
                            var deferred = $q.defer(),
                                el = $ng.element('#ajaxMsg');

                            try {
                                var promise = requestPromise.doIt();
                                promise.then(function (data) {
                                    el.empty();
                                    deferred.resolve(data);
                                }, function (err) {
                                    //el.html(err);
                                    deferred.reject(err);
                                });

                            } catch (e) {
                                el.html(e);
                                //$postal.publish({
                                //    channel: 'data.ajax',
                                //    topic: 'data.ajax.error',
                                //    data: { url: request.url, error: e }
                                //});
                                deferred.reject(e);

                            } finally {

                                //$postal.publish({
                                //    channel: 'data.ajax',
                                //    topic: 'data.ajax.call',
                                //    data: { url: request.url }
                                //});
                            };

                            return deferred.promise;

                        };
                    }
                };

            }]);

    })(ng, module);


})(angular);