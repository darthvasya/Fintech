myApp.controller('bagController', function($scope, $http) {
  $scope.ids = localStorage['bag'].split(',');
  var idss =[];
  for (var i = 0; i < $scope.ids.length; i++) {
    idss.push(parseInt($scope.ids[i]));
  }
  console.log(idss);
  $scope.bagFinal = {};
  $scope.marketId = 1;
  $scope.cards = {};
  var id = document.cookie.split('=')[1];

  getCards();
  function getCards() {
    $http.get('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/cards/' + id ).success(function (data) {
      $scope.cards = data;

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
      console.log(data);
    });
  }
});
