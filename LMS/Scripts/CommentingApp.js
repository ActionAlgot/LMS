(function () {
	var app = angular.module("CommentingApp", []);


	app.controller("ListCommentingController", ["$scope", function ($scope) {
		$scope.CommentList = [];
		$scope._OnComment = [
			function () {
				return function (commentText) {
					$scope.CommentList.unshift(commentText);
				}
			}
		];
	}]);

	app.controller("CommentingController", ["$scope", "$http", function ($scope, $http) {
		$scope.LatesCommentText = "";
		$scope.OnComment = [];

		$scope.Apply_OnComment = function (source) {
			for (var i in source)
				$scope.OnComment[i] = source[i]();
			console.log($scope.OnComment);
		}

		$scope.SubmitComment = function (comment) {
			$http.post("/Comment/Create", comment)
			.then(
				function Success(response) {
					$scope.LatestCommentText = comment.Text;
					for (var i in $scope.OnComment)
						$scope.OnComment[i]($scope.Comment.Text);
					$scope.Comment.Text = "";
				},
				function Error(response) { }
			);
		};

		
	}]);
}());