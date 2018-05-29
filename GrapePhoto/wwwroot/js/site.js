




// Write your JavaScript code.

require(['jquery'], function(){
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
    });
});
 



