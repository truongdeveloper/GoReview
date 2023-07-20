$(document).ready(function() {
    // Lặp qua tất cả các bài post và kiểm tra đoạn văn dài
    $(".post-text").each(function() {
        var paragraph = $(this).find(".post-text");
        var btnToggle = $(this).find("#btnToggle");

        var lineHeight = parseFloat(paragraph.css("line-height"));
        var maxLines = 3; // Số dòng tối đa

        // Kiểm tra đoạn văn dài
        if (paragraph[0].scrollHeight > lineHeight * maxLines) {
            btnToggle.show();
        } else {
            btnToggle.hide();
        }

        // Xử lý sự kiện khi nhấp vào nút "Xem thêm" trong bài post tương ứng
        btnToggle.click(function() {
            if (btnToggle.text() === "Xem thêm") {
                paragraph.css("max-height", "none");
                btnToggle.text("Thu gọn");
            } else {
                paragraph.css("max-height", lineHeight * maxLines + "px");
                btnToggle.text("Xem thêm");
            }
        });
    });
});