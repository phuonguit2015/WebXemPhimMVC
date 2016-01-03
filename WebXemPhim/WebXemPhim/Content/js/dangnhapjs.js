$(document).ready(function () {
    $('#dangnhap').click(function () {
        var value = mail.value;
        var value1 = pass.value;
        if (value == "" || value1 == "")
            alert("Tài khoản hoặc mật khẩu không được để trống!");
        else {
            $("#dangnhap").attr({
                'href': '/NguoiDungs/KiemTraDangNhap?taiKhoan=' + value + '&matKhau=' + value1 + '',
            })
        }
    });
});