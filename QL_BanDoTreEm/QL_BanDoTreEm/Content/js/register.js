// Lấy các phần tử HTML đại diện cho các trường nhập liệu và nút đăng ký
let username = document.getElementById("username");
let email = document.getElementById("email");
let password = document.getElementById("password");
let btnSignup = document.querySelector(".btn-signup");
let btnLogin = document.querySelector(".btn-login");

// Thêm sự kiện cho nút đăng ký
btnSignup.addEventListener("click", (e) => {
  // Ngăn chặn hành vi mặc định của nút
  e.preventDefault();
  // Tạo một đối tượng người dùng với thông tin nhập liệu
  let user = {
    username: username.value,
    email: email.value,
    password: password.value,
  };
  // Chuyển đổi đối tượng người dùng thành chuỗi JSON
  let json = JSON.stringify(user);
  // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
  if (!username.value || !email.value || !password.value) {
    alert("Vui lòng nhập đầy đủ thông tin");
  } else {
    // Lưu thông tin người dùng vào bộ nhớ cục bộ
    localStorage.setItem(username.value, json);
    alert("Đăng ký thành công");
  }
});