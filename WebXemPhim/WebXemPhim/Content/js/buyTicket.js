var user = {
    init: function () {
        user.DanhSachPhimDangChieu();
        user.DanhSachLoaiVe();
        user.loadMovie();
        var d = new Date($.now());
        user.loadShowTime(d.toLocaleDateString());
        user.registerEvent();
    },
    registerEvent: function(){
        $('#dsPhim').off('change').on('change', function () {
            var phimID = $(this).val();
            var loaiVeID = $('#dsLoaiVe').val();
            var lichChieuID = $('dsNgayChieu').val();
            if(phimID != '' && loaiVeID != '')
            {
                user.DanhSachNgayChieu(phimID, loaiVeID);
                if (lichChieuID != '' && lichChieuID != undefined)
                {
                    user.DanhSachGioChieu(lichChieuID);
                }
                else
                {
                    $('#dsGioChieu').html('');
                }
            }
            else {
                $('#dsNgayChieu').html('');
            }
               
        });
        $('#dsLoaiVe').off('change').on('change', function () {
            var loaiVeID = $(this).val();
            var phimID = $('#dsPhim').val();
            var lichChieuID = $('dsNgayChieu').val();
            if (loaiVeID != '') {
                user.CapNhatGiaVe(loaiVeID);                
            }
            if (phimID != '' && loaiVeID != '') {
                user.DanhSachNgayChieu(phimID, loaiVeID);
                if (lichChieuID != '' && lichChieuID != undefined) {
                    user.DanhSachGioChieu(lichChieuID);
                }
                else {
                    $('#dsGioChieu').html('');
                    $('#phong-chieu').text('');
                    $('#dsGhe').html('');
                }
            }
            else {
                $('#dsNgayChieu').html('');
                $('#phong-chieu').text('');
                $('#gia-ve').text('');
                $('#ThanhTien').val('');
            }

        });
        $('#dsNgayChieu').off('change').on('change', function () {
            var lichChieuID = $(this).val();
            
            if (lichChieuID != '') {
                user.DanhSachGioChieu(lichChieuID);
            }
            else {
                $('#dsGioChieu').html('');
            }

        });
        $('#dsGioChieu').off('change').on('change', function () {
            var lichChieuID = $(this).val();
            if (lichChieuID != '' && lichChieuID != undefined) {
                user.CapNhatPhongChieu(lichChieuID);
                user.DanhSachGhe(lichChieuID);
                $('#LichChieuID').val(lichChieuID);
            }
            else {
                $('#phong-chieu').text('');
                $('#dsGhe').html('');
                $('#LichChieuID').val('');
            }
        });
        $('#listPhim').off('change').on('change', function () {
            var id = $(this).val();
            var ngay = $('#listLichChieu').val();
            if (id != '')
                user.loadPhim(id, "2/2/2016", false);
            else
                user.loadPhim(-1, "2/2/2016", false);
            var d = new Date($.now());
            user.loadShowTime(d.toLocaleDateString());
        });
        $('#listLichChieu').off('change').on('change', function () {
            var ngay = $(this).val();
            var id = $('#listPhim').val();
            if (ngay != '' && id != '') {
                user.loadPhim(id, ngay, true);
            }
            else {
                if (id != '')
                    user.loadPhim(id, "2/2/2016", false);
                else
                    user.loadPhim(-1, "2/2/2016", false);
            }
        });
    },
    DanhSachPhimDangChieu: function () {
        $.ajax({
            url: '/Home/DanhSachPhimDangChieu',
            type: 'POST',
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN PHIM</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.PhimID + '">' + item.TenPhim + '</option>'
                    });
                    $('#dsPhim').html(html);
                }
            }
        })
    },
    DanhSachLoaiVe: function () {
        $.ajax({
            url: '/Home/DanhSachLoaiVe',
            type: 'POST',
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN LOẠI VÉ</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.LoaiVeID + '">' + item.TenLoaiVe + '</option>'
                    });
                    $('#dsLoaiVe').html(html);
                }
            }
        })
    },
    DanhSachNgayChieu: function (phimID, loaiVeID) {
        $.ajax({
            url: '/Home/DanhSachNgayChieu',
            type: 'POST',
            data: { PhimID: phimID, LoaiVeID: loaiVeID },
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN NGÀY CHIẾU</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.LichChieuID + '">' + item.NgayChieuToString + '</option>'
                    });
                    $('#dsNgayChieu').html(html);
                }
            }
        })
    },
    DanhSachGioChieu: function (lichChieuID) {
        $.ajax({
            url: '/Home/DanhSachGioChieu',
            type: 'POST',
            data: { LichChieuID: lichChieuID },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN GIỜ CHIẾU</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.LichChieuID + '">' + item.GioChieuToString + '</option>'
                    });
                    $('#dsGioChieu').html(html);
                }
            }
        })
    },
    CapNhatPhongChieu: function (lichChieuID) {
        $.ajax({
            url: '/Home/CapNhatPhongChieu',
            type: 'POST',
            data: { LichChieuID: lichChieuID },
            dataType: "json",
            success: function (response) {
                var html = '<span id="remove">' + response + '</span>';
                $('#phong-chieu').html(html);
            }
        })
    },
    DanhSachGhe: function (lichChieuID) {
        $.ajax({
            url: '/Home/DanhSachGhe',
            type: 'POST',
            data: { LichChieuID: lichChieuID },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">CHỌN SỐ GHẾ</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.GheID + '">' + item.SoGhe + '</option>'
                    });
                    $('#dsGhe').html(html);
                }
            }
        })
    },
    CapNhatGiaVe: function (loaiVeID) {
        $.ajax({
            url: '/Home/CapNhatGiaVe',
            type: 'POST',
            data: { LoaiVeID: loaiVeID },
            dataType: "json",
            success: function (response) {
                var html = '<span id="remove-giave">' + response + ' VNĐ' + '</span>';
                $('#gia-ve').html(html);
                $('#ThanhTien').val(response + ' VNĐ');
            }
        })
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