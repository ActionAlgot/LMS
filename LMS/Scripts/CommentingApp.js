(function () {
	var app = angular.module("CommentingApp", []);

	app.controller("CommentingController", ["$scope", "$http", function ($scope, $http) {
		$scope.LatesCommentText = "";
		$scope.SubmitComment = function (comment) {
			$http.post("/Comment/Create", comment)
			.then(
				function Success(response) {
					$scope.LatestCommentText = comment.Text;
					$scope.Comment.Text = "";
				},
				function Error(response) { }
			);
		};
	}]);
}());