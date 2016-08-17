(function () {
	var app = angular.module('KlassApp', []);

	app.controller('KlassMemberTable', ["$scope", "$http", function ($scope, $http) {
		$scope.KlassId = null;
		$scope.TableAppends = [];
		$scope.alert = function (shit) { alert(shit); };

		$scope.AddMember = function (UId) {
			$http.get("../AddKlassMember?Id=" + $scope.KlassId + "&UId=" + UId)
			.then(function Success(response) {
				//do UX reloading shit
			}, function Error(response) {
				console.log(response);
			});
		}

		$scope.RemoveMember = function (UId) {
			$http.get("../RemoveKlassMember?Id=" + $scope.KlassId + "&UId=" + UId)
			.then(function Success(response) {
				//do UX reloading shit
			}, function Error(response) {
				alert(response);
			});
		}

		$scope.GetData = function (url, receiver) {
			$http.get("../" + url)
			.then(function Success(response) {
				receiver.data = response.data;
			}, function Error(response) {
				console.log(response);
			});
		}
	}]);

	app.directive("bindCompiledHtml", function ($compile, $timeout) {
		return {
			template: '<td></td>',
			scope: {
				rawHtml: '=bindCompiledHtml'
			},
			link: function (scope, elem, attrs) {
				scope.$watch('rawHtml', function (value) {
					if (!value) return;
					var newElem = $compile(value)(scope.$parent);
					elem.contents().remove();
					elem.append(newElem);
				});
			}
		};
	});
}());