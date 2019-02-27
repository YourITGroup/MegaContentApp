function formsContentAppController($scope, editorState, contentResource,
    formResource, recordResource, userService,
    securityResource) {

    var vm = this;

    // Code borrowed from the Forms dashboard controller...

    //Default for canManageForms is false
    //Need a record in security to ensure user has access to edit/create forms
    vm.userCanManageForms = false;

    //Get Current User - To Check if the user Type is Admin
    userService.getCurrentUser().then(function (response) {
        vm.currentUser = response;
        vm.isAdminUser = response.userGroups.includes("admin");

        securityResource.getByUserId(vm.currentUser.id).then(function (response) {
            vm.userCanManageForms = response.data.userSecurity.manageForms;
        });
    });

    $scope.model.badge = {};

    vm.forms = [];

    // Find all properties that contain forms.
    contentResource.getById(editorState.current.id).then(function (node) {
        _.each(node.variants[0].tabs, function (tab){
            _.each(tab.properties, function (property) {

                if (property.editor === "UmbracoForms.FormPicker") {

                    // Get the form and it'sentries
                    formResource.getByGuid(property.value).then(function (response) {
                        var form = response.data;
                        vm.forms.push(form);

                        var filter = {
                            startIndex: 1, //Page Number
                            length: 20, //No per page
                            form: form.id,
                            sortBy: "created",
                            sortOrder: "descending",
                            states: ["Approved", "Submitted"],
                            localTimeOffset: new Date().getTimezoneOffset()
                        };

                        recordResource.getRecords(filter).then(function (response) {
                            form.entries = response.data;

                            console.log(vm.forms);
                        });
                    });
                }
            });
        });
        console.log(vm.forms);
    });
}
angular.module("umbraco").controller("ContentApps.Mega.FormsContentApp", formsContentAppController);