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



var modal = $("#myModal");
var closeButton = $(".close, #cancelButton");
var confirmButton = $("#confirmButton");
var isDelete = false;

function handleDeletePost (postId) {// Lấy ID bài post từ trang của bạn, có thể lấy từ một thuộc tính data hoặc một input ẩn, ví dụ: $("#postIdInput").val();
    modal.show();

    closeButton.click(function() {
        modal.hide();
    });

    // Do something when confirmButton is clicked
    confirmButton.click(function() {
        // Your delete logic here...
        if(!isDelete) {
            isDelete = true;
            $.ajax({
                type: "POST",
                url: "/Profile/DeleteByUserConfirmed", // Đường dẫn tới action DeletePost trong controller Profile
                data: { id: postId }, // Truyền tham số ID bài post vào đây
                success: function (data) {
                    if (data.success) {
                        // Xóa bài post thành công, thực hiện các tác vụ cần thiết
                        alert("Xóa bài post thành công!");
                        $("#post_" + postId).remove();
                        isDelete = false;
                    } else {
                        // Xóa bài post thất bại hoặc không tìm thấy bài post, xử lý thông báo lỗi tại đây
                        alert("Xóa bài post không thành công!");
                        isDelete = false;
                    }
                },
                error: function () {
                    // Xử lý lỗi khi gọi Ajax
                    alert("Đã xảy ra lỗi khi xóa bài post!");
                }
            });
            modal.hide();
        }

    });
    
};


function handleSearch() {
    var keyword = $("#inputSearch").val();

    $.ajax({
        url: "/Home/IndexAsync",
        type: "POST",
        data: { keyword: keyword },
        success: function (data) {
            displaySearchResults(data);
        },
        error: function () {
            // Xử lý lỗi khi gọi Ajax
            alert("Đã xảy ra lỗi khi Search!");
        }
    });

}

function displaySearchResults(data) {
    var resultsDiv = $("#searchResults");
    resultsDiv.empty();

    if (data != null) {
        $.each(data, function (index, item) {
            // Xây dựng nội dung HTML để hiển thị thông tin bài viết
            // Ví dụ:
            var html = `
                <div class="post">
                    <h3>${item.Title}</h3>
                    <p>${item.Content}</p>
                </div>
            `;

            // Thêm nội dung HTML vào resultsDiv
            resultsDiv.append(html);
        });
    } else {
        resultsDiv.text("Không tìm thấy kết quả.");
    }
}
