var BuildQuery = function(name, clauses) {
  var query = new Parse.Query(Parse.Object.extend(name));
  for (var fieldName in clauses) {
    query.equalTo(fieldName, clauses[fieldName]);
  }
  return query;
}

var BuildObject = function(name, values) {
  var ParseObject = Parse.Object.extend(name);
  var object = new ParseObject();
  for (var fieldName in values) {
    object.set(fieldName, values[fieldName]);
  }
  return object;
}

var LoadOrNew = function(name, clauses, callback) {
  var newObject = BuildObject(name, clauses);
  BuildQuery(name, clauses).first({
    success: function(object) {
      callback(object || newObject);
    },
    error: function(error) {
      callback(newObject, error);
    }
  });
}

var Find = function(name, clauses, callback) {
  BuildQuery(name, clauses).find({
    success: function(objects) {
      callback(objects);
    },
    error: function(error) {
      callback([], error);
    }
  });
}

Parse.Cloud.beforeSave("UserFile", function(request, response) {
  request.object.set("User", request.user);
  request.object.set("Version", 1);
  response.success();
});

Parse.Cloud.afterSave("UserFile", function(request) {
  BuildObject("UserDeviceFile", {
    DeviceId: request.object.get("DeviceId"),
    DeviceIdOrigin: request.object.get("DeviceId"),
    UserFile: request.object,
    User: request.user
  }).save();

  var clauses = {
    User: request.user,
    DirectoryName: request.object.get("DirectoryName")
  };

  Find("UserFile", clauses, function(files, error) {
    if (!error) {
      var size = 0;
      var version = 0;
      for (var index = 0; index < files.length; index++) {
        var file = files[index];
        size += file.get("Size");
        version += file.get("Version");
      }

      LoadOrNew("UserFolder", clauses, function(folder, error) {
        if (!error) {
          folder.set("Size", size);
          folder.set("Version", version);
          folder.set("FilesLength", files.length);
          folder.save();
        }
      });
    }
  });
});

Parse.Cloud.afterSave("UserFolder", function(request) {
  var clauses = {
    User: request.user
  };

  Find("UserFolder", clauses, function(folders, error) {
    if (!error) {
      var size = 0;
      var version = 0;
      var filesLength = 0;
      for (var index = 0; index < folders.length; index++) {
        var folder = folders[index];
        size += folder.get("Size");
        version += folder.get("Version");
        filesLength += folder.get("FilesLength");
      }

      LoadOrNew("UserAccount", clauses, function(account, error) {
        if (!error) {
          account.set("Size", size);
          account.set("Version", version);
          account.set("FilesLength", filesLength);
          account.set("FoldersLength", folders.length);
          account.save();
        }
      });
    }
  });
});
