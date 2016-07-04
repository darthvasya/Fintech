myApp.controller('cabinetController', function ($scope, $http, $routeParams, Socket) {

  if(localStorage['shop'] != undefined)
    $scope.marketId = localStorage['shop'];
  else {
    $scope.marketId = 3;
  }
  console.log("ID " + localStorage['shop']);
  $scope.shop = {};
  var idUser = localStorage['id'];
  $scope.cards = {};
  $scope.categories = {};
  $scope.products = {};
  $scope.categoryId = {};
  $scope.product = {};
  $scope.bag = [];



  if($scope.marketId <= 0) {
    location.href = "#/shops";
  }
  getCategories();
  function getCategories() {
    $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/Shops/' + $scope.marketId + '/Categories/').success(function (data) {
      $scope.categories = data;
    });
  }

  getCards();
  function getCards() {
    $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/cards/' + idUser ).success(function (data) {
      $scope.cards = data;
    });
  }

  getShop();
  function getShop() {
    $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/Shops/' + $scope.marketId).success(function(data) {
      $scope.shop = data;
    });
  }

  $scope.addcard = function (card, cardForm) {

    var send = {
      "uid": Number(idUser),
      "cardNumber": card.cardNumber,
      "validDate": card.validDate,
      "cardKey":  card.cardKey
    }
    if(cardForm.$valid) {
        console.log(send);
        $http.post('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/AddCard/', send).success(function (data) {
          console.log(data);
        });
    }
  }

  $scope.IDProd = 0;
  $scope.$on("$routeChangeSuccess", function () {
    var id = $routeParams["id"];
    var productId = $routeParams["productId"];
    var categoryId = $routeParams["categoryId"];
    $scope.IDProd = productId;

    if(id!=='undefined'){
      console.log(id);
      $scope.categoryId = id;
      $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/Shops/' + $scope.marketId  + '/Categories/' +  id + '/').success(function (data) {
        $scope.products = data;
      });
    }

    if(productId !== 'undefined') {
        $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/Shops/' + $scope.marketId + '/Categories/' + categoryId + '/Goods/'+productId+'/').success(function (data) {
          $scope.product = data;
          console.log(data);
        });
    }
  });
//ShopId
//CardId
//GoodsId
//UserId
//OrderTime

$scope.addToBag = function() {
  console.log(2);
  $scope.bag.push($scope.IDProd);
  if(localStorage['bag'] != undefined)
    var buf = localStorage['bag'] + ",";
  else {
    var buf = "";
  }
  localStorage.setItem('bag', buf + $scope.IDProd);
  console.log(localStorage['bag']);
}


});
