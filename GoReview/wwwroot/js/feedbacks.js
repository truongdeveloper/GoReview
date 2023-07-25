$(document).on('submit', '#commentForm', function (event) {
    event.preventDefault(); // Ngăn chặn submit form thông qua trình duyệt


    var postId = $(this).find('input[name="postId"]').val();
    // Lấy thông tin bình luận từ form
    var formData = $(this).serialize();
    // Gửi yêu cầu Ajax đến action "AddComment" trong PostsController
    console.log({ formData })
    $.ajax({
        type: 'POST',
        url: '/Feedbacks/AddComment',
        data: formData,
        success: function (response) {
            // Hiển thị kết quả bình luận trả về từ action
            var createComment = createCommentElement(response);
            $("#commentResult_" + postId).append(createComment);

            // Xóa nội dung trong textarea sau khi gửi bình luận thành công
            $('#comment_' + postId).val('');
        },
        error: function () {
            alert('Có lỗi xảy ra khi gửi bình luận.');
        }
    });
});

function createCommentElement(comment) {
    console.log(comment);
    var commentHtml = `
            <div class="comment mb-3" style="margin-right: 20%">
                <div class="row">
                    <div class="col-md-1 user-profile">
                        <img src="/images/${comment.imageUser}" class="" alt="...">
                    </div>
                    <div class="col-md-10">
                        <div class="card-body">
                            <h5 class="card-title">${comment.userName}</h5>
                            <p class="card-text">${comment.comment}</p>
                        </div>
                    </div>
                </div>
            </div>
        `;
    return $(commentHtml); // Trả về phần tử HTML dưới dạng jQuery object
}

$(document).on('click', '#btn-like', function () {
    var postId = $(this).closest(".post").data("postid");
    console.log(postId);
    var isLikeAction = false;
    if (!isLikeAction) {
        isLikeAction = true;
        $.ajax({
            type: "POST",
            url: "/Feedbacks/LikeAction",
            data: { id: postId }, // Truyền tham số ID bài post vào đây
            success: function (data) {
                console.log(data)
                if (data.isLiked) {
                    // Thực hiện các tác vụ khi like thành công
                    alert("Like thành công!");
                    location.reload();
                } else {
                    // Thực hiện các tác vụ khi unlike thành công
                    alert("Unlike thành công!");
                    location.reload();
                }
                isLikeAction = false; // Đánh dấu là đã hoàn tất hành động like/unlike
            },
            error: function () {
                // Xử lý lỗi khi gọi Ajax
                alert("Đã xảy ra lỗi khi thực hiện hành động like/unlike!");
                isLikeAction = false; // Đánh dấu là đã hoàn tất hành động like/unlike (dù có lỗi hay không)
            }
        });
    }
});

$(document).on('click', '#btn-report', function () {
    var postId = $(this).closest(".post").data("postid");
    console.log(postId);
    var isLikeAction = false;
    if (!isLikeAction) {
        isLikeAction = true;
        $.ajax({
            type: "POST",
            url: "/Feedbacks/ReportAction",
            data: { id: postId }, // Truyền tham số ID bài post vào đây
            success: function (data) {
                console.log(data)
                if (data.isLiked) {
                    // Thực hiện các tác vụ khi like thành công
                    alert("Report thanh cong");
                    location.reload();
                } else {
                    // Thực hiện các tác vụ khi unlike thành công
                    alert("Report thanh cong");
                    location.reload();
                }
                isLikeAction = false; // Đánh dấu là đã hoàn tất hành động like/unlike
            },
            error: function () {
                // Xử lý lỗi khi gọi Ajax
                alert("Đã xảy ra lỗi khi thực hiện hành động Report");
                isLikeAction = false; // Đánh dấu là đã hoàn tất hành động like/unlike (dù có lỗi hay không)
            }
        });
    }
});

