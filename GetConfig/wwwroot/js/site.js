// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Project screen
$(".project-card").click(function () {
    window.location.replace("/Projects/ConfigValues/" + $(this).attr("data-id"));
});

// Config values
$("#save-changes-btn").click(function () {
    $("#modal-close-btn").hide();
    $("#value-modal-footer").hide();
    $("#saving-bar").show();

    var action = $(this).attr("data-action");
    var projectId = $(this).attr("data-project-id");
    var name = $("#config-value-name").val();
    var description = $("#config-value-description").val();
    var value = $("#config-value").val();
    if (action == "create") {
        $.post("/Projects/CreateValue",
            {
                name: name,
                description: description,
                value: value,
                project: projectId
            },
            function () {

                location.reload(true);
            }
        );
    }
    else {
        var id = $(this).attr("data-id");
        $.post("/Projects/EditValue",
            {
                name: name,
                description: description,
                value: value,
                id: id
            },
            function () {

                location.reload(true);
            }
        );
    }

});

$("#confirm-delete-btn").click(function () {
    var id = $(this).attr("data-id");
    $.post("/Projects/DeleteValue",
        {
            id: id
        },
        function () {

            location.reload(true);
        }
    );
});

function setValueModal(name, value) {
    $("#valueViewModalTitle").html(name);
    $("#valueViewModalValue").html(value);
}

function prepareEditModal(name, value, description, id) {
    $("#config-value-name").val(name);
    $("#config-value-description").val(description);
    $("#config-value").val(value);
    $("#save-changes-btn").attr("data-id", id);
    $("#save-changes-btn").attr("data-action", "edit");
    $("#valueEditModalTitle").val("Edit value");
}

function prepareAddModal() {
    $("#config-value-name").val("");
    $("#config-value-description").val("");
    $("#config-value").val("");
    $("#save-changes-btn").attr("data-action", "create");
    $("#valueEditModalTitle").html("Add value");
}

function prepareDeleteModal(name, id) {
    $("#deleteValueName").html(name);
    $("#confirm-delete-btn").attr("data-id", id);
}

$('input[type=radio][name=projectColor]').change(function () {
    alert(this.value);
});