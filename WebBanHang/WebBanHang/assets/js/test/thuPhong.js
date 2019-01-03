// change accordion caret when collapsing
(function ($) {
    accordion.find('.accordion-toggle').click(function () {
        if ($(this).hasClass('collapsed')) {
            accordion.find('.accordion-toggle').not(this).addClass('collapsed');
        }
    })
})(jQuery);
!function ($) {
    $(document).off('click').on('click.collapse.data-api', '[data-toggle=collapse]', function (e) {
        var $this = $(this), href
          , target = $this.attr('data-target')
            || e.preventDefault()
            || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '') //strip for ie7
          , option = $(target).data('collapse') ? 'toggle' : $this.data()
        $this[$(target).hasClass('in') ? 'addClass' : 'removeClass']('collapsed')
        $(target).collapse(option)
    })
}(window.jQuery);