(function () {
	var app = angular.module('KlassApp', ['UserListModule']);

	app.controller('KlassMemberTable', ["$scope", "$http", function ($scope, $http) {
		$scope.KlassId = null;
		$scope.NonMembers = {data: null};
		$scope.Members = {data: null};
		$scope.NonMembersTableAppends = [];
		$scope.MembersTableAppends = [];

		$scope.PostShit = function (tag) {
			for (i in taggedShit) if (taggedShit[i].tag == tag)
				return taggedShit[i];
		};

		var AddMember = function (user) {
			$http.get("../AddKlassMember?Id=" + $scope.KlassId + "&UId=" + user.Id)
			.then(function Success(response) {
				if (response.data.Added) {
					$scope.NonMembers.data.splice($scope.NonMembers.data.indexOf(user), 1);
					$scope.Members.data.push(user);
				}
			}, function Error(response) {
				alert(response);
			});
		};

		var RemoveMember = function (user) {
			$http.get("../RemoveKlassMember?Id=" + $scope.KlassId + "&UId=" + user.Id)
			.then(function Success(response) {
				if (response.data.Removed) {
					$scope.Members.data.splice($scope.Members.data.indexOf(user), 1);
					$scope.NonMembers.data.push(user);
				}
			}, function Error(response) {
				alert(response);
			});
		};

		var taggedShit = [
			{
				tag: "NonMembers",
				UserList: $scope.NonMembers,
				TableAppends: $scope.NonMembersTableAppends,
				Funcs: { AddMember: AddMember }
			},
			{
				tag: "Members",
				UserList: $scope.Members,
				TableAppends: $scope.MembersTableAppends,
				Funcs: { RemoveMember: RemoveMember }
			}
		];

		$scope.GetData = function (url, receiver) {
			$http.get("../" + url)
			.then(function Success(response) {
				receiver.data = response.data;
			}, function Error(response) {
				console.log(response);
			});
		}
	}]);
}());