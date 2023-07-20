$(document).ready(function() {
    


    // Hide the modal when closeButton is clicked

});

var modal = $("#myModal");
var closeButton = $(".close, #cancelButton");
var confirmButton = $("#confirmButton");

function handleDeletePost (postId) {// Lấy ID bài post từ trang của bạn, có thể lấy từ một thuộc tính data hoặc một input ẩn, ví dụ: $("#postIdInput").val();
    modal.show();

    closeButton.click(function() {
        modal.hide();
    });

    // Do something when confirmButton is clicked
    confirmButton.click(function() {
        // Your delete logic here...
        $.ajax({
            type: "POST",
            url: "/Posts/DeleteConfirmed", // Đường dẫn tới action DeletePost trong controller Profile
            data: { id: postId }, // Truyền tham số ID bài post vào đây
            success: function (data) {
                if (data.success) {
                    // Xóa bài post thành công, thực hiện các tác vụ cần thiết
                    alert("Xóa bài post thành công!");
                    $("#post_" + postId).remove();
                } else {
                    // Xóa bài post thất bại hoặc không tìm thấy bài post, xử lý thông báo lỗi tại đây
                    alert("Xóa bài post không thành công!");
                }
            },
            error: function () {
                // Xử lý lỗi khi gọi Ajax
                alert("Đã xảy ra lỗi khi xóa bài post!");
            }
        });
        modal.hide();
    });
    
};
