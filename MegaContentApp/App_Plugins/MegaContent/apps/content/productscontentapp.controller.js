function productsContentAppController($scope, editorState, entityResource, editorService, umbRequestHelper, $log, $http) {

    var vm = this;
    vm.CurrentNodeId = editorState.current.id;
    vm.CurrentNodeAlias = editorState.current.contentTypeAlias;
    vm.Products = [];

    $scope.model.badge = {};

    $scope.model.anchors = [
        {
            id: "1",
            alias: "Dummy",
            label: "Click Me!",
            active: false
        },
        {
            id: "2",
            alias: "Products",
            label: "Products",
            active: false
        }

    ];

    $scope.openDocument = function (document) {

        const editor = {
            id: document.id,
            submit: function (model) {
                const args = { node: scope.node };
                eventsService.emit('editors.content.reload', args);
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        };
        editorService.contentEditor(editor);
    };

    function init() {
        entityResource.getChildren(editorState.current.id, 'Document')
            .then(function (data) {
                vm.Products = data;
                console.log(data[0]);

                $scope.model.badge = {
                    count: data.length,
                    type: data.length === 0 ? "alert" : "success"
                };
            });


    }

    init();
}
angular.module("umbraco").controller("ContentApps.Mega.ProductsContentApp", productsContentAppController);