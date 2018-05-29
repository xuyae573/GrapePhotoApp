




// Write your JavaScript code.

require(['jquery'], function(){
   $(document).ready(function () {
       $("#signoutMenu").click(function(e){
            e.preventDefault();
            $("#logoutForm").submit();
        });
    });
});
 


require(['jquery', '../lib/photoswipe/dist/photoswipe.js', '../lib/photoswipe/dist/photoswipe-ui-default.js'],
    function ($, PhotoSwipe, PhotoSwipeUI_Default) {
        $(document).ready(function () {
            $("img.grapephoto").click(function (e) {
                var index = $(this).index();
                var pswpElement = document.querySelectorAll('.pswp')[0];
                var items = [];
                $("img.grapephoto").each(function (index, value) {
                    var imageLink = $(value).closest("a").attr("src");
                    var w = $(this).parent("a").data("width");
                    var h = $(this).parent("a").data("height");
                    items.push({
                        src: imageLink,
                        w: w,
                        h: h
                    });
                });
                var options = {
                    index: index,
                    history: false,
                    focus: false,
                    showAnimationDuration: 0,
                    hideAnimationDuration: 0
                };

                var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
                gallery.init();
            });

        });
    });
