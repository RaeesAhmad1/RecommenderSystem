function body_sizer() {
    var a = $(window).height(),
        b = $("#page-header").height(),
        c = a - b;
    $("#page-sidebar").css("height", a),
        $(".scroll-sidebar").css("height", c),
        $("#page-content").css("min-height", c)
}

function pageTransitions() {
    var a = [".pt-page-moveFromLeft", "pt-page-moveFromRight", "pt-page-moveFromTop", "pt-page-moveFromBottom", "pt-page-fade", "pt-page-moveFromLeftFade", "pt-page-moveFromRightFade", "pt-page-moveFromTopFade", "pt-page-moveFromBottomFade", "pt-page-scaleUp", "pt-page-scaleUpCenter", "pt-page-flipInLeft", "pt-page-flipInRight", "pt-page-flipInBottom", "pt-page-flipInTop", "pt-page-rotatePullRight", "pt-page-rotatePullLeft", "pt-page-rotatePullTop", "pt-page-rotatePullBottom", "pt-page-rotateUnfoldLeft", "pt-page-rotateUnfoldRight", "pt-page-rotateUnfoldTop", "pt-page-rotateUnfoldBottom"];
    for (var b in a) {
        var c = a[b];
        if ($(".add-transition").hasClass(c)) return $(".add-transition").addClass(c + "-init page-transition"), void setTimeout(function() {
            $(".add-transition").removeClass(c + " " + c + "-init page-transition")
        }, 1200)
    }
}
$(document).ready(function() {
    body_sizer(), $(function() {
        $(".scroll-sidebar").slimscroll({
            height: "100%",
            color: "rgba(155, 164, 169, 0.68)",
            size: "6px"
        })
    }), $(function() {
        $("#sidebar-menu").hover(function() {
            $("#page-sidebar").toggleClass("sidebar-hover")
        })
    })
}), $(window).on("resize", function() {
    body_sizer()
}), $(document).ready(function() {
    pageTransitions(), $(".dropdown").on("show.bs.dropdown", function(a) {
        $(this).find(".dropdown-menu").first().stop(!0, !0).slideDown()
    }), $(".dropdown").on("hide.bs.dropdown", function(a) {
        $(this).find(".dropdown-menu").first().stop(!0, !0).slideUp()
    }), $(function() {
        $("#sidebar-menu").superclick({
            animation: {
                height: "show"
            },
            animationOut: {
                height: "hide"
            }
        });
        var a = window.location.pathname.split("/");
        a = a[a.length - 1], void 0 !== a && ($("#sidebar-menu").find("a[href$='" + a + "']").addClass("active"), $("#sidebar-menu").find("a[href$='" + a + "']").parents().eq(3).superclick("show"))
    }), $(function() {
        $("#close-sidebar").click(function() {
            $("body").toggleClass("closed-sidebar"), $(".glyph-icon", this).toggleClass("icon-outdent").toggleClass("icon-indent")
        })
    })
});