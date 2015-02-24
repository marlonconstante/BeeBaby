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
  FindByQuery(BuildQuery(name, clauses), callback);
}

var FindByQuery = function(query, callback) {
  query.find({
    success: function(objects) {
      callback(objects);
    },
    error: function(error) {
      callback([], error);
    }
  });
}

var InsertUserDeviceFile = function(file, user, deviceId) {
  BuildObject("UserDeviceFile", {
    DeviceId: deviceId || file.get("DeviceId"),
    DeviceIdOrigin: file.get("DeviceId"),
    UserFile: file,
    User: user
  }).save();
}

var SaveUserMoment = function(file, user) {
  Parse.Cloud.httpRequest({ url: file.url() }).then(function(response) {
    var momentPlan = JSON.parse(response.buffer.toString('utf8'));

    var clauses = {
      MomentId: momentPlan.MomentId,
      User: user
    };

    LoadOrNew("UserMoment", clauses, function(moment, error) {
      if (!error) {
        for (var fieldName in momentPlan) {
          moment.set(fieldName, momentPlan[fieldName]);
        }
        moment.save();
      }
    });
  });
}

var BuildQueryUserDeviceFile = function(user, deviceId, deviceIdEqualTo) {
  var query = new Parse.Query(Parse.Object.extend("UserDeviceFile"));
  query.equalTo("User", user);
  query.notEqualTo("DeviceIdOrigin", deviceId);
  if (deviceIdEqualTo) {
    query.equalTo("DeviceId", deviceId);
  } else {
    query.notEqualTo("DeviceId", deviceId);
  }
  query.limit(1000);
  return query;
}

Parse.Cloud.define("ConfirmReceiptFile", function(request, response) {
  var deviceId = request.params.DeviceId;
  var clauses = {
    objectId: request.params.ObjectId
  };

  LoadOrNew("UserFile", clauses, function(file, error) {
    if (error || file.isNew()) {
      response.success(false);
    } else {
      InsertUserDeviceFile(file, request.user, deviceId);
      response.success(true);
    }
  });
});

Parse.Cloud.define("FindNewFiles", function(request, response) {
  var deviceId = request.params.DeviceId;

  var query = BuildQueryUserDeviceFile(request.user, deviceId, false);
  query.equalTo("SameDeviceId", true);
  query.doesNotMatchKeyInQuery("UserFile", "UserFile", BuildQueryUserDeviceFile(request.user, deviceId, true));
  query.include("UserFile");

  FindByQuery(query, function(deviceFiles, error) {
    var files = [];
    for (var index = 0; index < deviceFiles.length; index++) {
      files.push(deviceFiles[index].get("UserFile"));
    }
    response.success(files);
  });
});

Parse.Cloud.beforeSave("UserDeviceFile", function(request, response) {
  request.object.set("SameDeviceId", request.object.get("DeviceId") == request.object.get("DeviceIdOrigin"));
  response.success();
});

Parse.Cloud.beforeSave("UserFile", function(request, response) {
  request.object.set("User", request.user);

  var clauses = {
    User: request.user,
    DirectoryName: request.object.get("DirectoryName"),
    FileName: request.object.get("FileName")
  };

  LoadOrNew("UserFile", clauses, function(file, error) {
    if (file.isNew()) {
      request.object.set("Version", 1);
      response.success();
    } else {
      Find("UserDeviceFile", { UserFile: file }, function(deviceFiles, error) {
        var promises = [];
        for (var index = 0; index < deviceFiles.length; index++) {
          promises.push(deviceFiles[index].destroy());
        }
        promises.push(file.destroy());

        Parse.Promise.when(promises).then(function() {
          request.object.set("Version", file.get("Version") + 1);
          response.success();
        }, function(error) {
          response.error(error);
        });
      });
    }
  });
});

Parse.Cloud.afterSave("UserFile", function(request) {
  InsertUserDeviceFile(request.object, request.user);

  if (request.object.get("FileName") == "_moment.backup") {
    SaveUserMoment(request.object.get("ParseFile"), request.user);
  }

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
