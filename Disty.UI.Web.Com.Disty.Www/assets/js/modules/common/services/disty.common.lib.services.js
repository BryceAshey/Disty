(function (ng) {
    'use strict';

    var module = ng.module('disty.common.lib.services', [
    ]);

    //---------------Service Libraries------------------------------//
    
    //$lodash
    (function ($ng, $module) {

        $module.value('$lodash', _);

    })(ng, module);

})(angular);