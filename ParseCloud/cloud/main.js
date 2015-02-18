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
  var id = request.object.id;
  if (id) {
    var query = new Parse.Query(Parse.Object.extend("UserFile"));
    query.get(id, {
      success: function(userFile) {
        var version = userFile.get("Version");
        if (request.object.get("Version") == version) {
          request.object.set("Version", version + 1);
          response.success();
        } else {
          response.error("Versão do arquivo " + id + " deveria ser " + version);
        }
      },
      error: function(model, error) {
        response.error("Erro ao carregar arquivo " + id + ". Motivo: " + error.message);
      }
    });
  } else {
    request.object.set("Version", 1);
    response.success();
  }
});

Parse.Cloud.afterSave("UserFile", function(request) {
  var clauses = {
    DeviceId: request.object.get("DeviceId"),
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
    DeviceId: request.object.get("DeviceId")
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
