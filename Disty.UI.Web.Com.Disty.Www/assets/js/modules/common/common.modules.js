(function() {
    'use strict';
     
    var _modules = [
        'ui.router'
    ];

    modules = _.union(distyConfig.modules, _modules);
    distyConfig.modules = _.union(distyConfig.modules, _modules);

})();