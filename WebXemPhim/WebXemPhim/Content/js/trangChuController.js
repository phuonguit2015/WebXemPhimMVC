var user = {
    init: function(){
        user.loadMovie();
        user.registerEvent();
    },
    registerEvent: function(){
        $('#dllPhim').off('change').on('change', function () {
            var id = $(this).val();
            if(id != '')
            {
                user.loadShowTime(parseInt(id));
            }
            else
            {
                $('#dllLichChieu').html('');
            }
        });
    },
    loadMovie: function () {
        $.ajax({
            url: '/TrangChu/FillMovies',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN PHIM</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.PhimID + '">' + item.TenPhim + '</option>'
                    });
                    $('#dllPhim').html(html);
                }
            }
        })
    },
    loadShowTime: function (id) {
        $.ajax({
            url: '/TrangChu/FillShowTime',
            type: "POST",
            data: {Phim: id},
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN GIỜ CHIẾU</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.LichChieuID + '">' + item.GioChieu + '</option>'
                    });
                    $('#dllLichChieu').html(html);
                }
            }
        })
    }
}

user.init();