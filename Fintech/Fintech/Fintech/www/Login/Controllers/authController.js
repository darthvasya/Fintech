myApp.controller('authController', function ($scope, $http, $location) {
  console.log(localStorage.getItem('id'));
  var idUSer = localStorage.getItem('id');
  $scope.message = idUSer;
  console.log(idUSer);
  $scope.isRegister = false
  if(idUSer != undefined) {
    $scope.isRegister = true;
  }
  $scope.register = function (answer, answerForm) {
    if(answerForm.$valid) {
      console.log(answer);
      $http.post('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/register/', answer).success(function(data) {
        if(data != -1) {
          localStorage['id'] = data;
          console.log(localStorage.getItem('id'));
          console.log("Regisxter OK");
          $scope.message = "Register OK " + localStorage.getItem('id');
          $scope.isRegister = true;
        } else {
          console.log("Error with register");
          $scope.message = "Register NO, ERROR"
        }
      });
    }
  }

  $scope.login = function(enter, enterForm) {
    if(enterForm.$valid) {
      //$scope.enter.
      var id = localStorage.getItem('id');
      console.log(id);
      var send = {
        "id": id,
        "password": $scope.enter.password
      };
      console.log(send);
      $http.post('http://vasya18-001-site1.dtempurl.com/CustomerService.svc/login/', send).success(function(data) {
        if(data == true) {
          console.log("Logged");
          location.href = ' Cabinet/#/home';
        } else {
          console.log("Error with logging");

        }
      });
    }
  }
});
