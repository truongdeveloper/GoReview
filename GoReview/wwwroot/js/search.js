
function btnSearch() {
    var searchString = $("#textSearch").val();
           
    $.ajax({
        url: "/Home/Search", // Đường dẫn đến Action xử lý tìm kiếm
        type: "POST",
        data: { name: searchString }, // Gửi dữ liệu tìm kiếm
        success: function (data) {
            displaySearchResults(data)
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function displaySearchResults(data) {
    var resultsDiv = $("#searchResults");
    resultsDiv.empty();

    if (data != null) {
        $.each(data, function (index, item) {
            var html = `<div class="post-container">`;
            html += `<h3>${item.Title}</h3>`;
            html += `<p>${item.Content}</p>`;
            html += `</div>`;
            resultsDiv.append(html);
        });
    } else {
        resultsDiv.text("Không tìm thấy kết quả.");
    }
}

    //function displaySearchResults(data) {
    //   var resultsDiv = $("#searchResults");
    //    resultsDiv.empty();

    //    if (data != null) {
    //        $.each(data, function (index, item) {
    //           var html = ` <div class="d-flex">
    //                   <div class="post-container">
                          
    //                           <div class="post-row">
    //                               <div class="user-profile">
    //                                    <img src=`+item.user.imageUser+` alt=""/>
    //                                    <div>
    //                                        <p>`+item.User.UserName+`</p>
    //                                        <span>
    //                                           `+item.CreateDate+`
    //                                        </span>
    //                                    </div>
    //                                </div>
    //                          </div>

    //                           <p class="post-text">
    //                               `+item.Content+`
    //                           </p>
    //                           <img src="`+item.Picture+`" class="post-img" alt="">

    //                           <div class="post-row">

    //                               <div class="activity-icons">

    //                                    <div><img src="/images/like-blue.png" alt=""> `+item.NumLike+`</div>
    //                             </div>
    //                          </div>

    //                            <hr />
    //                   </div>

    //               </div>`;
    //          resultsDiv.append(html);
    //        });
    //   } else {
    //       resultsDiv.text("Không tìm thấy kết quả.");
    //    }
    //}