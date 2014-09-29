Parse.Cloud.beforeSave("FriendshipShared", function(request, response) {
  var query = new Parse.Query(Parse.Object.extend("FriendshipShared"));
  query.equalTo("UserEmail", request.object.get("UserEmail"));
  query.equalTo("FriendUserEmail", request.object.get("FriendUserEmail"));
  query.count({
    success: function(count) {
      if (count == 0) {
        response.success();
      } else {
        response.error("Convite já foi enviado.");
      }
    },
    error: function(error) {
      response.error(error);
    }
  });
});
