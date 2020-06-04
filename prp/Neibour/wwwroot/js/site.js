// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// When a user clicks on a group, Load messages for that particular group.
$(document).ready(function () {
    $('.Dialog').on('click', function () {
        var id = (this).attr('data-id');
        var allCommantArea = $('div').addClass('allComments_' + id);
        $ajax
            (
                {
                    type: 'GET',
                    url: '@Url.Action("GetComments","Chat")',
                    data: { Id_dialog: id },
                    success: function (response) {
                        if ($('div').hasClass('allComments_' + id + '')) {
                            $('div[class=allComments_' + id + ']').remove();
                        }
                        allCommantArea.html(response);
                        allCommantArea.html.prependTo('#commentsBlock_' + id);
                    },
                    error: function (response) {
                        alert('Sorry');
                    }
                }
            )
    })
});

$(document).ready(function () {

    $("#button1").click(function () {
        $("#div1").load("/Chat/GetComments");
    });

});