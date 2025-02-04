// Khởi tạo chỉ số slide hiện tại
let slideIndex = 1;
// Hiển thị slide đầu tiên
showSlides(slideIndex);

// Hàm di chuyển đến slide trước/sau
function plusSlides(n) {
  showSlides(slideIndex += n);
}

// Hàm di chuyển đến slide cụ thể
function currentSlide(n) {
  showSlides(slideIndex = n);
}

// Hàm hiển thị slide cụ thể
function showSlides(n) {
  // Lấy danh sách các slide và các chấm điều khiển
  const slides = document.getElementsByClassName("slide");
  const dots = document.getElementsByClassName("dot");

  // Nếu chỉ số slide vượt quá số lượng slide, quay lại slide đầu tiên
  if (n > slides.length) {
    slideIndex = 1;
  }
  // Nếu chỉ số slide nhỏ hơn 1, di chuyển đến slide cuối cùng
  if (n < 1) {
    slideIndex = slides.length;
  }
  // Ẩn tất cả các slide
  for (let i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  // Xóa class "active" khỏi tất cả các chấm điều khiển
  for (let i = 0; i < dots.length; i++) {
    dots[i].classList.remove("active");
  }
  // Hiển thị slide hiện tại và thêm class "active" cho chấm điều khiển tương ứng
  slides[slideIndex - 1].style.display = "block";
  dots[slideIndex - 1].classList.add("active");
}

// Thêm sự kiện cho nút điều khiển trước/sau
document.querySelector(".prev").addEventListener("click", () => plusSlides(-1));
document.querySelector(".next").addEventListener("click", () => plusSlides(1));

// Thêm sự kiện cho các chấm điều khiển
const dots = document.getElementsByClassName("dot");
for (let i = 0; i < dots.length; i++) {
  dots[i].addEventListener("click", () => currentSlide(i + 1));
}