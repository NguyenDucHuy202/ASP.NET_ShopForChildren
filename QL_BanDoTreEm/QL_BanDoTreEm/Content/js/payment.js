function validateCheckoutForm() {
  // Lấy thông tin khách hàng từ các trường input
  const fullName = document.getElementById("full-name").value.trim();
  const address = document.getElementById("address").value.trim();
  const email = document.getElementById("email").value.trim();
  const phone = document.getElementById("phone").value.trim();

  // Kiểm tra thông tin khách hàng
  if (fullName === "" || address === "" || email === "" || phone === "") {
    alert("Vui lòng điền đầy đủ thông tin khách hàng.");
    return false;
  }

  // Kiểm tra định dạng email
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailRegex.test(email)) {
    alert("Địa chỉ email không hợp lệ.");
    return false;
  }

  // Kiểm tra định dạng số điện thoại
  const phoneRegex = /^\d{10}$/;
  if (!phoneRegex.test(phone)) {
    alert("Số điện thoại không hợp lệ.");
    return false;
  }

  // Nếu thông tin khách hàng hợp lệ, trả về true
  return true;
}

// Lấy tham chiếu đến nút "Thanh toán"
const paymentButton = document.querySelector(".payment");

// Thêm sự kiện "click" cho nút "Thanh toán"
paymentButton.addEventListener("click", function (event) {
  // Ngăn chặn hành động mặc định của nút "Thanh toán" (submit form)
  event.preventDefault();

  // Kiểm tra thông tin khách hàng
  if (validateCheckoutForm()) {
    // Xóa sản phẩm trong giỏ hàng
    localStorage.removeItem("cart");

    // Chuyển hướng về trang chủ và hiển thị thông báo đặt hàng thành công
    window.location.href = "index.html";
    alert("Đặt hàng thành công.");
  }
});

// Lắng nghe sự kiện thay đổi phương thức thanh toán
var paymentMethods = document.getElementsByName("payment");
for (var i = 0; i < paymentMethods.length; i++) {
    paymentMethods[i].addEventListener("change", function(event) {
        var qrCodeContainer = document.getElementById("qr-code-container");
        var qrCode = document.getElementById("qr-code");
        
        if (event.target.value === "tranfer") {
            qrCode.src = "images/QR.jpg"; // Đường dẫn đến hình ảnh mã QR
            qrCodeContainer.style.display = "block";
        } else {
            qrCodeContainer.style.display = "none";
        }
    });
}

// jQuery(document).ready(function($)
// {
// 	"use strict";

// 	// Khởi tạo biến
// 	var numOfItems = 0; 
// 	var checkoutItems = $('#checkout_items');

// 	// Khi trang được tải xong
// 	window.onload = function()
// 	{
// 		// Lấy chuỗi giỏ hàng từ bộ nhớ cục bộ
// 		var cartString = localStorage.getItem('cart');
// 		if (cartString)
// 		{
// 			// Chuyển đổi chuỗi thành một mảng các sản phẩm
// 			var cartItems = JSON.parse(cartString);

// 		  	// Duyệt qua từng sản phẩm trong giỏ hàng
// 		  	for (var i = 0; i < cartItems.length; i++)
// 			{
// 		    	var item = cartItems[i];
// 				// Cộng dồn số lượng sản phẩm
// 		    	numOfItems += parseInt(item.quantity);
// 		  	}
// 		   	// Hiển thị số lượng sản phẩm trên trang
// 			checkoutItems.text(numOfItems);
// 		}
// 	}
// });

// // Kiểm tra xem người dùng đã đăng nhập hay chưa
// if (sessionStorage.getItem("loggedInUser"))
// {
// 	// Đã đăng nhập, ẩn nút đăng nhập và đăng ký, hiển thị tên đăng nhập và nút đăng xuất
// 	document.getElementById("loginLink").style.display = "none";
// 	document.getElementById("registerLink").style.display = "none";
// 	document.getElementById("logoutButton").style.display = "block";
// 	document.getElementById("accountName").textContent = sessionStorage.getItem("loggedInUser");
// }
// else 
// {
// 	// Chưa đăng nhập, hiển thị nút đăng nhập và đăng ký, ẩn tên đăng nhập và nút đăng xuất
// 	document.getElementById("loginLink").style.display = "block";
// 	document.getElementById("registerLink").style.display = "block";
// 	document.getElementById("logoutButton").style.display = "none";
// 	document.getElementById("accountName").textContent = "Tài khoản";
// }
  
//   // Xử lý sự kiện đăng xuất
// 	document.getElementById("logoutButton").addEventListener("click", () => {
// 	// Xóa tên đăng nhập khỏi sessionStorage
// 	sessionStorage.removeItem("loggedInUser");
// 	// Chuyển hướng đến trang chủ
// 	window.location.href = "../index.html";
// 	}
// );