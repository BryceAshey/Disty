(function (window, $script) {
    'use strict';

    function distyConfig(isDebug) {
        var self = this instanceof distyConfig
            ? this
            : Object.create(distyConfig.prototype);

        self.isDebug = isDebug;
        self.modules = modules;
        self.init();
        window.distyConfig = self;
    };

    distyConfig.prototype = {
        init: function () {

            load();
            return this;
        },
        setRoutes: function ($routeProvider) {

            var routes = {
                section: {
                    addRoutes: function (routeProvider) {
                        routeProvider
                          .when('/:sectionId/classhome', { action: 'section.segment.classhome' })
                          .when('/:sectionId/welcome', { action: 'section.segment.welcome' })
                          .when('/:sectionId/segment/:segmentId', { action: 'section.segment.home' })
                          .when('/:sectionId/generalForum', { action: 'section.segment.generalForum' })
                          .when('/:sectionId/segment/:segmentId/:activityType/:activityId', { action: 'section.segment.activity' })
                          .when('/:sectionId/student/gradebook', { action: 'section.segment.studentgradebook' })
                          .when('/:sectionId/gradebook', { action: 'section.segment.gradebook' })
                          .when('/:sectionId/gradebook/segment/:segmentId/discussion/:activityId', { action: 'section.segment.discussiongradebook' })
                          .when('/:sectionId/gradebook/segment/:segmentId/discussion/:activityId/user/:userId', { action: 'section.segment.discussiongradebook' })
                          .when('/:sectionId/gradebook/segment/:segmentId/:activityType/:activityId', { action: 'section.segment.activitygradebook' })
                          .when('/:sectionId/gradebook/segment/:segmentId/:activityType/:activityId/user/:userId', { action: 'section.segment.activitygradebook' })
                          .otherwise({ action: '/' });
                    }
                }
            };

        }
    };

    function load() {

        $script.get('//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js', function () {
            $script.get('//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js', function () {
                $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular.js', function () {
                    $script.get('//ajax.googleapis.com/ajax/libs/angularjs/1.3.9/angular-route.js', function() {
                        $script([
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
                'modules/common/common.modules'
            ], 'distyCore');
        });

        $script.ready('distyCore', function () {
            $script([
                'disty'
            ], 'disty');
        });

        $script.ready('disty', function () {
            distyConfig.modules = modules;

            angular.element(document).ready(function () {
                angular.bootstrap(document, _.union(['disty'], modules));
            });
        });
    }

    window.distyConfig = distyConfig;

})(window, $script);