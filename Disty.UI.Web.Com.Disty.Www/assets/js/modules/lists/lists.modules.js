﻿(function () {
    'use strict';

    var _modules = [
        'disty.lists.controller',
        'disty.lists.directives'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();