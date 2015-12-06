//http://www.cnblogs.com/dolphinX/p/3415761.html
window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;

if (!window.indexedDB) {
    console.log("你的浏览器不支持IndexedDB");
}
var myDB = {
    name: 'test',
    version: 3,
    db: null
};
var IndexedDBHelper = {
    openDB: function (dbname, version) {
        var version = version || 1;
        var request = window.indexedDB.open(dbname, version);
        request.onerror = function (e) {
            console.log(e.currentTarget.error.message);
        };
        request.onsuccess = function (e) {
            myDB.db = e.target.result;
        };
        request.onupgradeneeded = function (e) {
            console.log('DB version changed to ' + version);
        };
    },
    closeDB: function (db) {
        db.close();
    },
    deleteDB: function (dbname) {
        indexedDB.deleteDatabase(dbname);
    }
}


