// function format date dd/MM/yyyy hh:mm
function formatDate(date) {
    var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear(),
        hour = d.getHours(),
        minute = d.getMinutes();
    if (month.length < 2) month = "0" + month;
    if (day.length < 2) day = "0" + day;
    if (hour.length < 2) hour = "0" + hour;
    if (minute.length < 2) minute = "0" + minute;
    return [day, month, year].join("/") + " " + [hour, minute].join(":");
}

function createNewPost(post, email) {
    return `<div class="card col-md-4 m-2 ${!post.publishStatus ? 'card-opacity':''}" style="width: 18rem; ${!post.publishStatus && currentId != post.authorID ? 'display: none;' : 'display:block;'}" id="post-${post.postID}" >
    <div class="card-body">
        <h5 class="card-title"> ${post.publishStatus ? post.title : post.title + '(hidden)'} </h5>
        <a href="javascript:void(0)" class="card-subtitle mb-2 text-muted">${email}</a>
        <p class="card-text my-2">${post.content}</p>
        <p class="card-link">${formatDate(post.updatedDate)}</p>
        <div>
             ${currentEmail == email ? `<a href="./Post/Edit?id=${post.postID}" class="mr-2 text-primary">Edit</a> <a href="./Post/Delete?id=${post.postID}" class="mr-2 text-danger" > Delete</a >` : ``}
        </div>
    </div>
</div>`;
}
var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
connection
    .start()
    .then(function () {
        console.log("SignalR Started...");
    })
    .catch(function (err) {
        return console.error(err);
    });

connection.on("CreatePost", function (post, email) {

    $("#post-list").prepend(createNewPost(post, email));
    $.toast({
        heading: "Create post",
        text: `Post ${post.title} has been updated by ${email}`,
        icon: "info",
        loader: true, // Change it to false to disable loader
        loaderBg: "#9EC600", // To change the background
    });
});

connection.on("UpdatePost", function (post, email) {
    console.log(post);
    let postId = post.postID;
    if ($(`#post-${postId}`).length) {
        var elem = document.getElementById(`post-${postId}`);
        if (post.publishStatus == true || currentEmail == email) {
            // elem.style = 'width: 18rem; display: block';
            elem.style.display = 'block';

            
        }
        else {
            //  elem.style = 'width: 18rem; display: none';
            elem.style.display = 'none';
        }
        let content = $(`#post-${postId} .card-text`);
        content.text(post.content);
        let title = $(`#post-${postId} .card-title`);
        if (post.publishStatus == false && currentEmail == email) {
            
            elem.classList.add('card-opacity');
            title.text(post.title + '(hidden)');
        }
        else {
            elem.classList.remove('card-opacity');
            title.text(post.title);
        }
        
        
        let date = $(`#post-${postId} .card-link`);
        date.text(formatDate(post.updatedDate));
        
        
    }
    $.toast({
        heading: "Update post",
        text: `Post ${post.title} has been updated by ${email}`,
        icon: "info",
        loader: true, // Change it to false to disable loader
        loaderBg: "#9EC600", // To change the background
    });
});

connection.on("DeletePost", function (post, email) {
    let postId = post.postID;
    if ($(`#post-${postId}`).length) {
        $(`#post-${postId}`).remove();
    }
    $.toast({
        heading: "Delete post",
        text: `Post ${post.title} has been Deleted by ${email}`,
        icon: "info",
        loader: true, // Change it to false to disable loader
        loaderBg: "#9EC600", // To change the background
    });
});
