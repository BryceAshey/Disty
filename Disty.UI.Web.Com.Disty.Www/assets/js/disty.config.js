(function (window, $script) {
    'use strict';

    function distyConfig(isDebug, baseUrl) {
        var self = this instanceof distyConfig
            ? this
            : Object.create(distyConfig.prototype);

        self.isDebug = isDebug;
        self.baseUrl = baseUrl;
        self.modules = modules;
        self.init();
        window.distyConfig = self;
    };

    distyConfig.prototype = {
        init: function () {

            load();
            return this;
        },
        setRoutes: function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/assets/html/home/index.html',
                    controller: 'home.controller'
                });

        }
    };

    function load() {

        $script.get('//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js', function () {
            $script.get('//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js', function () {
                $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular.js', function () {
                    $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular-resource.js', function () {
                        $script([
                            '/assets/js/lib/ng-dialog/ngDialog.min.js',
                            '/assets/js/lib/ui-router/angular-ui-router.min.js',
                            '/assets/js/lib/underscore/underscore-1.7.0.js',
                            '/assets/js/lib/docs.min.js',
                            '/assets/js/ie10-viewport-bug-workaround.js',
                        ], 'distyLibs');
                    });
                });
            });
        });

        $script.ready('distyLibs', function () {
            $script([
                'modules/common/services/disty.common.api.services',
                'modules/common/services/disty.common.lib.services',
                'modules/common/common.modules',
                'modules/home/controllers/disty.home.controller',
                'modules/home/home.modules',                
                'modules/lists/controllers/disty.lists.controller',
                'modules/lists/directives/disty.lists.directives',
                'modules/lists/services/disty.lists.services',
                'modules/lists/lists.modules',                
            ], 'distyCore');
        });

        $script.ready('distyCore', function () {
            $script([
                'disty.resources',
                'disty'
            ], 'disty');
        });

        $script.ready('disty', function () {
            distyConfig.modules = _.union(['disty'], modules);

            angular.element(document).ready(function () {
                angular.bootstrap(document, distyConfig.modules);
            });
        });
    }

    window.distyConfig = distyConfig;

})(window, $script);