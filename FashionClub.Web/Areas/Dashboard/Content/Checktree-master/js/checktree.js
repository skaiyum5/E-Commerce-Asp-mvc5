(function($){
    $.fn.checktree = function(){
        $(':checkbox').on('click', function (event){
            event.stopPropagation();
            var clk_checkbox = $(this),
            chk_state = clk_checkbox.is(':checked'),
            parent_li = clk_checkbox.closest('li'),
            parent_uls = parent_li.parents('ul');
            parent_li.find(':checkbox').prop('checked', chk_state);
            parent_uls.each(function(){
                parent_ul = $(this);
                parent_state = (parent_ul.find(':checkbox').length == parent_ul.find(':checked').length); 
                parent_ul.siblings(':checkbox').prop('checked', parent_state);
            });
         });
    };
}(jQuery));


$(document).ready(function () {
    var chk_state = $("input[type=checkbox]:checked"),
        parent_li = chk_state.closest('li'),
        parent_uls = parent_li.parents('ul');
        parent_li.find(':checkbox').prop('checked', chk_state);
        parent_uls.each(function () {
        parent_ul = $(this);
        parent_state = (parent_ul.find(':checkbox').length == parent_ul.find(':checked').length);
        parent_ul.siblings(':checkbox').prop('checked', parent_state);
        parent_ul.attr('class', 'show');
    });
});


//$(document).ready(function () {
//    var chk_state = $("input[type=checkbox]:checked"),
//        parent_li = chk_state.closest('li'),
//        parent_uls = parent_li.parents('ul');
//        parent_li.find(':checkbox').prop('checked', chk_state);
//        parent_uls.each(function () {
//        parent_ul = $(this);
//        parent_state = (parent_ul.find(':checkbox').length == parent_ul.find(':checked').length);
//        parent_ul.siblings(':checkbox').prop('checked', parent_state);
//    });
//});
