/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Set Header
3. Init Menu
4. Init Favorite
5. Init Fix Product Border
6. Init Isotope Filtering
7. Init Price Slider
8. Init Checkboxes
9. Lọc sản phẩm theo thể loại



******************************/

jQuery(document).ready(function($)
{
	"use strict";

	/* 

	1. Vars and Inits

	*/

	var header = $('.header');
	var topNav = $('.top_nav')
	var mainSlider = $('.main_slider');
	var hamburger = $('.hamburger_container');
	var menu = $('.hamburger_menu');
	var menuActive = false;
	var hamburgerClose = $('.hamburger_close');
	var fsOverlay = $('.fs_menu_overlay');

	setHeader();

	$(window).on('resize', function()
	{
		initFixProductBorder();
		setHeader();
	});


	initMenu();
	initFavorite();
	initFixProductBorder();
	initIsotopeFiltering();
	initPriceSlider();
	initCheckboxes();

	/* 

	2. Set Header

	*/

	function setHeader()
	{
		if(window.innerWidth < 992)
		{
			if($(window).scrollTop() > 100)
			{
				header.css({'top':"0"});
			}
			else
			{
				header.css({'top':"0"});
			}
		}
		else
		{
			if($(window).scrollTop() > 100)
			{
				header.css({'top':"-50px"});
			}
			else
			{
				header.css({'top':"0"});
			}
		}
		if(window.innerWidth > 991 && menuActive)
		{
			closeMenu();
		}
	}

	/* 

	3. Init Menu

	*/

	function initMenu()
	{
		if(hamburger.length)
		{
			hamburger.on('click', function()
			{
				if(!menuActive)
				{
					openMenu();
				}
			});
		}

		if(fsOverlay.length)
		{
			fsOverlay.on('click', function()
			{
				if(menuActive)
				{
					closeMenu();
				}
			});
		}

		if(hamburgerClose.length)
		{
			hamburgerClose.on('click', function()
			{
				if(menuActive)
				{
					closeMenu();
				}
			});
		}

		if($('.menu_item').length)
		{
			var items = document.getElementsByClassName('menu_item');
			var i;

			for(i = 0; i < items.length; i++)
			{
				if(items[i].classList.contains("has-children"))
				{
					items[i].onclick = function()
					{
						this.classList.toggle("active");
						var panel = this.children[1];
					    if(panel.style.maxHeight)
					    {
					    	panel.style.maxHeight = null;
					    }
					    else
					    {
					    	panel.style.maxHeight = panel.scrollHeight + "px";
					    }
					}
				}	
			}
		}
	}

	function openMenu()
	{
		menu.addClass('active');
		// menu.css('right', "0");
		fsOverlay.css('pointer-events', "auto");
		menuActive = true;
	}

	function closeMenu()
	{
		menu.removeClass('active');
		fsOverlay.css('pointer-events', "none");
		menuActive = false;
	}

	/* 

	4. Init Favorite

	*/

    function initFavorite()
    {
    	if($('.favorite').length)
    	{
    		var favs = $('.favorite');

    		favs.each(function()
    		{
    			var fav = $(this);
    			var active = false;
    			if(fav.hasClass('active'))
    			{
    				active = true;
    			}

    			fav.on('click', function()
    			{
    				if(active)
    				{
    					fav.removeClass('active');
    					active = false;
    				}
    				else
    				{
    					fav.addClass('active');
    					active = true;
    				}
    			});
    		});
    	}
    }

    /* 

	5. Init Fix Product Border

	*/

    function initFixProductBorder()
    {
    	if($('.product_filter').length)
    	{
			var products = $('.product_filter:visible');
    		var wdth = window.innerWidth;

    		// reset border
    		products.each(function()
    		{
    			$(this).css('border-right', 'solid 1px #e9e9e9');
    		});

    		// Nếu khung màn hình <= 991px

    		if(wdth < 480)
			{
				for(var i = 0; i < products.length; i++)
				{
					var product = $(products[i]);
					product.css('border-right', 'none');
				}
			}

    		else if(wdth < 576)
			{
				if(products.length < 5)
				{
					var product = $(products[products.length - 1]);
					product.css('border-right', 'none');
				}
				for(var i = 1; i < products.length; i+=2)
				{
					var product = $(products[i]);
					product.css('border-right', 'none');
				}
			}

    		else if(wdth < 768)
			{
				if(products.length < 5)
				{
					var product = $(products[products.length - 1]);
					product.css('border-right', 'none');
				}
				for(var i = 2; i < products.length; i+=3)
				{
					var product = $(products[i]);
					product.css('border-right', 'none');
				}
			}

    		else if(wdth < 992)
			{
				if(products.length < 5)
				{
					var product = $(products[products.length - 1]);
					product.css('border-right', 'none');
				}
				for(var i = 2; i < products.length; i+=3)
				{
					var product = $(products[i]);
					product.css('border-right', 'none');
				}
			}

			//Nếu khung màn hình lớn hơn 991px
			else
			{
				if(products.length < 5)
				{
					var product = $(products[products.length - 1]);
					product.css('border-right', 'none');
				}
				for(var i = 3; i < products.length; i+=4)
				{
					var product = $(products[i]);
					product.css('border-right', 'none');
				}
			}	
    	}
    }

    /* 

	6. Init Isotope Filtering

	*/

    function initIsotopeFiltering()
    {
    	var sortTypes = $('.type_sorting_btn');
    	var sortNums = $('.num_sorting_btn');
    	var sortTypesSelected = $('.sorting_type .item_sorting_btn is-checked span');
    	var filterButton = $('.filter_button');

    	if($('.product-grid').length)
    	{
    		$('.product-grid').isotope({
    			itemSelector: '.product-item',
	            getSortData: {
	            	price: function(itemElement)
	            	{
	            		var priceEle = $(itemElement).find('.product_price').contents().filter(function() {
							return this.nodeType == 3;
						}).text().replace( 'VNĐ', '' ).replace( /\./g,'' );
	            		return parseFloat(priceEle);
	            	},
					name: function(itemElement)
    				{
    				    var nameEle = $(itemElement).find('.product_name').text();
    				    return nameEle.trim().charAt(0);
    				}
	            },
	            animationOptions: {
	                duration: 750,
	                easing: 'linear',
	                queue: false
	            }
	        });

    		// Sắp xếp tăng dần theo giá-tên sản phẩm
	        sortTypes.each(function()
	        {
	        	$(this).on('click', function()
	        	{
	        		$('.type_sorting_text').text($(this).text());
	        		var option = $(this).attr('data-isotope-option');
	        		option = JSON.parse( option );
    				$('.product-grid').isotope( option );
	        	});
	        });

	        // Hiển thị số sản phẩm
	        sortNums.each(function()
	        {
	        	$(this).on('click', function()
	        	{
	        		var numSortingText = $(this).text();
					var numFilter = ':nth-child(-n+' + numSortingText + ')';
	        		$('.num_sorting_text').text($(this).text());
    				$('.product-grid').isotope({filter: numFilter });
	        	});
	        });	

	        // Thanh lọc giá #amount
	        filterButton.on('click', function()
	        {
	        	$('.product-grid').isotope({
		            filter: function()
		            {
		            	var priceRange = $('#amount').val();
			        	var priceMin = parseFloat(priceRange.split('-')[0].replace('VNĐ', ''));
			        	var priceMax = parseFloat(priceRange.split('-')[1].replace('VNĐ', ''));

			        	var itemPrice = $(this).find('.product_price').clone().children().remove().end().text().replace( 'VNĐ', '');
						var itemPrice2 =itemPrice.replace('.', '');

			        	return ((itemPrice > priceMin) && (itemPrice < priceMax)) ||  ((itemPrice2 > priceMin) && (itemPrice2 < priceMax));
		            },
		            animationOptions: {
		                duration: 750,
		                easing: 'linear',
		                queue: false
		            }
		        });
	        });
    	}
    }

    /* 

	7. Init Price Slider

	*/

    function initPriceSlider()
    {
		$( "#slider-range" ).slider(
		{
			range: true,
			min: 0,
			max: 2000,
			values: [ 0, 580 ],
			slide: function( event, ui )
			{
				$( "#amount" ).val(ui.values[ 0 ] + " VNĐ" + " - " + ui.values[ 1 ] + ".000" + " VNĐ");
			}
		});
			
		$( "#amount" ).val($( "#slider-range" ).slider( "values", 0 )+ " VNĐ" + " - " + $( "#slider-range" ).slider( "values", 1 ) + ".000" + " VNĐ");
    }

    /* 

	8. Init Checkboxes

	*/

    function initCheckboxes()
    {
    	if($('.checkboxes li').length)
    	{
    		var boxes = $('.checkboxes li');

    		boxes.each(function()
    		{
    			var box = $(this);

    			box.on('click', function()
    			{
    				if(box.hasClass('active'))
    				{
    					box.find('i').removeClass('fa-square');
    					box.find('i').addClass('fa-square-o');
    					box.toggleClass('active');
    				}
    				else
    				{
    					box.find('i').removeClass('fa-square-o');
    					box.find('i').addClass('fa-square');
    					box.toggleClass('active');
    				}
    				// box.toggleClass('active');
    			});
    		});

    		if($('.show_more').length)
    		{
    			var checkboxes = $('.checkboxes');

    			$('.show_more').on('click', function()
    			{
    				checkboxes.toggleClass('active');
    			});
    		}
    	};
    }


	////9. Lọc sản phẩm theo loại
	//$('.sidebar_categories a').on('click', function(event) {
	//	event.preventDefault();
	//	var filterValue = $(this).attr('data-filter');
	//	$('.product-grid').isotope({ filter: filterValue });

	//	$('.sidebar_categories li').removeClass('active');
 //   	$(this).closest('li').addClass('active');
	//});
	  
	//10.
	var numOfItems = 0; 
	var checkoutItems = $('#checkout_items');
	window.onload = function() {
		var cartString = localStorage.getItem('cart');
  	if (cartString) {
			var cart = JSON.parse(cartString);
			for (var i = 0; i < cart.length; i++) {
				var item = cart[i];
				numOfItems += parseInt(item.quantity);
			}
			checkoutItems.text(numOfItems);
		}
	}

	//11. Hiển thị / Ẩn thanh tìm kiếm
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