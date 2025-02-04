// Lấy các phần tử HTML đại diện cho các trường nhập liệu và nút đăng nhập
let username = document.getElementById("username");
let email = document.getElementById("email");
let password = document.getElementById("password");
let btnLogin = document.querySelector(".btn-login");

// Thêm sự kiện cho nút đăng nhập
btnLogin.addEventListener("click", (e) => {
  // Ngăn chặn hành vi mặc định của nút
  e.preventDefault();
  // Tạo một đối tượng người dùng với thông tin nhập liệu
  let user = {
    username: username.value,
    email: email.value,
    password: password.value,
  };
  console.log(user);
  // Chuyển đổi đối tượng người dùng thành chuỗi JSON
  let json = JSON.stringify(user);
  // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
  if (!username.value || !email.value || !password.value) {
    alert("Vui lòng nhập đầy đủ thông tin");
  }
  // Kiểm tra xem thông tin đăng nhập có khớp với thông tin trong bộ nhớ cục bộ không
  if (localStorage.getItem(username.value) == json) {
    alert("Đăng nhập thành công");
    // Lưu tên đăng nhập vào sessionStorage
    sessionStorage.setItem("loggedInUser", username.value);
    // Chuyển hướng đến trang chủ
    window.location.href = "../index.html";
  } else {
    alert("Đăng nhập thất bại");
  }
});
