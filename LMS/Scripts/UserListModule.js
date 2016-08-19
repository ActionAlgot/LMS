(function () {
	var app = angular.module('UserListModule', []);

	app.controller('UserListController', ["$scope", function ($scope) {
		$scope.tag;
		$scope.UserList = [];
		$scope.TableAppends = [];	//raw html list
		$scope.Funcs = {};		//funcs that will be called from TableAppends

		$scope.GetShit = function () {
			var response = $scope.PostShit($scope.tag);
			$scope.UserList = response.UserList;
			$scope.TableAppends = response.TableAppends;
			$scope.Funcs = response.Funcs;
		};
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