function megaContentAppController($scope, editorState, userService, umbRequestHelper, $log, $http) {

    var vm = this;
    vm.CurrentNodeId = editorState.current.id;
    vm.CurrentNodeAlias = editorState.current.contentTypeAlias;
    vm.Counter = 0;

    vm.UpdateCounter = function () {
        vm.Counter++;

        // we can add a badge to the app by defining $scope.model.badge - options are:
        // - count: Optional. Adds a number to the badge. 
        // - type: Optional. Sets the "severity" color of the badge - use "warning" or "alert".
        $scope.model.badge = {
            count: vm.Counter,
            type: vm.Counter > 9 ? "alert" : vm.Counter > 4 ? "warning" : null
        };
    };

    function init() {
        userService.getCurrentUser().then(function (user) {
            vm.UserName = user.name;
        });
    }

    init();
}
angular.module("umbraco").controller("ContentApps.MegaContentApp", megaContentAppController);