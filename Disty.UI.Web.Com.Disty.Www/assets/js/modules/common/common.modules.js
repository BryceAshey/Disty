(function() {
    'use strict';
     
    var _modules = [
        'ngResource',
        'ui.router',
        'disty.common.api.services',
        'disty.common.lib.services'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();