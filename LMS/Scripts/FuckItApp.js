(function () {
	//everything here is horrible because I was too busy
	//having fun doing stupid shit to do proper modules
	//so now this is a bucnh of inane crap
	app = angular.module('FuckItApp', ['UserListModule']);

	app.controller('BasicShitController', ['$scope', '$http', function ($scope, $http) {
		$scope.PostShit = function (tag) {
			for (i in taggedShit) if (taggedShit[i].tag == tag)
				return taggedShit[i];
		};
		$scope.TableAppendages = [];

		var fuckWhatever = { data: [] };

		$scope.GetData = function (url) {
			$http.get(url)
			.then(function Success(response) {
				fuckWhatever.data = response.data;
			}, function Error(response) {
				console.log(response);
			});
		}

		var taggedShit = [{
				tag: "Whatever",
				UserList: fuckWhatever,
				TableAppends: $scope.TableAppendages,
				Funcs: { }
			}]

	}]);
}());