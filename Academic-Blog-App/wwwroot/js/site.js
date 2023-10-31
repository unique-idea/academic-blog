
$(function () {

    var reportCommentId;
    var commentData;
    var Reportform = $("#reportModel");
    var newCommentForm = $("#formInputNewComment");
    Reportform.submit(function (event) {
        event.preventDefault();
        reportCommentId = $("#reportModelCommentId", Reportform).val();
        console.log("Report Comment Id Fetched: " + reportCommentId);
        ReportComment(reportCommentId);
        return false;
    });
    newCommentForm.submit(function (event) {
        event.preventDefault();
        commentData = $("#inputComment", newCommentForm).val();
        console.log("New Comment : " + newCommentForm);
        CreateNewComment(commentData);
        return false;
    });
    $(document).on("click", ".feedback-btn", function () {
        var commentId = $(this).data("comment-id");
        var spaddingInt = $(this).data("spadding-int");

        console.log("Spadding Int Fetched: " + spaddingInt);
        console.log('Comment Id Fetched: ' + commentId);
        GetReplys(commentId, spaddingInt);
        return false;
    });

    $(document).on("click", ".submit-reply-btn", function () {
        var commentId = $(this).data("reply-comment-id");
        var spaddingInt = $(this).data("spadding-int");
        var currentFormReply = $('#replyContainer-' + commentId);
        if (currentFormReply.is(':visible')) {
            $('#replyContainer-' + commentId).hide();
        } 
        console.log("Spadding Int Fetched: " + spaddingInt);
        console.log('Comment Id Reply Fetched: ' + commentId);
        var replyComment = document.getElementById('inputReplyComment-' + commentId).value;
        console.log(replyComment);
        CreateReply(commentId, replyComment, spaddingInt);
        return false;
    });

    var connection = new signalR.HubConnectionBuilder().withUrl("/CenterHub").build();
    connection.start()
        .then(function () {
            console.log('Hub connection started');
        })
        .catch(function (err) {
            console.error('Error while starting Hub connection: ' + err.toString());
        });
    connection.on("TrackPost", function (blogId) {
        console.log("Post tracked : " + blogId);
        TrackPost(blogId);
    })
    connection.on("GetBlogId", function () {
        console.log("Get Blog Connected");
        GetBlogId();
    })
    GetBlogId();

    connection.on("ReportComment", function (reportCommentId) {
        console.log("Report Blog : " + reportCommentId);
        ReportComment(reportCommentId);
    })
    connection.on("GetReplys", function (commentId){
        console.log("Get Replys Connected");
        GetReplys(commentId);
    })
    connection.on("CreateReply", function (commentId, replyComment, spaddingInt) {
        console.log("Create Reply Connected");
        CreateReply(commentId, replyComment, spaddingInt);
    })

    connection.on("CreateNewComment", function (commentData) {
        console.log("Create new comment connected");
        CreateNewComment(commentData);
    })
 
    function CreateNewComment(commentData) {
        console.log("Create New Comment Running Inside: " + commentData);
        $.ajax({
            url: '/AjaxPage/AjaxHandel?handler=CreateNewComment',
            method: 'GET',
            data: { commentData: commentData},
            success: function (result) {

                var account = result.account;
                var comment = result.comment;
                var login = result.login;
                console.log("Account: " + result.account);
                console.log("Comment: " + result.comment);

                var commentorHtml =
                    `<div class="comment-container">
                        <div class="commentor row" style="padding-left: 210px; clear: both; height: auto; width:720px;">
                            <div class="commentor-infor" style="height: auto; width: 500px; border-radius: 10px; margin: 10px; border: 1px solid rgba(0, 0, 0, 0.5); background-color:  rgba(128, 128, 128, 0.5);">
                                <span class="avatar">
                                    <img class="rounded-circle shadow" src="/${account}" alt="" style="width: 50px; height: 50px; float: left; margin-right: 30px; margin-top: 5px;" />
                                </span>
                                <div style="font-weight: 700; font-size: 24px; ">
                                    tonySta
                                </div>
                                <div style="color: black; opacity: 80%;">
                                     ${comment.content}
                                </div>
                            </div>
                        </div>
                        <div class="commentor-interact row" style="margin-left: 210px; clear: both; color: black; opacity: 80%;">`;
                if (login === true) {
                    commentorHtml += ` <button type="button" style="display: inline-block; margin-right: 10px; color: yellow; margin-bottom: 10px; font-size: 18px" id="report-${comment.id}" onclick="openReport('${comment.id}')">Report</button>`;
                }
                commentorHtml += `
                            <div id="formReply-${comment.id}" class="form-reply" style="display: inline-block; margin-right: 10px;">
                                <form method="get">
                                    <div>
                                        <button class="feedback-btn" id="feedback-${comment.id}" data-comment-id="${comment.id}" data-spadding-int="210" type="submit" style="color: green; font-size: 17px;">0 FeedBack</button>
                                    </div>
                                </form>
                            </div>`;
                if (login === true) {
                    commentorHtml += `<button class="reply-btn" type="button" style="display: inline-block; color: black; margin-bottom: 10px; font-size: 17px;" id="reply-${comment.id}" onclick="openReply('${comment.id}')">Reply</button>`;
                }
                commentorHtml += `    
                        </div>
                    </div>
                    <div class="comment-container" id="${comment.id}" style=""></div>
                     <div class="reply-container" id="replyContainer-${comment.id}" style="height: auto; width: 590px; margin-left: 255px; position: relative; display: none;">
                            <input type="text" id="inputReplyComment-${comment.id}" name="inputReplyComment" required maxlength="200" pattern=".*[^ ].*" placeholder="Enter Reply" style="height: 55px; width: 440px; border-radius: 10px; background-color: gainsboro; border: 1px solid; " />
                            <form method="get">
                                <button type="submit" class="submit-reply-btn" data-reply-comment-id="${comment.id}" data-spadding-int="255" style="color: green; font-size: 17px;">
                                    Submit
                                </button>
                            </form>
                        </div>`;
                $("#newComment").prepend(commentorHtml);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    function CreateReply(commentId, replyComment, spaddingInt) {
        console.log("Create Reply Running Inside: " + commentId);

        var currentCommentTag = document.getElementById("blogComment").textContent;
        var currentFeedBackTag = document.getElementById("feedback-" + commentId).textContent;

        var modifiedString = currentCommentTag.replace(new RegExp("Comments", "g"), "");
        var trimModifiedString = modifiedString.trim();

        var [feedBackNumber, feedBackText] = currentFeedBackTag.split(" ");

        var currentComment = parseInt(trimModifiedString);
        var currentFeedBack = parseInt(feedBackNumber);
        $.ajax({
            url: '/AjaxPage/AjaxHandel?handler=CreateReply',
            method: 'GET',
            data: { commentId: commentId, replyComment: replyComment, spaddingInt: spaddingInt },
            success: function (result) {

                currentComment += 1;
                currentFeedBack += 1;
                $("#blogComment").text(currentComment + " Comments");
                $("#feedback-" + commentId).text(currentFeedBack + " " + feedBackText);

                var account = result.account;
                var comment = result.comment;
                var distance = result.distance;
                var login = result.login;
                console.log("Account: " + result.account);
                console.log("Comment: " + result.comment);
                console.log("Distances: " + result.distance);
                console.log("Login: " + result.login);
                            var commentorHtml =
                                `<div class="comment-container">
                        <div class="commentor row" style="padding-left: ${spaddingInt}px; clear: both; height: auto; width:720px;">
                            <div class="commentor-infor" style="height: auto; width: 500px; border-radius: 10px; margin: 10px; border: 1px solid rgba(0, 0, 0, 0.5); background-color:  rgba(128, 128, 128, 0.5);">
                                <span class="avatar">
                                    <img class="rounded-circle shadow" src="/${account.avatar}" alt="" style="width: 50px; height: 50px; float: left; margin-right: 30px; margin-top: 5px;" />
                                </span>
                                <div style="font-weight: 700; font-size: 24px; ">
                                    tonySta
                                </div>
                                <div style="color: black; opacity: 80%;">
                                     ${comment.content}
                                </div>
                            </div>
                        </div>
                        <div class="commentor-interact row" style="margin-left: ${spaddingInt}px; clear: both; color: black; opacity: 80%;">`;
                if (login === true) {
                    commentorHtml += ` <button type="button" style="display: inline-block; margin-right: 10px; color: yellow; margin-bottom: 10px; font-size: 18px" id="report-${comment.id}" onclick="openReport('${comment.id}')">Report</button>`;
                }
                commentorHtml += `
                            <div id="formReply-${comment.id}" class="form-reply" style="display: inline-block; margin-right: 10px;">
                                <form method="get">
                                    <div>
                                        <button class="feedback-btn" id="feedback-${comment.id}" data-comment-id="${comment.id}" data-spadding-int="${spaddingInt}" type="submit" style="color: green; font-size: 17px;">0 FeedBack</button>
                                    </div>
                                </form>
                            </div>`;
                if (login === true) {
                    commentorHtml += `<button class="reply-btn" type="button" style="display: inline-block; color: black; margin-bottom: 10px; font-size: 17px;" id="reply-${comment.id}" onclick="openReply('${comment.id}')">Reply</button>`;
                }
                commentorHtml += `    
                        </div>
                    </div>
                    <div class="comment-container" id="${comment.id}" style=""></div>
                     <div class="reply-container" id="replyContainer-${comment.id}" style="height: auto; width: 590px; margin-left: ${distance}px; position: relative; display: none;">
                            <input type="text" id="inputReplyComment-${comment.id}" name="inputReplyComment" required maxlength="200" pattern=".*[^ ].*" placeholder="Enter Reply" style="height: 55px; width: 440px; border-radius: 10px; background-color: gainsboro; border: 1px solid; " />
                            <form method="get">
                                <button type="submit" class="submit-reply-btn" data-reply-comment-id="${comment.id}" data-spadding-int="${distance}" style="color: green; font-size: 17px;">
                                    Submit
                                </button>
                            </form>
                        </div>`;
                $("#" + commentId).prepend(commentorHtml);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function ReportComment(reportCommentId) {
        $.ajax({
            url: '/AjaxPage/AjaxHandel?handler=Report',
            method: 'GET',
            data: { reportCommentId: reportCommentId },
            success: function (result) {
                if (result.success) {
                    alert("Report Success");
                } else {
                    alert("Report Failed Try Back Later");
                }
            },
            error: (error) => {
                console.log(error);
            }
        })
    }

    function GetReplys(commentId, spaddingInt) {
        console.log("Get Replys Running Inside: " + commentId);
        var nextCommentId = '';
        $.ajax({
            url: '/AjaxPage/AjaxHandel?handler=Reply',
            method: 'GET',
            data: { commentId: commentId, distanceInt: spaddingInt },
            success: function (result) {
                var totalComentorHtml = '';
                var totalFeedBack = '0';
                var accountReplys = result.accountReplys;
                var replys = result.replys;
                var distance = result.distance;
                var feedBacks = result.feedBacks;
                var login = result.login;
                var distanceForReply = result.distanceForReply;
                console.log("Account Replys: " + result.accountReplys);
                console.log("Replys: " + result.replys);
                console.log("Distances: " + result.distance);
                console.log("distanceForReply: " + result.distanceForReply);
                console.log("Login: " + result.login);
                $.each(replys, function (index, reply) {
                    $.each(feedBacks, function (index, feed) {
                        var [id, number] = feed.split(":");
                        if (id == reply.id) {
                            totalFeedBack = number;
                        }
                    });
                    $.each(accountReplys, function (index, acc) {
                        if (reply.commentorId === acc.id) {                       
                         
                                    nextCommentId = reply.id;
                                    console.log("Next CommentId: " + nextCommentId);
                            var commentorHtml = 
                                `<div class="comment-container">
                        <div class="commentor row" style="padding-left: ${distance}px; clear: both; height: auto; width:720px;">
                            <div class="commentor-infor" style="height: auto; width: 500px; border-radius: 10px; margin: 10px; border: 1px solid rgba(0, 0, 0, 0.5); background-color:  rgba(128, 128, 128, 0.5);">
                                <span class="avatar">
                                    <img class="rounded-circle shadow" src="/${acc.avatar}" alt="" style="width: 50px; height: 50px; float: left; margin-right: 30px; margin-top: 5px;" />
                                </span>
                                <div style="font-weight: 700; font-size: 24px; ">
                                    ${acc.name}
                                </div>
                                <div style="color: black; opacity: 80%;">
                                     ${reply.content}
                                </div>
                            </div>
                        </div>
                        <div class="commentor-interact row" style="margin-left: ${distance}px; clear: both; color: black; opacity: 80%;"> `;
                            if (login === true) {
                                commentorHtml += `  <button type="button" style="display: inline-block; margin-right: 10px; color: yellow; margin-bottom: 10px; font-size: 18px" id="report-${reply.id}" onclick="openReport('${reply.id}')">Report</button>`;
                            }
                            commentorHtml += `                        
                            <div id="formReply-${reply.id}" class="form-reply" style="display: inline-block; margin-right: 10px;">
                                <form method="get">
                                    <div>
                                        <button class="feedback-btn" id="feedback-${reply.id}" data-comment-id="${reply.id}" data-spadding-int="${distance}" type="submit" style="color: green; font-size: 17px;">${totalFeedBack} FeedBack</button>
                                    </div>
                                </form>
                            </div> `;
                            if (login === true) {
                                commentorHtml += `  <button class="reply-btn" type="button" style="display: inline-block; color: black; margin-bottom: 10px; font-size: 17px;" id="reply-${reply.id}" onclick="openReply('${reply.id}')">Reply</button>`;
                            }                      
                            commentorHtml += `                            
                        </div>
                    </div>
                    <div class="comment-container" id="${reply.id}" style=""></div>
                     <div class="reply-container" id="replyContainer-${reply.id}" style="height: auto; width: 590px; margin-left: ${distanceForReply}px; position: relative; display: none;">
                            <input type="text" id="inputReplyComment-${reply.id}" name="inputReplyComment" required maxlength="200" pattern=".*[^ ].*" placeholder="Enter Reply" style="height: 55px; width: 440px; border-radius: 10px; background-color: gainsboro; border: 1px solid; " />
                            <form method="get">
                                <button type="submit" class="submit-reply-btn" data-reply-comment-id="${reply.id}" data-spadding-int="${distanceForReply}" style="color: green; font-size: 17px;">
                                    Submit
                                </button>
                            </form>
                        </div>`;
                        totalComentorHtml += commentorHtml;
                        }           
                    });
                });
                $("#" + commentId).html(totalComentorHtml);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function GetBlogId() {
        console.log("Get Blog Id Running Inside");
        $.ajax({
            url: '/AjaxPage/AjaxHandel?handler=BlogId',
            method: 'GET',
            success: function (result) {

                var blogId = result.blogId;
                console.log("Get Blog id return: " + blogId);
                TrackPost(blogId);
            },
            error: function (error) {
                console.log('Error occurred during GetBlogId request:', error);
            }
        })
    }
    function TrackPost(blogId) {
        console.log("Track Blog running inside : " + blogId);
        var currentViewTag = document.getElementById("blogView").textContent;
        var currentView = parseInt(currentViewTag);
        console.log("Current View: " + currentView);
        GenerateFingerprint(function (fingerprint) {
            console.log('Fingerprin: ' + fingerprint);
            console.log('Blog Id: ' + blogId)
            $.ajax({
                url: '/AjaxPage/AjaxHandel?handler=TrackBlog',
                type: 'GET',
                data: {
                    blogId: blogId,
                    fingerprint: fingerprint
                },
                success: function (result) {
                    if (result.success) {
                        console.log('View Counted');
   
                        currentView += 1;
                        var newBlogView = `<i class="fa fa-eye"  style="font-size:30px;color:black">${currentView}</i>`; 
                        $("#blogView").html(newBlogView);
                    } else {
                        console.log('View UnCounted');
                    }
                },
                error: function (error) {
                    console.log('Error occurred during tracking:', error);
                }
            });
        });
    }

    function GenerateFingerprint(callback) {
        Fingerprint2.get(function (components) {
            var values = components.map(function (component) { return component.value });
            var fingerprint = Fingerprint2.x64hash128(values.join(''), 31);
            callback(fingerprint);
        });
    }
});
