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
  });
