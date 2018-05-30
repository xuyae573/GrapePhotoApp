




// Write your JavaScript code.

require(['jquery'], function () {
    function LikeFunction() {
        $(".like-button").unbind("click");
        var appRootUrl = window.appRootUrl;
        console.log(appRootUrl);
        var baseUrl = appRootUrl + '/Common/';
        $(".like-button").click(function () {
            var self = $(this);
            var postId = $(this).data("postid");
            var likeAlready = $(this).hasClass("liked");
            var url = "";
            if (likeAlready) {
                url = baseUrl + "/DislikePost/"
            } else {
                url = baseUrl + "/LikePost/"
            }
            $.ajax({
                url: url,
                data: { 'postId': postId },
                type: "post",
                success: function (result) {
                    if (result.success) {
                        if (likeAlready) {
                            $(self).removeClass("liked");
                            $(self).find(".fa-heart").removeClass("fa-heart").removeClass("text-danger").addClass("fa-heart-o");
                        } else {
                            $(self).addClass("liked");
                            $(self).find(".fa-heart-o").removeClass("fa-heart-o").addClass("text-danger").addClass("fa-heart");
                        }
                        $(self).find("#likecount").html(result.count);
                        LikeFunction();
                    }
                }
            });
        });
    }
   $(document).ready(function () {
       $("#signoutMenu").click(function(e){
            e.preventDefault();
            $("#logoutForm").submit();
       });

       $("#searchuserico").click(function () {
           $("#searchuser").submit();
       });

       $(".header-search").keyup(function (e) {
           if (e.keyCode === 13) {
               $("#searchuser").submit();
           }
       });
       LikeFunction();
    });
});
 



