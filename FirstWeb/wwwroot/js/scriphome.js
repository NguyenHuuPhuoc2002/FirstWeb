

function navigateToPage(pageIndex) {
    // Lưu vị trí cuộn hiện tại vào localStorage
    localStorage.setItem('scrollPosition', window.scrollY);
// Chuyển hướng đến trang với index
window.location.href = `@Url.Action("Index", "Home")?index=${pageIndex}`;
}

// Khi trang tải lại, khôi phục vị trí cuộn từ localStorage
window.onload = function () {
    var scrollPosition = localStorage.getItem('scrollPosition');
if (scrollPosition) {
    window.scrollTo(0, scrollPosition);
localStorage.removeItem('scrollPosition');
    }
}
