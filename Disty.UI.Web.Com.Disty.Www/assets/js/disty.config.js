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
                    templateUrl: window.distyConfig.baseUrl + '/assets/html/home/index.html',
                    controller: 'home.controller'                    
                }).state('home.list', {
                    parent: 'home',
                    url: 'list/:listId',
                    templateUrl: window.distyConfig.baseUrl + '/assets/html/partials/lists/listGrid.html'
                });
        }
    };

    function load() {

        $script.get('//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js', function () {
            $script.get('//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js', function () {
                $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular.js', function () {
                    $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular-resource.js', function () {
                        $script([
                            'lib/ng-dialog/ngDialog.min',
                            'lib/ui-router/angular-ui-router.min',
                            'lib/underscore/underscore-1.7.0',
                            'lib/docs.min',
                            'ie10-viewport-bug-workaround',
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
                'modules/email/controllers/disty.email.controller',
                'modules/email/directives/disty.email.directives',
                'modules/email/services/disty.email.services',
                'modules/email/email.modules',
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