// Đặt ngày và giờ để đếm ngược
var fullTime = new Date("May 31, 2023 12:00:00").getTime();

// Cập nhật đếm ngược mỗi 1 giây
setInterval
(
	function()
    {
		var now = new Date().getTime(); // Lấy thời gian hiện tại
		var Dur = fullTime - now; // Tính toán thời gian còn lại cho đến ngày đếm ngược
		var days = Math.floor(Dur/(1000*60*60*24));
		var hours = Math.floor(Dur/(1000*60*60));
		var minutes = Math.floor(Dur/(1000*60));
		var seconds = Math.floor(Dur/(1000));
		
		// Phép toán modulo để lấy số giờ, phút và giây còn lại
		hours %= 24;
		minutes %= 60;
		seconds %= 60;
		
		// Hiển thị thời gian còn lại trên trang
		document.getElementById("day").innerHTML = days;
		document.getElementById("hour").innerHTML = hours;
		document.getElementById("minute").innerHTML = minutes;
		document.getElementById("second").innerHTML = seconds;
		
		// Nếu đếm ngược kết thúc, hiển thị số không
		if(Dur <= 0)
		{
			document.getElementById("day").innerHTML = 0;
			document.getElementById("hour").innerHTML = 0;
			document.getElementById("minute").innerHTML = 0;
			document.getElementById("second").innerHTML = 0;
		}
	}
)

// Chờ cho tài liệu được tải xong
jQuery(document).ready(function($)
{
	"use strict";

	// Khởi tạo biến
	var numOfItems = 0; 
	var checkoutItems = $('#checkout_items');

	// Khi trang được tải xong
	window.onload = function()
	{
		// Lấy chuỗi giỏ hàng từ bộ nhớ cục bộ
		var cartString = localStorage.getItem('cart');
		if (cartString)
		{
			// Chuyển đổi chuỗi thành một mảng các sản phẩm
			var cartItems = JSON.parse(cartString);

		  	// Duyệt qua từng sản phẩm trong giỏ hàng
		  	for (var i = 0; i < cartItems.length; i++)
			{
		    	var item = cartItems[i];
				// Cộng dồn số lượng sản phẩm
		    	numOfItems += parseInt(item.quantity);
		  	}
		   	// Hiển thị số lượng sản phẩm trên trang
			checkoutItems.text(numOfItems);
		}
	}

	//Hiển thị / Ẩn thanh tìm kiếm
	document.querySelector('.fa-search').addEventListener('click', function() {
	var searchBox = document.getElementById('search-box');
	if (searchBox.style.display === 'none') {
	searchBox.style.display = 'block';
	} else {
	searchBox.style.display = 'none';
	}
  });
});

// Kiểm tra xem người dùng đã đăng nhập hay chưa
if (sessionStorage.getItem("loggedInUser"))
{
	// Đã đăng nhập, ẩn nút đăng nhập và đăng ký, hiển thị tên đăng nhập và nút đăng xuất
	document.getElementById("loginLink").style.display = "none";
	document.getElementById("registerLink").style.display = "none";
	document.getElementById("logoutButton").style.display = "block";
	document.getElementById("accountName").textContent = sessionStorage.getItem("loggedInUser");
}
else 
{
	// Chưa đăng nhập, hiển thị nút đăng nhập và đăng ký, ẩn tên đăng nhập và nút đăng xuất
	document.getElementById("loginLink").style.display = "block";
	document.getElementById("registerLink").style.display = "block";
	document.getElementById("logoutButton").style.display = "none";
	document.getElementById("accountName").textContent = "Tài khoản";
}
  
  // Xử lý sự kiện đăng xuất
	document.getElementById("logoutButton").addEventListener("click", () => {
	// Xóa tên đăng nhập khỏi sessionStorage
	sessionStorage.removeItem("loggedInUser");
	// Chuyển hướng đến trang chủ
	window.location.href = "../index.html";
	}
);