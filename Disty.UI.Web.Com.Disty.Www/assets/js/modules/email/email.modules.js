(function () {
    'use strict';

    var _modules = [
        'disty.email.controller',
        'disty.email.service'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();