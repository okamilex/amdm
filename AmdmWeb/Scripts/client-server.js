$(function () {
    var chat = $.connection.amdmHub;    
    chat.client.addMessage = function (name, message) {
        debugger;
        $('#alertDiv').append('<div class="alert alert-success">'+
                                '<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>'+
                                '<strong>' + name +'</strong>' + message + 
                              '</div>'
        );        
    };  
    $.connection.hub.start().done(function () {});
});


