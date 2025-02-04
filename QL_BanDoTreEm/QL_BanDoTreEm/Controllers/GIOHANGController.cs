using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QL_BanDoTreEm.Models;

namespace QL_BanDoTreEm.Controllers
{
    public class GIOHANGController : Controller
    {
        // GET: GIOHANG

        public ActionResult Index()
        {
            if (Session["TEN"] == null)
                return RedirectToAction("LoginAdmin", "DANGNHAP");

            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            string tendangnhap = Session["TEN"].ToString();

            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();

            GIOHANG GH = dB_BANHANGEntities.GIOHANG.Where(t => t.ID_KHACHHANG == kh.ID_KHACHHANG).First();

            return View(dB_BANHANGEntities.CHITIET_GIOHANG.Where(T => T.ID_GIOHANG == GH.ID_GIOHANG).ToList());
        }

        [HttpGet]
        public bool InsertShoppingCard(string maSP)
        {
            if (Session["TEN"] == null)
                return false;
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            string tendangnhap = Session["TEN"].ToString();

            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();
            SANPHAM sp = dB_BANHANGEntities.SANPHAM.Where(t => t.ID_SANPHAM == maSP).First();
            GIOHANG GH = dB_BANHANGEntities.GIOHANG.Where(t => t.ID_KHACHHANG == kh.ID_KHACHHANG).First();


            if (dB_BANHANGEntities.CHITIET_GIOHANG.Where(t => t.ID_SANPHAM == maSP && t.ID_GIOHANG == GH.ID_GIOHANG).Count() == 0)
            {
                CHITIET_GIOHANG ctgh = new CHITIET_GIOHANG();
                ctgh.ID_SANPHAM = maSP;
                ctgh.ID_GIOHANG = GH.ID_GIOHANG;
                ctgh.SOLUONGMUA = 1;
                ctgh.THANHTIEN = sp.DONGIABAN;

                dB_BANHANGEntities.CHITIET_GIOHANG.Add(ctgh);
                dB_BANHANGEntities.SaveChanges();

            }
            List<CHITIET_GIOHANG> ds_ctgh = GH.CHITIET_GIOHANG.ToList();
            Session["SoSP_GH"] = ds_ctgh.Count();

            return true;
        }
        [HttpGet]
        public ActionResult CapNhatSoLuong(string maHD, string maHH, int soLuong)
        {
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            CHITIET_GIOHANG ctgh = dB_BANHANGEntities.CHITIET_GIOHANG.Where(t => t.ID_SANPHAM == maHH).OrderByDescending(t => t.ID_SANPHAM).First();
            ctgh.SOLUONGMUA = soLuong;
            ctgh.THANHTIEN = ctgh.SOLUONGMUA * ctgh.SANPHAM.DONGIABAN;
            dB_BANHANGEntities.SaveChanges();

            List<CHITIET_GIOHANG> list = dB_BANHANGEntities.CHITIET_GIOHANG.Where(T => T.ID_GIOHANG == maHD).ToList();
            return PartialView("ChiTietGioHang", list);
        }

        [HttpGet]
        public ActionResult XoaChiTietHoaDon(string maHD, string maHH)
        {
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            CHITIET_GIOHANG cthd = dB_BANHANGEntities.CHITIET_GIOHANG.Where(t => t.ID_SANPHAM == maHH).OrderByDescending(t => t.ID_SANPHAM).First();
            dB_BANHANGEntities.CHITIET_GIOHANG.Remove(cthd);
            dB_BANHANGEntities.SaveChanges();

            List<CHITIET_GIOHANG> ds_ctgh = dB_BANHANGEntities.CHITIET_GIOHANG.Where(T => T.ID_GIOHANG == maHD).ToList();

            Session["SoSP_GH"] = ds_ctgh.Count();

            return PartialView("ChiTietGioHang", ds_ctgh);
        }

        [HttpPost]
        public ActionResult ThanhToan(string[] sanPhamMua, string vnPay)
        {

            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();

            List<CHITIET_GIOHANG> ds_ctgh = new List<CHITIET_GIOHANG>();

            // Mã giỏ hàng [0], Mã sản phẩm [1]
            List<(string, string)> list = new List<(string, string)>();

            if (sanPhamMua == null)

                return RedirectToAction("Index");
            foreach (string s in sanPhamMua)
            {
                string maGH = s.Split('-')[0];
                string maSP = s.Split('-')[1];
                list.Add((maGH, maSP));

                CHITIET_GIOHANG ctgh = dB_BANHANGEntities.CHITIET_GIOHANG
                    .Where(t => t.ID_SANPHAM == maSP && t.ID_GIOHANG == maGH).FirstOrDefault();
                ds_ctgh.Add(ctgh);
            }


            string tendangnhap = Session["TEN"].ToString();
            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();

            // Lấy mã mới cho hóa đơn
            string maHD = dB_BANHANGEntities.HOADON.Max(t => t.ID_HOADON);
            maHD = Helper.getNewKey(maHD);

            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime nowVST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            // Tạo hóa đơn
            HOADON hd = new HOADON()
            {
                ID_HOADON = maHD,
                NGAYLAP = DateTime.Now,
                ID_KHACHHANG = kh.ID_KHACHHANG,
                PHIVANCHUYEN = 0,
                TRANGTHAI = "Chuẩn bị hàng",
            };
            dB_BANHANGEntities.HOADON.Add(hd);

            int tongThanhTien = 0; // Tổng thành tiền của hóa đơn

            // Thêm chi tiết hóa đơn
            foreach (CHITIET_GIOHANG ctgh in ds_ctgh)
            {

                CHITIET_HOADON cthd = new CHITIET_HOADON()
                {
                    ID_HOADON = maHD,
                    ID_SANPHAM = ctgh.ID_SANPHAM,
                    SOLUONGMUA = ctgh.SOLUONGMUA,
                    THANHTIEN = ctgh.THANHTIEN,
                };

                tongThanhTien += cthd.THANHTIEN ?? 0;

                dB_BANHANGEntities.CHITIET_HOADON.Add(cthd);

                // Lấy thông tin sản phẩm và cập nhật SoLuong
                List<SANPHAM> sp = dB_BANHANGEntities.SANPHAM.Where(t => t.ID_SANPHAM == ctgh.ID_SANPHAM).ToList();
                foreach (SANPHAM s in sp)
                {
                    int soLuong = int.Parse(s.SOLUONG);
                    int soLuongMua = int.Parse(ctgh.SOLUONGMUA.ToString());

                    s.SOLUONG = (soLuong - soLuongMua).ToString();
                }

            }

            hd.TONGTIEN = tongThanhTien;// Cập nhật lại tổng thành tiền

            dB_BANHANGEntities.SaveChanges();
            //
            //List<SANPHAM>listSP = dB_BANHANGEntities.SANPHAM.Where(t=>t.id)
            int offline = 1;
            if (vnPay == "bank")
            {
                offline = 0;
                UrlPayment(hd.ID_HOADON);
            }
            SendEmail(offline, hd.ID_HOADON);

            // xóa chi tiết giỏ hàng
            dB_BANHANGEntities.CHITIET_GIOHANG.RemoveRange(ds_ctgh);
            dB_BANHANGEntities.SaveChanges();

            Session["SoSP_GH"] = ds_ctgh.Count();
            Session["SoSP_GH"] = 0;
            return RedirectToAction("Index", "LICHSUMUAHANG");
        }

        public List<CHITIET_HOADON> getData(string id)
        {
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();


            string tendangnhap = Session["TEN"].ToString();

            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();

            HOADON hd = dB_BANHANGEntities.HOADON.Where(t => t.ID_HOADON == id).First();

            List<CHITIET_HOADON> cthd = dB_BANHANGEntities.CHITIET_HOADON.Where(t => t.ID_HOADON == hd.ID_HOADON).OrderByDescending(t => t.ID_HOADON).ToList();
            return cthd;
        }
        public void SendEmail(int hinhthucthanhtoan, string id)
        {
            DB_BANHANGEntities dB_BANHANGEntities = new DB_BANHANGEntities();
            string tendangnhap = Session["TEN"].ToString();

            KHACHHANG kh = dB_BANHANGEntities.KHACHHANG.Where(t => t.TENDANGNHAP == tendangnhap).First();
            List<CHITIET_HOADON> listCTHD = getData(id);
            // send mail
            var str_SP = "";
            var Thanhtien = decimal.Zero;
            var Tongtien = decimal.Zero;

            //var u = Session["use"] as QL_BanDoTreEm.Models.KHACHHANG;
            //model.SDT_KH = Session["u"] as string;
            var Sdt = kh.DIENTHOAI;
            var Tenkhachhang = kh.TENKHACHHANG;
            var Email = kh.EMAIL;
            var Trangthai = "";
            if (hinhthucthanhtoan == 1)
            {
                Trangthai = "Thanh toán khi nhận hàng";
            }
            else
            {
                Trangthai = "Thanh toán trực tuyến";
            }
            var Diachinhanhang = kh.DIACHI;
            foreach (var sp in listCTHD)
            {
                str_SP += "<tr>";
                str_SP += "<td>" + sp.SANPHAM.TENSANPHAM + "</td>";
                str_SP += "<td>" + sp.SOLUONGMUA + "</td>";
                str_SP += "<td>" + sp.SANPHAM.DONGIABAN + "</td>";
                str_SP += "<td>" + sp.THANHTIEN + "</td>";
                Tongtien = Tongtien + int.Parse(sp.THANHTIEN.ToString());
            }


            //Tongtien = (int)TongThanhTien() + phiShip;
            //if (Tongtien >= 299000)
            //{
            //    Tongtien -= phiShip;
            //}
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/Template/send2.html"));

            string mahd = listCTHD.First().ID_HOADON.ToString();
            contentCustomer = contentCustomer.Replace("{{Madon}}", listCTHD.First().ID_HOADON.ToString());
            contentCustomer = contentCustomer.Replace("{{Sanpham}}", str_SP.ToString());
            contentCustomer = contentCustomer.Replace("{{Tenkhachhang}}", Tenkhachhang);
            contentCustomer = contentCustomer.Replace("{{Sdt}}", Sdt);
            contentCustomer = contentCustomer.Replace("{{Email}}", Email);
            contentCustomer = contentCustomer.Replace("{{Diachinhanhang}}", Diachinhanhang);
            contentCustomer = contentCustomer.Replace("{{Thanhtien}}", Thanhtien.ToString());
            contentCustomer = contentCustomer.Replace("{{Tongtien}}", (string.Format("{0:0,000}", Tongtien) + ".000đ"));
            contentCustomer = contentCustomer.Replace("{{Trangthai}}", Trangthai.ToString());
            QL_BanDoTreEm.Mail.Mail.sendEmail("BabyZone", "Đơn hàng #" + mahd, contentCustomer.ToString(), Email.Trim());
        }

        public void UrlPayment(string id)
        {
            var Tongtien = decimal.Zero;
            //string mahd = taoMaHD();
            // lấy mã hóa đơn
            List<CHITIET_HOADON> listCTHD = getData(id);

            foreach (var hd in listCTHD)
            {
                Tongtien = Tongtien + int.Parse(hd.THANHTIEN.ToString());
            }


            var urlPayment = "";

            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            Tongtien *= 100 * 1000;
            //
            //var Price = (long)order.TotalAmount * 100;

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Tongtien.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "NCB");


            string mahd = listCTHD.First().ID_HOADON.ToString();
            DateTime CreatedDate = DateTime.Now;
            vnpay.AddRequestData("vnp_CreateDate", CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + mahd);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);

            Random random = new Random();
            int randomNumber = random.Next(1, 10000);
            string _idTrans = mahd + randomNumber;
            vnpay.AddRequestData("vnp_TxnRef", _idTrans); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            Response.Redirect(urlPayment);
            //return urlPayment;

        }

        //Xử lí vnpay             
        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                // long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                
            }
            //var a = UrlPayment(0, "DH3574");

            return RedirectToAction("Index");
        }
    }
}