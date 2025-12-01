window.animateProductToCart = function (productElementId) {
    const productElement = document.getElementById(productElementId);
    if (!productElement) return;

    const imageWrapper = productElement.querySelector('.product-image-wrapper');
    if (!imageWrapper) return;

    const cartLink = document.querySelector('.cart-nav-link');
    if (!cartLink) return;

    // Tạo clone của ảnh sản phẩm
    const image = imageWrapper.querySelector('img') || imageWrapper.querySelector('.product-image-placeholder');
    if (!image) return;

    const clone = image.cloneNode(true);
    clone.style.position = 'fixed';
    clone.style.width = image.offsetWidth + 'px';
    clone.style.height = image.offsetHeight + 'px';
    clone.style.zIndex = '9999';
    clone.style.pointerEvents = 'none';
    clone.style.transition = 'all 0.6s cubic-bezier(0.68, -0.55, 0.265, 1.55)';
    
    const rect = image.getBoundingClientRect();
    clone.style.left = rect.left + 'px';
    clone.style.top = rect.top + 'px';
    
    document.body.appendChild(clone);

    // Lấy vị trí của nút giỏ hàng
    const cartRect = cartLink.getBoundingClientRect();
    const targetX = cartRect.left + cartRect.width / 2;
    const targetY = cartRect.top + cartRect.height / 2;

    // Trigger animation
    requestAnimationFrame(() => {
        clone.style.left = targetX + 'px';
        clone.style.top = targetY + 'px';
        clone.style.width = '20px';
        clone.style.height = '20px';
        clone.style.opacity = '0.5';
        clone.style.transform = 'scale(0.3)';
    });

    // Xóa clone sau khi animation xong
    setTimeout(() => {
        if (clone.parentNode) {
            clone.parentNode.removeChild(clone);
        }
    }, 600);
};

