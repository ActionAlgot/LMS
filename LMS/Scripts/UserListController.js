(function () {
	var app = angular.module('UserListApp', []);

	app.controller('UserListController', [$scope, function ($scope) {
		$scope.tag;
		$scope.TableAppends = [];	//raw html list
		$scope.TableFuncs = {};		//funcs that will be called from TableAppends

		$scope.init = function () {
			var response = $scope.PostShit($scope.tag);
			$scope.TableAppends = response.tableAppends;
			$scope.TableFuncs = response.funcs;
		};
	}]);

	app.service('UserListService', function () {

	});

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