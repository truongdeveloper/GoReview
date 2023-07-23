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