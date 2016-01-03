var user = {
    init: function () {
        user.loadMovie();
        var d = new Date($.now());
        user.loadShowTime(d.toLocaleDateString());
        user.registerEvent();
    },    
    registerEvent: function () {
        $('#listPhim').off('change').on('change', function () {
            var id = $(this).val();
            var ngay = $('#listLichChieu').val();
            if (id != '')
                user.loadPhim(id,"2/2/2016",false);
            else
                user.loadPhim(-1, "2/2/2016", false);
            var d = new Date($.now());
            user.loadShowTime(d.toLocaleDateString());
        });
        $('#listLichChieu').off('change').on('change', function () {
            var ngay = $(this).val();
            var id = $('#listPhim').val();
            if (ngay != '' && id != '')
            {              
                user.loadPhim(id, ngay, true);
            }
            else
            {
                if(id!='')
                    user.loadPhim(id, "2/2/2016", false);
                else
                    user.loadPhim(-1, "2/2/2016", false);
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
                    $('#listPhim').html(html);
                }
            }
        })
    },
    loadShowTime: function (d) {
        $.ajax({
            url: '/TrangChu/GetNextDay',
            type: "POST",
            data: { dt: d },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN NGÀY CHIẾU</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item + '">' + item + '</option>'
                    });
                    $('#listLichChieu').html(html);
                }
            }
        })
    },
    loadPhim: function (PhimID, ngayChieu, CoNgayChieu) {
        $.ajax({
            cache: false,
            url: '/Home/DanhSachLichChieuPartial',
            contentType: 'application/html; charset=utf-8',
            type: "GET",
            data: { PhimID: PhimID, ngayChieu: ngayChieu, CoNgayChieu: CoNgayChieu },
            dataType: "html",
            success: function (response) {
                $(".partialLichChieu").html('');
                $(".partialLichChieu").html(response);
            }
        })
    }
    
}

user.init();
