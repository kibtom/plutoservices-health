// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {
  $('.bounce').bind('mouseenter', function()
     {doBounce($(this), 1, '10px', 300);

        function doBounce(element, times, distance, speed) {
            for(i = 0; i < times; i++) {
                element.animate({marginTop: '-='+distance},speed)
                    .animate({marginTop: '+='+distance},speed);
            }        
        }

    });

});


//$( "#toggle" ).toggle( "bounce", { times: 3 }, "slow" );