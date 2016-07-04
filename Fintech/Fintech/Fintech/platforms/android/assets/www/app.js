var myApp = angular.module('myApp', ["ngRoute"])
  .config(function ($routeProvider) {
    $routeProvider.when('/mycards',
    {
      templateUrl: 'templates/mycards.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/addcard',
    {
      templateUrl: 'templates/addcard.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/home',
    {
      templateUrl: 'templates/home.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/history',
    {
      templateUrl: 'templates/history.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/category/:id',
    {
      templateUrl: 'templates/category.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/products/:categoryId/:productId',
    {
      templateUrl: 'templates/product.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/bag',
    {
      templateUrl: 'templates/bag.html',
      controller: 'bagController'
    });
    $routeProvider.when('/categorys',
    {
      templateUrl: 'templates/categories.html',
      controller: 'cabinetController'
    });
    $routeProvider.when('/shops',
    {
      templateUrl: 'templates/shops.html',
      controller: 'cabinetController'
    });
  });

  myApp.factory('Socket', ['$rootScope', function($rootScope) {
    var connections = {};  // пул соединений, каждое из которых создается по требованию

    function getConnection(channel) {
        if (!connections[channel]) {
            connections[channel] = io.connect('http://localhost:3000/' + channel);
        }

        return connections[channel];
    }

    // При создании нового сокета, он инициализируется namespace-частью строки подключения.
    function Socket(namespace) {
        this.namespace = namespace;
    }

    Socket.prototype.on = function(eventName, callback) {
        var con = getConnection(this.namespace), self = this;  // получение или создание нового соединения
        con.on(eventName, function() {
            var args = arguments;
            $rootScope.$apply(function() { callback.apply(con, args); });
        });
    };

    Socket.prototype.emit = function(eventName, data, callback) {
        var con = getConnection(this.namespace);                   // получение или создание нового соединения.
        con.emit(eventName, data, function() {
            var args = arguments;
            $rootScope.$apply(function() {  if (callback) { callback.apply(con, args); }  });
        })
    };

    return Socket;
}]);
