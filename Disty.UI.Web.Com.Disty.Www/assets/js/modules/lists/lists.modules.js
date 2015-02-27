(function () {
    'use strict';

    var _modules = [
        'disty.lists.controller',
        'disty.lists.directives',
        'disty.lists.service'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();