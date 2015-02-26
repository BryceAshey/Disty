(function() {
    'use strict';
     
    var _modules = [
        'ngResource',
        'ui.router',
        'disty.common.api.services'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();