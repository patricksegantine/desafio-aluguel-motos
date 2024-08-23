db = db.getSiblingDB('admin');
var databases = db.runCommand({ listDatabases: 1 }).databases;
var dbName = 'motto-db-dev';

var dbExists = databases.some(function (database) {
    return database.name === dbName;
});

if (!dbExists) {
    db = db.getSiblingDB(dbName);
    print('Banco de dados e coleção criados.');
} else {
    print('Banco de dados já existe.');
}
