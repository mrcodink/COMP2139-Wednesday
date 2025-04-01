function loadComments(projectId){
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectId=' + projectId,
        method: 'GET',
        success: function(data){
            var commentsHtml = '';
            for(var i = 0; i < data.length > 0; i++){
                commentsHtml += '<div class="comments">';
                commentsHtml += '<p>' + data[i].Content + '</p>';
                commentsHtml += '<span>Posted On:' + new Date(data[i].DatePosted).toLocaleDateString() + '</span>';
                commentsHtml += '</div>';
            }
            $('#commentList').html(commentsHtml);
        }
    })
    
}

$(document).ready(function(){
    
    //loadComments - call GetComments
    var projectId = $('#projectComments input[name="projectId"]').val();
    loadComments(projectId);
    
    // Submit event for new comment (AddComment)
    $('#addCommentForm').submit(function(evt){
        
        //stop default form submission
        evt.preventDefault();
        
        var formData = {
            ProjectId: projectId,
            Content: $('#projectComments textarea[name="Content"]').val()
        };
        
        
        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response){
                
                if(response.success){
                    $('#projectComments textarea[name="Content"]').val(''); // clear new comment from form textarea
                    loadComments(projectId); // reload comments after adding a new one
                } else {
                    alert(response.message);
                    
                }
            },
            error: function (xhr, status, error){
                alert('Error: ' + xhr.responseText);
            }
        })
    });
    
});