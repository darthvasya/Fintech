myApp.controller('bagController', function($scope, $http) {
  $scope.ids = localStorage['bag'].split(',');
  var idss =[];
  for (var i = 0; i < $scope.ids.length; i++) {
    idss.push(parseInt($scope.ids[i]));
  }
  console.log(idss);
  $scope.bagFinal = {};
  if(localStorage.getItem('shop') != undefined)
    $scope.marketId = localStorage.getItem('shop');
  else {
    $scope.marketId = 3;
  }
  $scope.cards = {};
  var id = localStorage['id'];

  getCards();
  function getCards() {
    console.log("ID " + id);
    $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/cards/' + id ).success(function (data) {
      $scope.cards = data;
      console.log(data);
    });
  }

  $scope.makeOrder = function() {
    var order = {
      "ShopId": $scope.marketId,
      "CardId": $scope.cards[0].id,
      "GoodsId": idss,
      "UserId": parseInt(id),
      "OrderTime": new Date().getTime()
    }
    // var order ={
    //   "ShopId": 1,
    //   "GoodsId": [1, 2],
    //   "CardId": 1,
    //   "OrderTime": "ggg",
    //   "UserId": 1
    //   }
    console.log(order);
    $http.post('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/MakeOrder/', order).success(function (data) {
      localStorage.setItem("key", data);
      console.log(data);
    });
  }

  $scope.orderInfo = {};
  $http.post('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/ordersInfo/', idss).success(function(data) {
    $scope.orderInfo = data;
    console.log(data);
  });


});
