//xóa cart
var remove_cart = document.getElementsByClassName("btn-danger");
for (var i = 0; i < remove_cart.length; i++) {
      var button = remove_cart[i]
      button.addEventListener("click", function () {
      var button_remove = event.target
      button_remove.parentElement.parentElement.remove()
      })
      updatecart()

}

//update cart
function updatecart() {
    var cart_item = document.getElementsByClassName("cart-items")[0];
    var cart_rows = cart_item.getElementsByClassName("cart-row");
    var total = 0;
    for (var i = 0; i < cart_rows.length; i++) {
      var cart_row = cart_rows[i]
      var price_item = cart_row.getElementsByClassName("cart-price ")[0]
      var quantity_item = cart_row.getElementsByClassName("cart-quantity-input")[0]
      var price = parseInt(price_item.innerText.replace('.', '').trim()); 
      //replace('.', '').trim() chuyển đổi giá trị tiền từ chuỗi sang số và bỏ qua các ký tự không phải số
      var quantity = quantity_item.value // lấy giá trị trong thẻ input
      total = total + (price * quantity)
    }
    document.getElementsByClassName("cart-total-price")[0].innerText = total.toLocaleString('vi-VN', { style: 'currency', currency: 'VND', minimumFractionDigits: 0 });
    //toLocaleString chuyển đổi số sang chuỗi với định dạng có dấu chấm phân tách hàng nghìn và dấu phẩy phân tách phần thập phân.
    // Thay đổi text = total trong .cart-total-price. Chỉ có một .cart-total-price nên mình sử dụng [0].
  }

//thay đổi số lượng sản phẩm 
var quantity_input = document.getElementsByClassName("cart-quantity-input");
for (var i = 0; i < quantity_input.length; i++) {
      var input = quantity_input[i];
      input.addEventListener("change", function (event) {
        var input = event.target
        if (isNaN(input.value) || input.value <= 0) {
              input.value = 1;
        }
        updatecart()
      })
} 

// // Thêm vào giỏ
// var add_cart = document.getElementsByClassName("btnAddToCart");
// for (var i = 0; i < add_cart.length; i++) {
//   var add = add_cart[i];
//   add.addEventListener("click", function (event) {

//     var button = event.target;
//     var product = button.parentElement.parentElement;
//     var img = product.parentElement.getElementsByClassName("product-content-left-big-img")[0].src
//     var title = product.getElementsByClassName("product-content-right-name-h1")[0].innerText
//     var price = product.getElementsByClassName("product_price")[0].innerText
//     addItemToCart(title, price, img)
//     updatecart()
//   })
// }

// Lưu giỏ hàng vào localStorage
function saveCart() {
  var cartItems = document.getElementsByClassName('cart-items')[0]
  var cartRows = cartItems.getElementsByClassName('cart-row')
  var cart = []
  for (var i = 0; i < cartRows.length; i++) {
    var cartRow = cartRows[i]
    var title = cartRow.getElementsByClassName('cart-item-title')[0].innerText
    var price = cartRow.getElementsByClassName('cart-price')[0].innerText
    var imgSrc = cartRow.getElementsByClassName('cart-item-image')[0].src
    var quantity = parseInt(cartRow.getElementsByClassName('cart-quantity-input')[0].value)
    var item = {title: title, price: price, imgSrc: imgSrc, quantity: quantity}
    cart.push(item)
  }
  localStorage.setItem('cart', JSON.stringify(cart))
}

	// Thêm sản phẩm vào giỏ hàng và lưu vào localStorage
function addItemToCart(title, price, img, quantity) {
  var cartRow = document.createElement('div')
  cartRow.classList.add('cart-row')
  var cartItems = document.getElementsByClassName('cart-items')[0]
  var cart_title = cartItems.getElementsByClassName('cart-item-title')
  var cartRowContents = `
  <div class="cart-item cart-column">
      <img class="cart-item-image" src="${img}" width="100" height="90">
      <span class="cart-item-title">${title}</span>
  </div>
  <span class="cart-price cart-column">${price}</span>
  <div class="cart-quantity cart-column">
      <input class="cart-quantity-input" type="number" value="${quantity}">
      <button class="btn btn-danger">Xóa</button>
  </div>`
  cartRow.innerHTML = cartRowContents
  cartItems.append(cartRow)
  cartRow.getElementsByClassName('btn-danger')[0].addEventListener('click', function () {
    var button_remove = event.target
    button_remove.parentElement.parentElement.remove()
    updatecart()
    saveCart()
  })
  cartRow.getElementsByClassName('cart-quantity-input')[0].addEventListener('change', function (event) {
    var input = event.target
    if (isNaN(input.value) || input.value <= 0) {
      input.value = 1;
    }
    updatecart()
    saveCart()
  })
  saveCart()
}

// Load giỏ hàng từ localStorage
function loadCart() {
  var cart = JSON.parse(localStorage.getItem('cart'))
  if (cart) {
    for (var i = 0; i < cart.length; i++) {
      var item = cart[i]
      addItemToCart(item.title, item.price, item.imgSrc, item.quantity)
    }
    updatecart()
  }
}

// Load giỏ hàng khi trang được load
function loadCartFromLocalStorage() {
  var cartString = localStorage.getItem('cart');
  if (cartString) {
    var cart = JSON.parse(cartString);
    for (var i = 0; i < cart.length; i++) {
      var item = cart[i];
      console.log(item);
      addItemToCart(item.title, item.price, item.imgSrc, item.quantity);
    }
    updatecart();
  }
}

//Load số lượng trong giỏ hàng
var numOfItems = 0;
var checkoutItems = document.querySelector('#checkout_items');

window.onload = function() {
  loadCartFromLocalStorage();
  var cartString = localStorage.getItem('cart');
  if (cartString) {
    var cartItems = JSON.parse(cartString);
    for (var i = 0; i < cartItems.length; i++) {
      var item = cartItems[i];
      numOfItems += parseInt(item.quantity);
    }
    // Hiển thị số lượng sản phẩm trong giỏ hàng ban đầu
    checkoutItems.innerHTML = numOfItems;
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

var orderButton = document.getElementById('order-button');
orderButton.addEventListener('click', function(event) {
    var cartItems = document.querySelector('.cart-items');
    if (cartItems.children.length === 0) {
        event.preventDefault(); // Ngăn chặn hành vi mặc định của liên kết
        alert('Không có sản phẩm trong giỏ hàng');
    } else {
        window.location.href="payment.html"
    }
});

